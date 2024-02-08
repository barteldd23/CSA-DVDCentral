namespace DDB.DVDCentral.BL
{
    public class MovieGenreManager : GenericManager<tblMovieGenre>
    {
        public MovieGenreManager(DbContextOptions<DVDCentralEntities> options) : base(options)
        {

        }

        public int Insert(Guid movieId, Guid genreId, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities(options))
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();
                    tblMovieGenre row = new tblMovieGenre();

                    row.Id = Guid.NewGuid();
                    row.MovieId = movieId;
                    row.GenreId = genreId;

                    dc.tblMovieGenres.Add(row);

                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();
                }

                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(Guid movieId, Guid genreId, bool rollback = false)
        {
            try
            {
                int results;
                using (DVDCentralEntities dc = new DVDCentralEntities(options))
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblMovieGenre row = dc.tblMovieGenres.FirstOrDefault(r => r.MovieId == movieId && r.GenreId == genreId);

                    if (row != null)
                    {
                        dc.tblMovieGenres.Remove(row);
                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row was not found.");
                    }
                }
                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(Guid moviegenreId, bool rollback = false)
        {
            try
            {
                int results;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblMovieGenre row = dc.tblMovieGenres.FirstOrDefault(r => r.Id == moviegenreId);

                    if (row != null)
                    {
                        dc.tblMovieGenres.Remove(row);
                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row was not found.");
                    }
                }
                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int Update(Guid movieId, Guid genreId, bool rollback = false)
        {
            try
            {
                int results;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblMovieGenre row = dc.tblMovieGenres.FirstOrDefault(r => r.MovieId == movieId && r.GenreId == genreId);

                    if (row != null)
                    {
                        row.MovieId = movieId;
                        row.GenreId = genreId;

                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row was not found.");
                    }
                }
                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
