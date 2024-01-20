namespace DDB.DVDCentral.UI.ViewModels
{
    public class MovieViewModel
    {
        public Movie Movie { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Director> Directors { get; set; }
        public List<Rating> Ratings { get; set; }
        public List<Format> Formats { get; set; }
        public List<int> GenreIds { get; set; }
        public IFormFile File { get; set; }

        public MovieViewModel()
        {
            Genres = GenreManager.Load();
            Directors = DirectorManager.Load();
            Ratings = RatingManager.Load();
            Formats = FormatManager.Load();
            Movie = new Movie();
        }

        public MovieViewModel(int id)
        {
            try
            {
                Genres = GenreManager.Load();
                Directors = DirectorManager.Load();
                Ratings = RatingManager.Load();
                Formats = FormatManager.Load();
                Movie = MovieManager.LoadById(id);
                GenreIds = MovieGenreManager.GetGenres(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
