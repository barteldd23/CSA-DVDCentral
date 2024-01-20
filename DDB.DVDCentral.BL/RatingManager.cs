using DDB.DVDCentral.PL;
using DVDCentral.BL.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;

namespace DDB.DVDCentral.BL
{
    public static class RatingManager
    {
        
        public static int Insert(Rating rating,
                                 bool rollback = false) 
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblRating entry = new tblRating();
                    entry.Id = dc.tblRatings.Any() ? dc.tblRatings.Max(e => e.Id) + 1 : 1;
                    entry.Description = rating.Description;

                    rating.Id = entry.Id;

                    dc.tblRatings.Add(entry);

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

        public static int Update(Rating rating,
                                 bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if(rollback) transaction = dc.Database.BeginTransaction();

                    tblRating entity = dc.tblRatings.Where(e => e.Id == rating.Id).FirstOrDefault();

                    if (entity != null)
                    {
                        entity.Description = rating.Description;
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
                                 bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblRating entity = dc.tblRatings.Where(e => e.Id==Id).FirstOrDefault();
                    if (entity != null)
                    {
                        dc.tblRatings.Remove(entity);
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

        public static Rating LoadById(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblRating entity = dc.tblRatings.Where(e => e.Id == id).FirstOrDefault();

                    if (entity != null)
                    {
                        return new Rating
                        {
                            Id = entity.Id,
                            Description = entity.Description
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

        public static List<Rating> Load()
        {
            List<Rating> list = new List<Rating>();

            using (DVDCentralEntities dc = new DVDCentralEntities())
            {
                (from e in dc.tblRatings
                 select new
                 {
                     e.Id,
                     e.Description
                 })
                 .ToList()
                 .ForEach(rating => list.Add(new Rating
                 {
                     Id = rating.Id,
                     Description = rating.Description
                 }));
            }

            return list;
        }
    }
}