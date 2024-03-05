using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDB.DVDCentral.BL
{
    public class MovieSPManager : GenericManager<spGetMoviesResult>
    {
        public MovieSPManager(DbContextOptions<DVDCentralEntities> options) : base(options) { }

        public List<spGetMoviesResult> Load(string storedproc = "spGetMovies")
        {
            try
            {
                List<spGetMoviesResult> movies = new List<spGetMoviesResult>();
                base.Load(storedproc)
                    .ForEach(m => movies.Add(
                        new spGetMoviesResult
                        {
                            Id = m.Id,
                            Title = m.Title,
                            Description = m.Description,
                            Cost = m.Cost,
                            RatingId = m.RatingId,
                            FormatId = m.FormatId,
                            DirectorId = m.DirectorId,
                            Quantity = m.Quantity,
                            RatingDescription = m.RatingDescription,
                            FormatDescription = m.FormatDescription,
                            LastName = m.LastName,
                            FirstName = m.FirstName
                        }));

                return movies;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
