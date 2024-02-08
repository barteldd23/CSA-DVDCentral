namespace DDB.DVDCentral.BL
{
    public class RatingManager : GenericManager<tblRating>
    {
        public RatingManager(DbContextOptions<DVDCentralEntities> options) : base(options) { }

        public int Insert(Rating rating, bool rollback = false)
        {
            try
            {
                tblRating row = new tblRating { Description = rating.Description };
                rating.Id = row.Id;
                return base.Insert(row, rollback);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Rating> Load()
        {

            try
            {
                List<Rating> rows = new List<Rating>();
                base.Load()
                    .ForEach(d => rows.Add(
                        new Rating
                        {
                            Id = d.Id,
                            Description = d.Description,
                        }));

                return rows;
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
                tblRating row = base.LoadById(id);

                if (row != null)
                {
                    Rating rating = new Rating
                    {
                        Id = row.Id,
                        Description = row.Description,
                    };

                    return rating;
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

        public int Update(Rating rating, bool rollback = false)
        {
            try
            {
                int results = base.Update(new tblRating
                {
                    Id = rating.Id,
                    Description = rating.Description
                }, rollback);
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
                return base.Delete(id, rollback);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
