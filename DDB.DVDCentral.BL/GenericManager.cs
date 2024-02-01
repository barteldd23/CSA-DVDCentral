namespace DDB.DVDCentral.BL
{
    public abstract class GenericManager <T> where T : class, IEntity
    {
        protected DbContextOptions<DVDCentralEntities> options;

        public GenericManager(DbContextOptions<DVDCentralEntities> options)
        {
            this.options = options;
        }

        public List<T> Load()
        {
            try
            {

                return new DVDCentralEntities(options)
                    .Set<T>()
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
                return new DVDCentralEntities(options).Set<T>().Where(t => t.Id == id).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Insert(T entity, bool rollback = false)
        {
            return 0;
        }

        public int Update(T entity, bool rollback = false)
        {
            return 0;
        }

        public int Delete(Guid id, bool rollback = false)
        {
            return 0;
        }



    }
}
