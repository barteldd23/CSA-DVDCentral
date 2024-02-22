using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BDF.Bingo.UI
{
    /// <summary>
    /// Interaction logic for Bingo.xaml
    /// </summary>
    public partial class BingoCard : Window
    {
        const int OFFSET = 100;
        const int TOP_MARGIN = 50;
        string[] title = new string[] { "B", "I", "N", "G", "O" };
        int[,] board = new int[5, 5];
        bool[,] boardState = new bool[5, 5];
        Label[,] boardLabels = new Label[6, 6];
        HubConnection _connection;

        MySettings mySettings;
        private readonly ILogger<BingoCard> _logger;
        string APIAddess;

        public BingoCard(ILogger<BingoCard> logger)
        {

            _logger = logger;
            InitializeComponent();
        }


        public BingoCard()
        {
            InitializeComponent();



        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            mySettings = App.Configuration.GetSection("MySettings").Get<MySettings>();
            APIAddess = mySettings.APIAddress;
            NewCard();
            ConnectToChannel();
        }

        private void NewCard()
        {
            board = new int[5, 5];
            boardLabels = new Label[6, 6];

            for (int i = grdBoard.Children.Count - 1; i < 0; i++)
            {
                if (grdBoard.Children[i].GetType() != typeof(Button))
                    grdBoard.Children.Remove(grdBoard.Children[i]);
            }

            for (int row = 0; row <= 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    Label label = new Label();
                    label.BorderBrush = Brushes.Blue;
                    label.BorderThickness = new Thickness(2);
                    label.Name = "lblBoard" + row.ToString() + col.ToString();
                    label.VerticalAlignment = VerticalAlignment.Center;
                    label.HorizontalAlignment = HorizontalAlignment.Left;

                    label.Margin = new Thickness(col * OFFSET, row * OFFSET + TOP_MARGIN, 525 - col * OFFSET, 600 - row * OFFSET);
                    label.Width = OFFSET;
                    label.Height = OFFSET;
                    label.HorizontalContentAlignment = HorizontalAlignment.Center;
                    label.VerticalContentAlignment = VerticalAlignment.Center;
                    label.FontSize = 24;

                    if (row == 0)
                    {
                        label.FontWeight = FontWeights.Bold;
                        label.Background = Brushes.LightBlue;
                        label.Content = title[col];
                    }
                    else
                    {
                        label.FontWeight = FontWeights.Normal;
                        label.Background = Brushes.LightPink;
                        Random generator = new Random();
                        int newnumber = generator.Next(1, 16) + (col * 15);
                        while (CheckforNumber(newnumber, true, out int r, out int c))
                        {
                            newnumber = generator.Next(1, 16) + (col * 15);
                        }
                        label.Content = "X";
                        board[row - 1, col] = newnumber;
                        boardState[row - 1, col] = false;
                        boardLabels[row, col] = label;
                    }
                    grdBoard.Children.Add(label);
                }
            }
            SortBoard();
            DisplayBoard();
        }

        private void SortBoard()
        {
            int[] numbers = new int[25];
            int index = 0;
            for (int row = 0; row <= board.GetUpperBound(0); row++)
            {
                for (int col = 0; col <= board.GetUpperBound(1); col++)
                {
                    numbers[index++] = board[row, col];
                }
            }

            Array.Sort(numbers);
            index = 0;

            for (int col = 0; col <= board.GetUpperBound(0); col++)
            {
                for (int row = 0; row <= board.GetUpperBound(1); row++)
                {
                    board[row, col] = numbers[index++];
                }
            }

        }

        private void DisplayBoard()
        {
            for (int col = 0; col <= board.GetUpperBound(0); col++)
            {
                for (int row = 0; row <= board.GetUpperBound(1); row++)
                {
                    //string controlName = "lblBoard" + (row+1).ToString() + col.ToString();
                    Label label = boardLabels[row + 1, col];
                    label.Content = board[row, col];
                }
            }
        }
        private bool CheckforNumber(int newnumber,
                                    bool loading,
                                    out int r,
                                    out int c)
        {
            for (int row = 0; row <= board.GetUpperBound(0); row++)
            {
                for (int col = 0; col <= board.GetUpperBound(1); col++)
                {
                    if (board[row, col] == newnumber)
                    {
                        r = row;
                        c = col;
                        if (!loading)
                            boardState[row, col] = true;
                        return true;
                    }
                }
            }
            r = 0;
            c = 0;
            return false;
        }

        void ConnectToChannel()
        {


            _connection = new HubConnectionBuilder()
                .WithUrl(APIAddess)
                .Build();


            _connection.On<string, string>("ReceiveMessage", (s1, s2) => OnSend(s1, s2));

            _connection.StartAsync();

            string message = DateTime.Now.ToString() + ": Connected...";
            this.Title = "Connected...";

            try
            {
                _connection.InvokeAsync("SendMessage", "Brian", message);
            }
            catch (Exception ex)
            {
                int i = 0;
                //Log(System.Drawing.Color.Red, ex.ToString());
            }


        }

        void SendMessageToChannel(string message)
        {
            try
            {
                //_connection.InvokeAsync("SendMessage", _connection.ConnectionId, message);
                _connection.InvokeAsync("SendMessage", "Brian", message);
            }
            catch (Exception ex)
            {
                //Log(System.Drawing.Color.Red, ex.ToString());
            }


        }
        private void OnSend(string name, string message)
        {

            // change the color of a good square.
            // Message will be last number selected.
            this.Title = message;
            int number;


            if (message == "newgame")
            {
                NewCard();
            }
            else
            {
                if (int.TryParse(message, out number))
                {
                    this.Title = "Looking for " + number.ToString();
                    if (CheckforNumber(Convert.ToInt32(message), false, out int r, out int c))
                    {
                        this.Title = "Found " + number.ToString() + " at : " + r + ", " + c;
                        boardLabels[r + 1, c].Background = Brushes.Purple;
                        boardLabels[r + 1, c].Foreground = Brushes.White;
                        SendMessageToChannel("Match");
                        if (CheckForWin())
                        {
                            this.Title += " - You win!";
                            SendMessageToChannel("Winner");
                        }
                    }
                    else
                    {
                        this.Title = "Not Found: " + number.ToString();
                    }
                }
            }
        }

        private bool CheckForWin()
        {
            // Check for Horizontals
            for (int row = 0; row <= board.GetUpperBound(0); row++)
            {
                if (boardState[row, 0]
                    && boardState[row, 1]
                    && boardState[row, 2]
                    && boardState[row, 3]
                    && boardState[row, 4])
                {
                    boardLabels[row + 1, 0].Background = Brushes.Red;
                    boardLabels[row + 1, 1].Background = Brushes.Red;
                    boardLabels[row + 1, 2].Background = Brushes.Red;
                    boardLabels[row + 1, 3].Background = Brushes.Red;
                    boardLabels[row + 1, 4].Background = Brushes.Red;
                    return true;
                }
            }
            // Check for Verticals
            for (int col = 0; col <= board.GetUpperBound(1); col++)
            {
                if (boardState[0, col]
                    && boardState[1, col]
                    && boardState[2, col]
                    && boardState[3, col]
                    && boardState[4, col])
                {
                    boardLabels[1, col].Background = Brushes.Red;
                    boardLabels[2, col].Background = Brushes.Red;
                    boardLabels[3, col].Background = Brushes.Red;
                    boardLabels[4, col].Background = Brushes.Red;
                    boardLabels[5, col].Background = Brushes.Red;
                    return true;
                }
            }

            // Check Diagonals
            if (boardState[0, 0]
                && boardState[1, 1]
                && boardState[2, 2]
                && boardState[3, 3]
                && boardState[4, 4])
            {
                boardLabels[1, 0].Background = Brushes.Red;
                boardLabels[2, 1].Background = Brushes.Red;
                boardLabels[3, 2].Background = Brushes.Red;
                boardLabels[4, 3].Background = Brushes.Red;
                boardLabels[5, 4].Background = Brushes.Red;
                return true;
            }

            if (boardState[0, 4]
                && boardState[1, 3]
                && boardState[2, 2]
                && boardState[3, 1]
                && boardState[4, 0])
            {
                boardLabels[1, 4].Background = Brushes.Red;
                boardLabels[2, 3].Background = Brushes.Red;
                boardLabels[3, 2].Background = Brushes.Red;
                boardLabels[4, 1].Background = Brushes.Red;
                boardLabels[5, 0].Background = Brushes.Red;
                return true;
            }
            return false;

        }

        private void btnNewCard_Click(object sender, RoutedEventArgs e)
        {
            NewCard();
        }
    }
}
