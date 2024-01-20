using DDB.DVDCentral.PL;
using DVDCentral.BL.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;

namespace DDB.DVDCentral.BL
{
    public static class MovieManager
    {
        
        public static int Insert(Movie movie,
                                 bool rollback = false) 
        {
            

            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblMovie entry = new tblMovie();
                    entry.Id = dc.tblMovies.Any() ? dc.tblMovies.Max(e => e.Id) + 1 : 1;
                    entry.Description = movie.Description;
                    entry.Title = movie.Title;
                    entry.Cost = movie.Cost;
                    entry.RatingId = movie.RatingId;
                    entry.FormatId = movie.FormatId;
                    entry.DirectorId = movie.DirectorId;
                    entry.InStkQty = movie.InStkQty;
                    entry.ImagePath = movie.ImagePath;

                    movie.Id = entry.Id;

                    dc.tblMovies.Add(entry);

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

        public static int Update(Movie movie,
                                 bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if(rollback) transaction = dc.Database.BeginTransaction();

                    tblMovie entry = dc.tblMovies.Where(e => e.Id == movie.Id).FirstOrDefault();

                    if (entry != null)
                    {
                        entry.Description = movie.Description;
                        entry.Title = movie.Title;
                        entry.Cost = movie.Cost;
                        entry.RatingId = movie.RatingId;
                        entry.FormatId = movie.FormatId;
                        entry.DirectorId = movie.DirectorId;
                        entry.InStkQty = movie.InStkQty;
                        entry.ImagePath = movie.ImagePath;
                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row does not exist");
                    }
                }
                
                return results;
                

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Delete(int Id,
                                 bool rollback=false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblMovie entity = dc.tblMovies.Where(e => e.Id == Id).FirstOrDefault();
                    if (entity != null)
                    {
                        dc.tblMovies.Remove(entity);
                        results = dc.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Row does not exist.");
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

        public static Movie LoadById(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblMovie entity = dc.tblMovies.Where(e => e.Id == id).FirstOrDefault();

                    if (entity != null)
                    {
                        return new Movie
                        {
                            Id = entity.Id,
                            Title = entity.Title,
                            Description = entity.Description,
                            Cost = entity.Cost,
                            RatingId = entity.RatingId,
                            FormatId = entity.FormatId,
                            DirectorId = entity.DirectorId,
                            InStkQty = entity.InStkQty,
                            ImagePath = entity.ImagePath
                        };
                    }
                    else
                    {
                        throw new Exception("Row does not exist.");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<Movie> Load(int? genreId = null)
        {
            List<Movie> list = new List<Movie>();

            using (DVDCentralEntities dc = new DVDCentralEntities())
            {
                (from e in dc.tblMovies
                 join f in dc.tblFormats on e.FormatId equals f.Id
                 join r in dc.tblRatings on e.RatingId equals r.Id
                 join d in dc.tblDirectors on e.DirectorId equals d.Id
                 join mg in dc.tblMovieGenres on e.Id equals mg.MovieId
                 where mg.GenreId == genreId || genreId == null
                 select new
                 {
                     e.Id,
                     e.Title,
                     e.Description,
                     e.Cost,
                     e.RatingId,
                     e.FormatId,
                     e.DirectorId, 
                     e.InStkQty,
                     e.ImagePath,
                     RatingDescription = r.Description,
                     FormatDescription = f.Description,
                     DirectorFullName = d.FirstName + " " + d.LastName
                 })
                 .Distinct().ToList()
                 .ForEach(movie => list.Add(new Movie
                 {
                     Id = movie.Id,
                     Title = movie.Title,
                     Description = movie.Description,
                     Cost = movie.Cost,
                     RatingId = movie.RatingId,
                     FormatId = movie.FormatId,
                     DirectorId = movie.DirectorId,
                     InStkQty = movie.InStkQty,
                     ImagePath = movie.ImagePath,
                     RatingDescription = movie.RatingDescription,
                     FormatDescription = movie.FormatDescription,
                     DirectorFullName = movie.DirectorFullName
                 }));
            }

            return list;
        }
    }
}