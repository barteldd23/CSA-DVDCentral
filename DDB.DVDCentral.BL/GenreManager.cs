namespace DDB.DVDCentral.BL
{
    public static class GenreManager
    {
        
        public static int Insert(Genre genre,
                                 bool rollback = false) 
        {
            

            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblGenre entry = new tblGenre();
                    entry.Id = Guid.NewGuid();
                    entry.Description = genre.Description;

                    genre.Id = entry.Id;

                    dc.tblGenres.Add(entry);

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

        public static int Update(Genre genre,
                                 bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if(rollback) transaction = dc.Database.BeginTransaction();

                    tblGenre entity = dc.tblGenres.Where(e => e.Id == genre.Id).FirstOrDefault();

                    if (entity != null)
                    {
                        entity.Description = genre.Description;
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

        public static int Delete(Guid Id,
                                 bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblGenre entity = dc.tblGenres.Where(e => e.Id==Id).FirstOrDefault();
                    if (entity != null)
                    {
                        dc.tblGenres.Remove(entity);
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

        public static Genre LoadById(Guid id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblGenre entity = dc.tblGenres.Where(e => e.Id == id).FirstOrDefault();

                    if (entity != null)
                    {
                        return new Genre
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

        public static List<Genre> Load()
        {
            List<Genre> list = new List<Genre>();

            using (DVDCentralEntities dc = new DVDCentralEntities())
            {
                (from e in dc.tblGenres
                 select new
                 {
                     e.Id,
                     e.Description
                 })
                 .ToList()
                 .ForEach(genre => list.Add(new Genre
                 {
                     Id = genre.Id,
                     Description = genre.Description
                 }));
            }

            return list;
        }
    }
}