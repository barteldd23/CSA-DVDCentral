namespace DDB.DVDCentral.BL
{
    public abstract class GenericManager<T> where T : class, IEntity
    {
        protected DbContextOptions<DVDCentralEntities> options;

        public GenericManager(DbContextOptions<DVDCentralEntities> options)
        {
            this.options = options;
        }

        public GenericManager() { }

        public List<T> Load()
        {
            try
            {
                return new DVDCentralEntities(options)
                    .Set<T>()
                    .ToList<T>()
                    .OrderBy(x => x.SortField)
                    .ToList<T>();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<T> Load(string storedproc)
        {
            try
            {
                return new DVDCentralEntities(options)
                    .Set<T>()
                    .FromSqlRaw($"exec {storedproc}")
                    .ToList<T>()
                    .OrderBy(x => x.SortField)
                    .ToList<T>();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public T LoadById(Guid id)
        {
            try
            {
                var row = new DVDCentralEntities(options).Set<T>().Where(t => t.Id == id).FirstOrDefault();
                return row;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Insert(T entity, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities(options))
                {
                    // Check if genre already exists - do not allow ....
                    //bool inUse = dc.tblGenres.Any(e => e.Description.Trim().ToUpper() == entity.Description.Trim().ToUpper());

                    //if (inUse && !rollback)
                    //{
                    //    throw new Exception("This entity already exists.");
                    //}

                    IDbContextTransaction dbTransaction = null;
                    if (rollback) dbTransaction = dc.Database.BeginTransaction();

                    entity.Id = Guid.NewGuid();

                    dc.Set<T>().Add(entity);
                    results = dc.SaveChanges();

                    if (rollback) dbTransaction.Rollback();

                }

                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Update(T entity, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities(options))
                {
                    IDbContextTransaction dbTransaction = null;
                    if (rollback) dbTransaction = dc.Database.BeginTransaction();

                    dc.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    results = dc.SaveChanges();

                    if (rollback) dbTransaction.Rollback();

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
                    IDbContextTransaction dbTransaction = null;
                    if (rollback) dbTransaction = dc.Database.BeginTransaction();

                    T row = dc.Set<T>().FirstOrDefault(t => t.Id == id);

                    if (row != null)
                    {
                        dc.Set<T>().Remove(row);
                        results = dc.SaveChanges();
                        if (rollback) dbTransaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row does not exist.");
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
