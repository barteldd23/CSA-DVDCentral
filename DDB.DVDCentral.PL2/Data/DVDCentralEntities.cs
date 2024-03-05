using DDB.DVDCentral.PL2.Entities;
using Microsoft.EntityFrameworkCore;

namespace DDB.DVDCentral.PL2.Data
{
    public class DVDCentralEntities : DbContext
    {
        Guid[] userId = new Guid[4];
        Guid[] formatId = new Guid[4];
        Guid[] customerId = new Guid[3];
        Guid[] directorId = new Guid[6];
        Guid[] ratingId = new Guid[5];
        Guid[] genreId = new Guid[10];
        Guid[] movieId = new Guid[7];
        Guid[] orderId = new Guid[3];
        Guid[] cartId = new Guid[2];

        public virtual DbSet<tblCustomer> tblCustomers { get; set; }

        public virtual DbSet<tblDirector> tblDirectors { get; set; }

        public virtual DbSet<tblOrderItem> tblOrderItems { get; set; }

        public virtual DbSet<tblGenre> tblGenres { get; set; }

        public virtual DbSet<tblMovie> tblMovies { get; set; }

        public virtual DbSet<tblMovieGenre> tblMovieGenres { get; set; }

        public virtual DbSet<tblOrder> tblOrders { get; set; }

        public virtual DbSet<tblRating> tblRatings { get; set; }

        public virtual DbSet<tblFormat> tblFormats { get; set; }

        public virtual DbSet<tblUser> tblUsers { get; set; }

        public DVDCentralEntities(DbContextOptions<DVDCentralEntities> options) : base(options)
        {


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        public DVDCentralEntities()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            CreateUsers(modelBuilder);
            CreateGenres(modelBuilder);
            CreateFormats(modelBuilder);
            CreateCustomers(modelBuilder);
            CreateDirectors(modelBuilder);
            CreateRatings(modelBuilder);
            CreateMovies(modelBuilder);
            CreateMovieGenres(modelBuilder);
            CreateOrders(modelBuilder);
            CreateOrderItems(modelBuilder);
            CreateCarts(modelBuilder);
            CreateCartItems(modelBuilder);
        }

        private void CreateFormats(ModelBuilder modelBuilder)
        {
            for (int i = 0; i < formatId.Length; i++)
                formatId[i] = Guid.NewGuid();


            modelBuilder.Entity<tblFormat>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblFormat_Id");

                entity.ToTable("tblFormat");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            List<tblFormat> Formats = new List<tblFormat>
            {
                new tblFormat {Id = formatId[0], Description = "VHS"},
                new tblFormat {Id = formatId[1], Description = "DVD"},
                new tblFormat {Id = formatId[2], Description = "Blu-Ray" },
                new tblFormat {Id = formatId[3], Description = "Other" }
            };
            modelBuilder.Entity<tblFormat>().HasData(Formats);
        }


        private void CreateCarts(ModelBuilder modelBuilder)
        {
            for (int i = 0; i < cartId.Length; i++)
                cartId[i] = Guid.NewGuid();

            modelBuilder.Entity<tblCart>();


            List<tblCart> carts = new List<tblCart>
            {
                new tblCart {Id = cartId[0], UserId = userId[0]},
                new tblCart {Id = cartId[1], UserId = userId[1]}
            };
            modelBuilder.Entity<tblCart>().HasData(carts);
        }

        private void CreateCartItems(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblCartItem>();

            List<tblCartItem> cartItems = new List<tblCartItem>
            {
                new tblCartItem {Id = Guid.NewGuid(), CartId = cartId[0], MovieId = movieId[0], Quantity = 1},
                new tblCartItem {Id = Guid.NewGuid(), CartId = cartId[0], MovieId = movieId[1], Quantity = 2},
                new tblCartItem {Id = Guid.NewGuid(), CartId = cartId[1], MovieId = movieId[1], Quantity = 1}
            };
            modelBuilder.Entity<tblCartItem>().HasData(cartItems);
        }

        private void CreateUsers(ModelBuilder modelBuilder)
        {
            for (int i = 0; i < userId.Length; i++)
            {
                userId[i] = Guid.NewGuid();
            }

            modelBuilder.Entity<tblUser>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblUser_Id");

                entity.ToTable("tblUser");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(28)
                    .IsUnicode(false);
                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<tblUser>().HasData(new tblUser
            {
                Id = userId[0],
                FirstName = "Steve",
                LastName = "Marin",
                UserName = "smarin",
                Password = GetHash("maple")
            });
            modelBuilder.Entity<tblUser>().HasData(new tblUser
            {
                Id = userId[1],
                FirstName = "John",
                LastName = "Doro",
                UserName = "jdoro",
                Password = GetHash("maple")
            });
            modelBuilder.Entity<tblUser>().HasData(new tblUser
            {
                Id = userId[2],
                FirstName = "Brian",
                LastName = "Foote",
                UserName = "bfoote",
                Password = GetHash("maple")
            });

            modelBuilder.Entity<tblUser>().HasData(new tblUser
            {
                Id = userId[3],
                FirstName = "Other",
                LastName = "Other",
                UserName = "sophie",
                Password = GetHash("sophie")
            });
        }

