using MessagingToolkit.QRCode.Codec;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DDB.DVDCentral.WPFUI
{
    /// <summary>
    /// Interaction logic for QrCode.xaml
    /// </summary>
    public partial class QrCode : Window
    {
        public QrCode()
        {
            InitializeComponent();
        }

        private void btnEncode_Click(object sender, RoutedEventArgs e)
        {
            QRCodeEncoder encoder = new QRCodeEncoder();
            Bitmap qrcode = encoder.Encode(txtInfo.Text);

            // display
            IntPtr hbitmap = qrcode.GetHbitmap();
            ImageSource imageSource = Imaging.CreateBitmapSourceFromHBitmap(hbitmap, 
                                                                                IntPtr.Zero,
                                                                                Int32Rect.Empty,
                                                                                BitmapSizeOptions.FromEmptyOptions());

            imgCode.Source = imageSource;

            qrcode.Save(DateTime.Now.ToLongDateString() + ".png", ImageFormat.Png);
        }
    }
}
