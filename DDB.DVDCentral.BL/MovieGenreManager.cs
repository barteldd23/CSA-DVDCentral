using DDB.DVDCentral.PL;
using DVDCentral.BL.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDB.DVDCentral.BL
{
    public static class MovieGenreManager
    {
        public static int Insert(int movieId,
                                 int genreId,
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
                    entity.Id = dc.tblMovieGenres.Any() ? dc.tblMovieGenres.Max(e => e.Id) + 1 : 1;
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

        public static int Update(int id,
                                 int newMovieId,
                                 int newGenreId,
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

        public static int Delete(int id,
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
        public static int Delete(int movieId,
                                 int genreId,
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

        public static List<int> GetGenres(int movieId)
        {
            using (DVDCentralEntities dc = new DVDCentralEntities())
            {

                tblMovieGenre entity = dc.tblMovieGenres.Where(e => e.MovieId == movieId).FirstOrDefault();
                if (entity != null)
                {
                    List<int> genreIds = new List<int>();
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