        private void CreateRatings(ModelBuilder modelBuilder)
        {
            for (int i = 0; i < ratingId.Length; i++)
                ratingId[i] = Guid.NewGuid();

            modelBuilder.Entity<tblRating>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblRating_Id");

                entity.ToTable("tblRating");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            List<tblRating> Ratings = new List<tblRating>
            {
                new tblRating {Id = ratingId[0], Description = "G"},
                new tblRating {Id = ratingId[1], Description = "PG-13"},
                new tblRating {Id = ratingId[2], Description = "PG"},
                new tblRating {Id = ratingId[3], Description = "R" },
                new tblRating {Id = ratingId[4], Description = "Other"}
            };
            modelBuilder.Entity<tblRating>().HasData(Ratings);
        }

        private void CreateOrderItems(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblOrderItem>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblOrderItem_Id");

                entity.ToTable("tblOrderItem");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Order)
                  .WithMany(p => p.OrderItems)
                  .HasForeignKey(d => d.OrderId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("fk_tblOrderItem_OrderId");

            });


            List<tblOrderItem> OrderItems = new List<tblOrderItem>
            {
                new tblOrderItem {Id = Guid.NewGuid(), OrderId = orderId[1], Cost = 8.99, Quantity = 1, MovieId = movieId[0]},
                new tblOrderItem {Id = Guid.NewGuid(), OrderId = orderId[1], Cost = 9.99, Quantity = 1, MovieId = movieId[1]},
                new tblOrderItem {Id = Guid.NewGuid(), OrderId = orderId[2], Cost = 10.99, Quantity = 1, MovieId = movieId[1]}
            };
            modelBuilder.Entity<tblOrderItem>().HasData(OrderItems);
        }

        private void CreateOrders(ModelBuilder modelBuilder)
        {
            for (int i = 0; i < orderId.Length; i++)
            {
                orderId[i] = Guid.NewGuid();
            }

            modelBuilder.Entity<tblOrder>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblOrder_Id");

                entity.ToTable("tblOrder");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.OrderDate).HasColumnType("datetime");
                entity.Property(e => e.ShipDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                  .WithMany(p => p.Orders)
                  .HasForeignKey(d => d.CustomerId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("fk_tblOrder_CustomerId");

            });

            List<tblOrder> Orders = new List<tblOrder>
            {
                new tblOrder {Id = orderId[0], CustomerId = customerId[1], UserId = userId[1], OrderDate = new DateTime(2017, 9, 11), ShipDate = new DateTime(2017, 9, 15)},
                new tblOrder {Id = orderId[1], CustomerId = customerId[2], UserId = userId[1], OrderDate = new DateTime(2021, 5, 5), ShipDate = new DateTime(2021, 5, 10)},
                new tblOrder {Id = orderId[2], CustomerId = customerId[2], UserId = userId[2], OrderDate = new DateTime(2022, 10, 6), ShipDate = new DateTime(2022, 10, 11) },
            };

            modelBuilder.Entity<tblOrder>().HasData(Orders);
        }

        private void CreateMovieGenres(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblMovieGenre>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblMovieGenre_Id");

                entity.ToTable("tblMovieGenre");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Genre)
                .WithMany(p => p.tblMovieGenres)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("tblMovieGenre_GenreId");

                entity.HasOne(d => d.Movie)
                .WithMany(p => p.tblMovieGenres)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("tblMovieGenre_MovieId");


            });

            List<tblMovieGenre> MovieGenres = new List<tblMovieGenre>
            {
                new tblMovieGenre {Id = Guid.NewGuid(), GenreId = genreId[2], MovieId = movieId[0]},
                new tblMovieGenre {Id = Guid.NewGuid(), GenreId = genreId[3], MovieId = movieId[0]},
                new tblMovieGenre {Id = Guid.NewGuid(), GenreId = genreId[5], MovieId = movieId[0]},
                new tblMovieGenre {Id = Guid.NewGuid(), GenreId = genreId[2], MovieId = movieId[1]},
                new tblMovieGenre {Id = Guid.NewGuid(), GenreId = genreId[3], MovieId = movieId[1]},
                new tblMovieGenre {Id = Guid.NewGuid(), GenreId = genreId[0], MovieId = movieId[2]},
                new tblMovieGenre {Id = Guid.NewGuid(), GenreId = genreId[1], MovieId = movieId[2]},
                new tblMovieGenre {Id = Guid.NewGuid(), GenreId = genreId[5], MovieId = movieId[2]},
                new tblMovieGenre {Id = Guid.NewGuid(), GenreId = genreId[3], MovieId = movieId[3]},
                new tblMovieGenre {Id = Guid.NewGuid(), GenreId = genreId[5], MovieId = movieId[3]},
                new tblMovieGenre {Id = Guid.NewGuid(), GenreId = genreId[3], MovieId = movieId[4]},
                new tblMovieGenre {Id = Guid.NewGuid(), GenreId = genreId[6], MovieId = movieId[4]},
                new tblMovieGenre {Id = Guid.NewGuid(), GenreId = genreId[7], MovieId = movieId[5]},
            };

            modelBuilder.Entity<tblMovieGenre>().HasData(MovieGenres);
        }

        private void CreateMovies(ModelBuilder modelBuilder)
        {
            for (int i = 0; i < movieId.Length; i++)
            {
                movieId[i] = Guid.NewGuid();
            }

            modelBuilder.Entity<tblMovie>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblMovie_Id");
                entity.ToTable("tblMovie");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false);
                entity.Property(e => e.ImagePath)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Director)
                    .WithMany(p => p.tblMovies)
                    .HasForeignKey(d => d.DirectorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tblMovie_DirectorId");

                entity.HasOne(d => d.Format).WithMany(p => p.tblMovies)
                    .HasForeignKey(d => d.FormatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tblMovie_FormatId");

                entity.HasOne(d => d.Rating).WithMany(p => p.tblMovies)
                    .HasForeignKey(d => d.RatingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tblMovie_RatingId");

                // Include Stored Procedure
                modelBuilder.Entity<spGetMoviesResult>().HasNoKey();

            });


            List<tblMovie> Movies = new List<tblMovie>
            {
                new tblMovie {
                    Id = movieId[0],
                    Title = "Rocky",
                    Description = "Rocky is a 1976 American sports drama film directed by John G. Avildsen, written by and starring Sylvester Stallone.",
                    Cost = 6.99,
                    RatingId = ratingId[0],
                    FormatId = formatId[0],
                    DirectorId = directorId[0],
                    Quantity = 2,
                    ImagePath = "Rocky.jpg"
                },
                 new tblMovie {
                    Id = movieId[6],
                    Title = "Other",
                    Description = "Other",
                    Cost = 6.99,
                    RatingId = ratingId[0],
                    FormatId = formatId[0],
                    DirectorId = directorId[0],
                    Quantity = 2,
                    ImagePath = "Rocky.jpg"
                },

                new tblMovie {
                    Id = movieId[1],
                    Title = "Jaws",
                    Description ="Jaws is a 1975 American thriller film directed by Steven Spielberg and based on the Peter Benchley 1974 novel of the same name.",
                    Cost = 8.99,
                    RatingId = ratingId[1],
                    FormatId = formatId[1],
                    DirectorId = directorId[1],
                    Quantity = 1,
                    ImagePath = "Jaws1.jpg"
                },
                new tblMovie {
                    Id = movieId[2],
                    Title = "The Princess Bride",
                    Description = "The Princess Bride is a 1987 American fantasy adventure comedy film directed and co-produced by Rob Reiner, starring Cary Elwes, Robin Wright, Mandy Patinkin, Chris Sarandon, Wallace Shawn, André the Giant, and Christopher Guest.",
                    Cost = 12.50,
                    RatingId = ratingId[2],
                    FormatId = formatId[2],
                    DirectorId = directorId[2],
                    Quantity = 4,
                    ImagePath = "PrincessBride.jpg"
                },
                new tblMovie {
                    Id = movieId[3],
                    Title = "Indiana Jones and the Last Crusade",
                    Description = "Indiana Jones and the Last Crusade is a 1989 American action-adventure film directed by Steven Spielberg, from a story co-written by executive producer George Lucas.",
                    Cost = 10.50,
                    RatingId = ratingId[3],
                    FormatId = formatId[2],
                    DirectorId = directorId[3],
                    Quantity = 2,
                    ImagePath = "IndianaJonesLastCrusade.jpg"
                },
                new tblMovie {
                    Id = movieId[4],
                    Title = "Star Wars: Episode IV – A New Hope",
                    Description = "Star Wars: Episode IV – A New Hope is a 1977 American epic space-opera film written and directed by George Lucas, produced by Lucasfilm and distributed by 20th Century Fox.",
                    Cost = 7.50,
                    RatingId = ratingId[1],
                    FormatId = formatId[1],
                    DirectorId = directorId[1],
                    Quantity = 1,
                    ImagePath = "StarWarsNewHope.jpg"
                },
                new tblMovie {
                    Id = movieId[5],
                    Title = "Pale Rider",
                    Description = "Pale Rider is a 1985 American Western film produced and directed by Clint Eastwood, who also stars in the lead role.",
                    Cost = 9.99,
                    RatingId = ratingId[1],
                    FormatId = formatId[1],
                    DirectorId = directorId[1],
                    Quantity = 1,
                    ImagePath = "PaleRider.jpg"
                }
            };
            modelBuilder.Entity<tblMovie>().HasData(Movies);
        }

        private void CreateDirectors(ModelBuilder modelBuilder)
        {
            for (int i = 0; i < directorId.Length; i++)
                directorId[i] = Guid.NewGuid();

            modelBuilder.Entity<tblDirector>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblDirector_Id");

                entity.ToTable("tblDirector");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            List<tblDirector> directors = new List<tblDirector>
            {
                new tblDirector {Id = directorId[0], FirstName = "John", LastName = "Avildsen"},
                new tblDirector {Id = directorId[1], FirstName = "Steven", LastName = "Spielberg"},
                new tblDirector {Id = directorId[2], FirstName = "Rob", LastName = "Reiner"},
                new tblDirector {Id = directorId[3], FirstName = "George", LastName = "Lucas"},
                new tblDirector {Id = directorId[4], FirstName = "Clint", LastName = "Eastwood"},
                new tblDirector {Id = directorId[5], FirstName = "Other", LastName = "Other"}
            };

            modelBuilder.Entity<tblDirector>().HasData(directors);
        }

        private void CreateCustomers(ModelBuilder modelBuilder)
        {
            for (int i = 0; i < customerId.Length; i++)
                customerId[i] = Guid.NewGuid();

            modelBuilder.Entity<tblCustomer>(entity =>
            {
                entity.ToTable("tblCustomer");

                entity.HasKey(e => e.Id).HasName("PK_tblCustomer_Id");

                entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.ZIP)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<tblCustomer>().HasData(new tblCustomer
            {
                Id = customerId[0],
                FirstName = "Steve",
                LastName = "Marin",
                Address = "453 Oak Street",
                City = "Fond du Lac",
                State = "WI",
                ZIP = "54935",
                Phone = "9205879797",
                UserId = userId[0]
            });

            modelBuilder.Entity<tblCustomer>().HasData(new tblCustomer
            {
                Id = customerId[1],
                FirstName = "John",
                LastName = "Doro",
                Address = "987 Willow Road",
                City = "Slinger",
                State = "WI",
                ZIP = "56495",
                Phone = "9202623345",
                UserId = userId[1]
            });

            modelBuilder.Entity<tblCustomer>().HasData(new tblCustomer
            {
                Id = customerId[2],
                FirstName = "Brian",
                LastName = "Foote",
                Address = "159 Johnson Avenue",
                City = "Allenton",
                State = "WI",
                ZIP = "53142",
                Phone = "9202623415",
                UserId = userId[2]
            });
        }

        private void CreateGenres(ModelBuilder modelBuilder)
        {
            for (int i = 0; i < genreId.Length; i++)
                genreId[i] = Guid.NewGuid();

            modelBuilder.Entity<tblGenre>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblGenre_Id");

                entity.ToTable("tblGenre");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            List<tblGenre> Genres = new List<tblGenre>
            {
                new tblGenre {Id = genreId[0], Description = "Comedy"},
                new tblGenre {Id = genreId[1], Description = "Action"},
                new tblGenre {Id = genreId[2], Description = "Sci-Fi"},
                new tblGenre {Id = genreId[3], Description = "Horror"},
                new tblGenre {Id = genreId[4], Description = "Romance"},
                new tblGenre {Id = genreId[5], Description = "Documentary"},
                new tblGenre {Id = genreId[6], Description = "Musical"},
                new tblGenre {Id = genreId[7], Description = "Mystery"},
                new tblGenre {Id = genreId[8], Description = "Western"},
                new tblGenre {Id = genreId[9], Description = "Other"},
            };

            modelBuilder.Entity<tblGenre>().HasData(Genres);

        }

        private static string GetHash(string Password)
        {
            using (var hasher = new System.Security.Cryptography.SHA1Managed())
            {
                var hashbytes = System.Text.Encoding.UTF8.GetBytes(Password);
                return Convert.ToBase64String(hasher.ComputeHash(hashbytes));
            }
        }

    }
}
