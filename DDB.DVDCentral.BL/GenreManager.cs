using Microsoft.Extensions.Logging;

namespace DDB.DVDCentral.BL
{
    public class GenreManager : GenericManager<tblGenre>
    {
        public GenreManager(DbContextOptions<DVDCentralEntities> options) : base(options) { }

        public GenreManager(ILogger logger, DbContextOptions<DVDCentralEntities> options) : base(logger,options) { }

        public List<Genre> Load(Guid? movieId = null)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities(options))
                {
                    List<Genre> genres = new List<Genre>();

                    if (movieId != null)
                    {
                        var results = (from g in dc.tblGenres
                                       join mg in dc.tblMovieGenres on g.Id equals mg.GenreId
                                       where mg.MovieId == movieId || movieId == null
                                       select new
                                       {
                                           g.Id,
                                           g.Description
                                       }).Distinct().ToList();

                        results.ForEach(g => genres.Add(new Genre
                        {
                            Id = g.Id,
                            Description = g.Description
                        }));

                    }
                    else
                    {
                        var results = (from g in dc.tblGenres
                                       select new
                                       {
                                           g.Id,
                                           g.Description
                                       }).Distinct().ToList();

                        results.ForEach(g => genres.Add(new Genre
                        {
                            Id = g.Id,
                            Description = g.Description
                        }));
                    }

                    return genres.OrderBy(g => g.Description).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Genre LoadById(Guid id)
        {
            try
            {
                tblGenre row = base.LoadById(id);

                if (row != null)
                {
                    Genre genre = new Genre
                    {
                        Id = row.Id,
                        Description = row.Description,
                    };

                    return genre;
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Insert(Genre genre, bool rollback = false)
        {
            try
            {
                tblGenre row = new tblGenre { Description = genre.Description };
                genre.Id = row.Id;
                return base.Insert(row, rollback);

            }
            catch (Exception)
            {
                throw;
            }

        }


        public int Update(Genre genre, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities(options))
                {
                    // Check if genre already exists - do not allow ....
                    tblGenre existingGenre = dc.tblGenres.Where(g => g.Description.Trim().ToUpper() == genre.Description.Trim().ToUpper()).FirstOrDefault();

                    if (existingGenre != null && genre.Id != existingGenre.Id && rollback == false)
                    {
                        throw new Exception("This genre already exists.");
                    }
                    else
                    {
                        IDbContextTransaction transaction = null;
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        tblGenre upDateRow = dc.tblGenres.FirstOrDefault(g => g.Id == genre.Id);

                        if (upDateRow != null)
                        {
                            upDateRow.Description = genre.Description;

                            dc.tblGenres.Update(upDateRow);

                            // Commit the changes and get the number of rows affected
                            results = dc.SaveChanges();

                            if (rollback) transaction.Rollback();
                        }
                        else
                        {
                            throw new Exception("Row was not found.");
                        }
                    }
                }
                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public int Delete(Guid id, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities(options))
                {
                    // Check if genre is associated with an exisiting movie with only one genre - do not allow delete ....
                    var moviesWithThisGenre = dc.tblMovieGenres.Where(m => m.GenreId == id);
                    bool inuse = false;

                    foreach (var movie in moviesWithThisGenre)
                    {
                        if (new GenreManager(options).Load(movie.MovieId).Count == 1)
                        {
                            inuse = true;
                            break;
                        }
                    }

                    if (inuse && rollback == false)
                    {
                        throw new Exception("This genre is associated with an existing movie with only one genre assigned and therefore cannot be deleted.");
                    }
                    else
                    {
                        IDbContextTransaction transaction = null;
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        tblGenre genre = dc.tblGenres.FirstOrDefault(g => g.Id == id);

                        if (genre != null)
                        {
                            // delete all the associated tblMovieGenre rows. 
                            dc.tblMovieGenres.RemoveRange(moviesWithThisGenre);

                            // remove the genre
                            dc.tblGenres.Remove(genre);

                            // Commit the changes and get the number of rows affected
                            results = dc.SaveChanges();

                            if (rollback) transaction.Rollback();
                        }
                        else
                        {
                            throw new Exception("Row was not found.");
                        }
                    }
                }
                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}