﻿namespace DDB.DVDCentral.BL
{
    public class RatingManager : GenericManager<tblRating>
    {
        public RatingManager(DbContextOptions<DVDCentralEntities> options) : base(options) { }

        public int Insert(Rating rating,
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
                    entry.Id = Guid.NewGuid();
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

        public int Update(Rating rating,
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

        public int Delete(Guid Id,
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

        public Rating LoadById(Guid id)
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

        public List<Rating> Load()
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