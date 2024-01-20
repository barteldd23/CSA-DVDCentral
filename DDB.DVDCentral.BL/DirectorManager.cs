using DDB.DVDCentral.PL;
using DVDCentral.BL.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;

namespace DDB.DVDCentral.BL
{
    public static class DirectorManager
    {
        
        public static int Insert(Director director,
                                 bool rollback = false) 
        {
            

            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblDirector entry = new tblDirector();
                    entry.Id = dc.tblDirectors.Any() ? dc.tblDirectors.Max(e => e.Id) + 1 : 1;
                    entry.FirstName = director.FirstName;
                    entry.LastName = director.LastName;

                    director.Id = entry.Id;

                    dc.tblDirectors.Add(entry);

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

        public static int Update(Director director,
                                 bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if(rollback) transaction = dc.Database.BeginTransaction();

                    tblDirector entity = dc.tblDirectors.Where(e => e.Id == director.Id).FirstOrDefault();

                    if (entity != null)
                    {
                        entity.FirstName = director.FirstName;
                        entity.LastName = director.LastName;
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

                    tblDirector entity = dc.tblDirectors.Where(e => e.Id==Id).FirstOrDefault();
                    if (entity != null)
                    {
                        dc.tblDirectors.Remove(entity);
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

        public static Director LoadById(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblDirector entity = dc.tblDirectors.Where(e => e.Id == id).FirstOrDefault();

                    if (entity != null)
                    {
                        return new Director
                        {
                            Id = entity.Id,
                            FirstName = entity.FirstName,
                            LastName = entity.LastName
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

        public static List<Director> Load()
        {
            List<Director> list = new List<Director>();

            using (DVDCentralEntities dc = new DVDCentralEntities())
            {
                (from e in dc.tblDirectors
                 select new
                 {
                     e.Id,
                     e.FirstName,
                     e.LastName
                 })
                 .ToList()
                 .ForEach(director => list.Add(new Director
                 {
                     Id = director.Id,
                     FirstName = director.FirstName,
                     LastName = director.LastName
                 }));
            }

            return list;
        }
    }
}