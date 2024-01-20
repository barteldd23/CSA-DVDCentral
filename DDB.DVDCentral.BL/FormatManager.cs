using DDB.DVDCentral.PL;
using DVDCentral.BL.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;

namespace DDB.DVDCentral.BL
{
    public static class FormatManager
    {
        
        public static int Insert(Format format,
                                 bool rollback = false) 
        {
            

            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblFormat entry = new tblFormat();
                    entry.Id = dc.tblFormats.Any() ? dc.tblFormats.Max(e => e.Id) + 1 : 1;
                    entry.Description = format.Description;

                    format.Id = entry.Id;

                    dc.tblFormats.Add(entry);

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

        public static int Update(Format format,
                                 bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if(rollback) transaction = dc.Database.BeginTransaction();

                    tblFormat entity = dc.tblFormats.Where(e => e.Id == format.Id).FirstOrDefault();

                    if (entity != null)
                    {
                        entity.Description = format.Description;
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

                    tblFormat entity = dc.tblFormats.Where(e => e.Id==Id).FirstOrDefault();
                    if (entity != null)
                    {
                        dc.tblFormats.Remove(entity);
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

        public static Format LoadById(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblFormat entity = dc.tblFormats.Where(e => e.Id == id).FirstOrDefault();

                    if (entity != null)
                    {
                        return new Format
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

        public static List<Format> Load()
        {
            List<Format> list = new List<Format>();

            using (DVDCentralEntities dc = new DVDCentralEntities())
            {
                (from e in dc.tblFormats
                 select new
                 {
                     e.Id,
                     e.Description
                 })
                 .ToList()
                 .ForEach(format => list.Add(new Format
                 {
                     Id = format.Id,
                     Description = format.Description
                 }));
            }

            return list;
        }
    }
}