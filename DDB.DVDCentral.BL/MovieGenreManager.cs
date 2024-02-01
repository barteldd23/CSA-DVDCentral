namespace DDB.DVDCentral.BL
{
    public static class MovieGenreManager
    {
        public static int Insert(Guid movieId,
                                 Guid genreId,
                                 bool rollback = false)
        {
            int results = 0;

            try
            {
                using DVDCentralEntities dc = new DVDCentralEntities();
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblMovieGenre entity = new tblMovieGenre();
                    entity.Id = Guid.NewGuid();
                    entity.MovieId = movieId;
                    entity.GenreId = genreId;

                    dc.tblMovieGenres.Add(entity);
                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();
                }

                return results;
            }
            catch (Exception)
            {

                throw;
            } 
        }

        public static int Update(Guid id,
                                 Guid newMovieId,
                                 Guid newGenreId,
                                 bool rollback=false)
        {
            int results = 0;
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();
                    tblMovieGenre entity = dc.tblMovieGenres.Where(e => e.MovieId == id).FirstOrDefault();
                    if (entity != null)
                    {
                        entity.MovieId = newMovieId;
                        entity.GenreId = newGenreId;
                        
                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row Does Not Exist.");
                    }

                }

                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Delete(Guid id,
                                 bool rollback = false)
        {
            int results = 0;
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if(rollback) transaction = dc.Database.BeginTransaction();

                    tblMovieGenre entity = dc.tblMovieGenres.Where(e =>e.Id == id).FirstOrDefault();
                    if (entity != null)
                    {
                        dc.tblMovieGenres.Remove(entity);
                        results = dc.SaveChanges();
                        
                    }
                    else
                    {
                        throw new Exception("Row Does Not Exist.");
                    }

                    if (rollback) transaction.Rollback();
                    return results;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int Delete(Guid movieId,
                                 Guid genreId,
                                 bool rollback = false)
        {
            int results = 0;
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblMovieGenre entity = dc.tblMovieGenres.Where(e => e.MovieId == movieId && e.GenreId == genreId).FirstOrDefault();
                    if (entity != null)
                    {
                        dc.tblMovieGenres.Remove(entity);
                        results = dc.SaveChanges();

                    }
                    else
                    {
                        throw new Exception("Row Does Not Exist.");
                    }

                    if (rollback) transaction.Rollback();
                    return results;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<Guid> GetGenres(Guid movieId)
        {
            using (DVDCentralEntities dc = new DVDCentralEntities())
            {

                tblMovieGenre entity = dc.tblMovieGenres.Where(e => e.MovieId == movieId).FirstOrDefault();
                if (entity != null)
                {
                    List<Guid> genreIds = new List<Guid>();
                    List<tblMovieGenre> entities = dc.tblMovieGenres.Where(e => e.Id == movieId).ToList();
                    foreach(tblMovieGenre item in entities)
                    {
                        genreIds.Add(item.GenreId);
                    }
                    return genreIds;

                }
                else
                {
                    throw new Exception("No Genres");
                }

            }
        }

    }
}
