using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DDB.DVDCentral.PL2.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblCustomer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    State = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ZIP = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: false),
                    Phone = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCustomer_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblDirector",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDirector_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblFormat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFormat_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblGenre",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblGenre_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOrder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShipDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOrder_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOrderItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOrderItem_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblRating",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRating_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    FirstName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "varchar(28)", unicode: false, maxLength: 28, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUser_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblMovie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    RatingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DirectorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMovie_Id", x => x.Id);
                    table.ForeignKey(
                        name: "fk_tblMovie_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "tblDirector",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "fk_tblMovie_FormatId",
                        column: x => x.FormatId,
                        principalTable: "tblFormat",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "fk_tblMovie_RatingId",
                        column: x => x.RatingId,
                        principalTable: "tblRating",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tblCart",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblCart_tblUser_UserId",
                        column: x => x.UserId,
                        principalTable: "tblUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblMovieGenre",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMovieGenre_Id", x => x.Id);
                    table.ForeignKey(
                        name: "tblMovieGenre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "tblGenre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "tblMovieGenre_MovieId",
                        column: x => x.MovieId,
                        principalTable: "tblMovie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblCartItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCartItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblCartItem_tblCart_CartId",
                        column: x => x.CartId,
                        principalTable: "tblCart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblCartItem_tblMovie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "tblMovie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "tblCustomer",
                columns: new[] { "Id", "Address", "City", "FirstName", "LastName", "Phone", "State", "UserId", "ZIP" },
                values: new object[,]
                {
                    { new Guid("13940c48-f87e-420f-8329-aecb37dfef68"), "453 Oak Street", "Fond du Lac", "Steve", "Marin", "9205879797", "WI", new Guid("d199bf78-3bb3-40dd-8f6d-06d6bdc5b404"), "54935" },
                    { new Guid("8abb067e-6b0f-409c-8e08-f5d8b5354c80"), "159 Johnson Avenue", "Allenton", "Brian", "Foote", "9202623415", "WI", new Guid("d85b818c-1be6-4be1-b925-d9af249d83bf"), "53142" },
                    { new Guid("c919a719-b175-40a0-95c3-b9a965dfbc0a"), "987 Willow Road", "Slinger", "John", "Doro", "9202623345", "WI", new Guid("ad65c547-7df1-4085-97d1-c9c3327d608d"), "56495" }
                });

            migrationBuilder.InsertData(
                table: "tblDirector",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("4c090eef-2e71-4986-aafd-8313d208c581"), "Steven", "Spielberg" },
                    { new Guid("73091ae1-8ead-41ff-8b49-5f26eaf23a5f"), "Other", "Other" },
                    { new Guid("7eba7696-e529-4341-bab7-59a60fce9f45"), "Clint", "Eastwood" },
                    { new Guid("886dc06f-dc36-4aad-a81a-a474e6dc47bd"), "John", "Avildsen" },
                    { new Guid("983cd154-bbb4-4b74-94ae-c167b0ab2128"), "Rob", "Reiner" },
                    { new Guid("fca5d738-a87f-432a-a95b-d5e968073b79"), "George", "Lucas" }
                });

            migrationBuilder.InsertData(
                table: "tblFormat",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { new Guid("a231d4ba-102d-4bc0-a608-d90e8bf74a82"), "VHS" },
                    { new Guid("b5a83b31-05dd-47ee-9bd1-2f1574cd2120"), "DVD" },
                    { new Guid("f17f9043-d5d6-4618-88f9-ad9b48549f9f"), "Blu-Ray" },
                    { new Guid("f2e7f900-353b-41c5-ad18-952f430906e8"), "Other" }
                });

            migrationBuilder.InsertData(
                table: "tblGenre",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { new Guid("179e337f-afa9-44dc-8322-44a76c2ce106"), "Horror" },
                    { new Guid("31e9baf7-adfa-4b1d-865d-0ff6bff4fbbb"), "Sci-Fi" },
                    { new Guid("67c6a687-292d-4502-8369-4ece917e3877"), "Comedy" },
                    { new Guid("73fc274f-826b-4668-9070-09ef9d9afd78"), "Action" },
                    { new Guid("95c0abd3-6b8b-492c-a395-b9e2bd867415"), "Documentary" },
                    { new Guid("a9893ca8-7f18-42cb-8ef0-2c2f8308ecf4"), "Mystery" },
                    { new Guid("ae2ce9c8-3a7c-4985-86d0-c68d5b86e630"), "Musical" },
                    { new Guid("de4c5b06-dde6-4d14-9f9e-5895b8550ade"), "Western" },
                    { new Guid("e16e3d25-5e87-4cb5-ad3b-9cbf616ac330"), "Romance" },
                    { new Guid("ff869789-dd03-498d-ba9b-1fde7810faa7"), "Other" }
                });

            migrationBuilder.InsertData(
                table: "tblOrder",
                columns: new[] { "Id", "CustomerId", "OrderDate", "ShipDate", "UserId" },
                values: new object[,]
                {
                    { new Guid("25ec747b-5277-4af3-870f-000e1da4c38a"), new Guid("8abb067e-6b0f-409c-8e08-f5d8b5354c80"), new DateTime(2022, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d85b818c-1be6-4be1-b925-d9af249d83bf") },
                    { new Guid("5c6df3cd-350a-4464-9567-d54d5044c998"), new Guid("8abb067e-6b0f-409c-8e08-f5d8b5354c80"), new DateTime(2021, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ad65c547-7df1-4085-97d1-c9c3327d608d") },
                    { new Guid("af6d8837-bd7b-4457-a6d8-d8c171be76ea"), new Guid("c919a719-b175-40a0-95c3-b9a965dfbc0a"), new DateTime(2017, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2017, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ad65c547-7df1-4085-97d1-c9c3327d608d") }
                });

            migrationBuilder.InsertData(
                table: "tblOrderItem",
                columns: new[] { "Id", "Cost", "MovieId", "OrderId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("b00da67b-cbac-4ef6-8ba2-86856e45edd6"), 10.99, new Guid("3440a337-f353-42d7-8227-81a7e2737925"), new Guid("5c6df3cd-350a-4464-9567-d54d5044c998"), 0 },
                    { new Guid("b1e6aeea-5d8f-4fc8-9bc8-a79a603f0b30"), 8.9900000000000002, new Guid("2c1073c4-d98a-4929-8541-77e619e9e48f"), new Guid("af6d8837-bd7b-4457-a6d8-d8c171be76ea"), 0 },
                    { new Guid("e006bdfe-712e-4464-9443-323f9b01eb36"), 9.9900000000000002, new Guid("3440a337-f353-42d7-8227-81a7e2737925"), new Guid("af6d8837-bd7b-4457-a6d8-d8c171be76ea"), 0 }
                });

            migrationBuilder.InsertData(
                table: "tblRating",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { new Guid("60d883fb-bb1e-4f17-bafd-8f2a0f13522c"), "Other" },
                    { new Guid("728fa8da-22f3-4311-9098-9dfc103eb341"), "G" },
                    { new Guid("ec4fbee8-88cf-4eb4-a8df-3ba87c61ed12"), "PG-13" },
                    { new Guid("ef99cf54-ede2-4e04-b402-1ac94d841050"), "PG" },
                    { new Guid("f42296df-7d7d-40eb-bd63-b46dfff1681f"), "R" }
                });

            migrationBuilder.InsertData(
                table: "tblUser",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "UserName" },
                values: new object[,]
                {
                    { new Guid("ad65c547-7df1-4085-97d1-c9c3327d608d"), "John", "Doro", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", "jdoro" },
                    { new Guid("d199bf78-3bb3-40dd-8f6d-06d6bdc5b404"), "Steve", "Marin", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", "smarin" },
                    { new Guid("d85b818c-1be6-4be1-b925-d9af249d83bf"), "Brian", "Foote", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", "bfoote" }
                });

            migrationBuilder.InsertData(
                table: "tblCart",
                columns: new[] { "Id", "UserId" },
                values: new object[,]
                {
                    { new Guid("3660f6ea-a990-48fb-a110-18f5e5269b15"), new Guid("ad65c547-7df1-4085-97d1-c9c3327d608d") },
                    { new Guid("447d9c4e-a25f-4814-bc2b-1a089f2161d7"), new Guid("d199bf78-3bb3-40dd-8f6d-06d6bdc5b404") }
                });

            migrationBuilder.InsertData(
                table: "tblMovie",
                columns: new[] { "Id", "Cost", "Description", "DirectorId", "FormatId", "ImagePath", "Quantity", "RatingId", "Title" },
                values: new object[,]
                {
                    { new Guid("16e596d1-3a51-409f-ab32-56d000fba06c"), 9.9900000000000002, "Pale Rider is a 1985 American Western film produced and directed by Clint Eastwood, who also stars in the lead role.", new Guid("4c090eef-2e71-4986-aafd-8313d208c581"), new Guid("b5a83b31-05dd-47ee-9bd1-2f1574cd2120"), "PaleRider.jpg", 1, new Guid("ec4fbee8-88cf-4eb4-a8df-3ba87c61ed12"), "Pale Rider" },
                    { new Guid("1f7c68d3-8a65-41ea-99e9-5c4f6bada051"), 7.5, "Star Wars: Episode IV – A New Hope is a 1977 American epic space-opera film written and directed by George Lucas, produced by Lucasfilm and distributed by 20th Century Fox.", new Guid("4c090eef-2e71-4986-aafd-8313d208c581"), new Guid("b5a83b31-05dd-47ee-9bd1-2f1574cd2120"), "StarWarsNewHope.jpg", 1, new Guid("ec4fbee8-88cf-4eb4-a8df-3ba87c61ed12"), "Star Wars: Episode IV – A New Hope" },
                    { new Guid("2c1073c4-d98a-4929-8541-77e619e9e48f"), 6.9900000000000002, "Rocky is a 1976 American sports drama film directed by John G. Avildsen, written by and starring Sylvester Stallone.", new Guid("886dc06f-dc36-4aad-a81a-a474e6dc47bd"), new Guid("a231d4ba-102d-4bc0-a608-d90e8bf74a82"), "Rocky.jpg", 2, new Guid("728fa8da-22f3-4311-9098-9dfc103eb341"), "Rocky" },
                    { new Guid("3440a337-f353-42d7-8227-81a7e2737925"), 8.9900000000000002, "Jaws is a 1975 American thriller film directed by Steven Spielberg and based on the Peter Benchley 1974 novel of the same name.", new Guid("4c090eef-2e71-4986-aafd-8313d208c581"), new Guid("b5a83b31-05dd-47ee-9bd1-2f1574cd2120"), "Jaws1.jpg", 1, new Guid("ec4fbee8-88cf-4eb4-a8df-3ba87c61ed12"), "Jaws" },
                    { new Guid("4bbb3600-4df7-4cfc-a9b0-28bb16cfbc37"), 12.5, "The Princess Bride is a 1987 American fantasy adventure comedy film directed and co-produced by Rob Reiner, starring Cary Elwes, Robin Wright, Mandy Patinkin, Chris Sarandon, Wallace Shawn, André the Giant, and Christopher Guest.", new Guid("983cd154-bbb4-4b74-94ae-c167b0ab2128"), new Guid("f17f9043-d5d6-4618-88f9-ad9b48549f9f"), "PrincessBride.jpg", 4, new Guid("ef99cf54-ede2-4e04-b402-1ac94d841050"), "The Princess Bride" },
                    { new Guid("54ee857e-f175-4649-b3f8-5f0c226aed95"), 10.5, "Indiana Jones and the Last Crusade is a 1989 American action-adventure film directed by Steven Spielberg, from a story co-written by executive producer George Lucas.", new Guid("fca5d738-a87f-432a-a95b-d5e968073b79"), new Guid("f17f9043-d5d6-4618-88f9-ad9b48549f9f"), "IndianaJonesLastCrusade.jpg", 2, new Guid("f42296df-7d7d-40eb-bd63-b46dfff1681f"), "Indiana Jones and the Last Crusade" },
                    { new Guid("913cd5df-2225-4bca-a9da-43ff1e42f7f1"), 6.9900000000000002, "Other", new Guid("886dc06f-dc36-4aad-a81a-a474e6dc47bd"), new Guid("a231d4ba-102d-4bc0-a608-d90e8bf74a82"), "Rocky.jpg", 2, new Guid("728fa8da-22f3-4311-9098-9dfc103eb341"), "Other" }
                });

            migrationBuilder.InsertData(
                table: "tblCartItem",
                columns: new[] { "Id", "CartId", "MovieId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("9ede93ed-f379-4268-a94a-4cfde7d9b69b"), new Guid("447d9c4e-a25f-4814-bc2b-1a089f2161d7"), new Guid("3440a337-f353-42d7-8227-81a7e2737925"), 2 },
                    { new Guid("b13699f5-0c56-46dd-a74d-9557195ccd17"), new Guid("3660f6ea-a990-48fb-a110-18f5e5269b15"), new Guid("3440a337-f353-42d7-8227-81a7e2737925"), 1 },
                    { new Guid("c1d736d5-6781-4d1d-b788-a20dfd6baa15"), new Guid("447d9c4e-a25f-4814-bc2b-1a089f2161d7"), new Guid("2c1073c4-d98a-4929-8541-77e619e9e48f"), 1 }
                });

            migrationBuilder.InsertData(
                table: "tblMovieGenre",
                columns: new[] { "Id", "GenreId", "MovieId" },
                values: new object[,]
                {
                    { new Guid("00be876e-e1ea-41a2-aa94-0063a3647aae"), new Guid("31e9baf7-adfa-4b1d-865d-0ff6bff4fbbb"), new Guid("2c1073c4-d98a-4929-8541-77e619e9e48f") },
                    { new Guid("02082e9c-a000-4d79-86ca-19e6f6dccd80"), new Guid("67c6a687-292d-4502-8369-4ece917e3877"), new Guid("4bbb3600-4df7-4cfc-a9b0-28bb16cfbc37") },
                    { new Guid("20c9d141-06b3-45f4-a0e3-490c3baf0a32"), new Guid("73fc274f-826b-4668-9070-09ef9d9afd78"), new Guid("4bbb3600-4df7-4cfc-a9b0-28bb16cfbc37") },
                    { new Guid("2c706ef2-46b1-4b95-b1d7-446947ce33f4"), new Guid("ae2ce9c8-3a7c-4985-86d0-c68d5b86e630"), new Guid("1f7c68d3-8a65-41ea-99e9-5c4f6bada051") },
                    { new Guid("347bbbd0-baff-4a60-a61c-a795ae140ffc"), new Guid("31e9baf7-adfa-4b1d-865d-0ff6bff4fbbb"), new Guid("3440a337-f353-42d7-8227-81a7e2737925") },
                    { new Guid("4f1b6f91-2b68-44f2-9bb8-06c29f272700"), new Guid("179e337f-afa9-44dc-8322-44a76c2ce106"), new Guid("1f7c68d3-8a65-41ea-99e9-5c4f6bada051") },
                    { new Guid("5164eedf-858e-43b0-9834-429e565b84cd"), new Guid("179e337f-afa9-44dc-8322-44a76c2ce106"), new Guid("54ee857e-f175-4649-b3f8-5f0c226aed95") },
                    { new Guid("81726d03-bc2d-4db3-a68d-893dd08ea314"), new Guid("179e337f-afa9-44dc-8322-44a76c2ce106"), new Guid("2c1073c4-d98a-4929-8541-77e619e9e48f") },
                    { new Guid("8ec9ad65-d981-4195-b661-8217b204c46c"), new Guid("95c0abd3-6b8b-492c-a395-b9e2bd867415"), new Guid("2c1073c4-d98a-4929-8541-77e619e9e48f") },
                    { new Guid("a7106034-cf06-4990-b6b8-866a325de216"), new Guid("95c0abd3-6b8b-492c-a395-b9e2bd867415"), new Guid("4bbb3600-4df7-4cfc-a9b0-28bb16cfbc37") },
                    { new Guid("b64abd3c-8f26-4629-ac14-c9eda6e1a02b"), new Guid("95c0abd3-6b8b-492c-a395-b9e2bd867415"), new Guid("54ee857e-f175-4649-b3f8-5f0c226aed95") },
                    { new Guid("d0949c45-2d11-498f-9136-38441cc4e1d8"), new Guid("179e337f-afa9-44dc-8322-44a76c2ce106"), new Guid("3440a337-f353-42d7-8227-81a7e2737925") },
                    { new Guid("f5d44f1c-f0f6-4cc7-bddc-dd1c549939ee"), new Guid("a9893ca8-7f18-42cb-8ef0-2c2f8308ecf4"), new Guid("16e596d1-3a51-409f-ab32-56d000fba06c") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblCart_UserId",
                table: "tblCart",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCartItem_CartId",
                table: "tblCartItem",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCartItem_MovieId",
                table: "tblCartItem",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_tblMovie_DirectorId",
                table: "tblMovie",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_tblMovie_FormatId",
                table: "tblMovie",
                column: "FormatId");

            migrationBuilder.CreateIndex(
                name: "IX_tblMovie_RatingId",
                table: "tblMovie",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_tblMovieGenre_GenreId",
                table: "tblMovieGenre",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_tblMovieGenre_MovieId",
                table: "tblMovieGenre",
                column: "MovieId");

            migrationBuilder.Sql(@"CREATE PROCEDURE [dbo].[spGetMovies]
                AS
                select m.Id, m.RatingId, m.DirectorId, m.FormatId, m.Cost, m.Title, m.Description, m.Quantity,
                r.Description RatingDescription,
                f.Description FormatDescription,
                d.FirstName, d.LastName
                from tblMovie m
                inner join tblRating r on m.RatingId = r.Id
                inner join tblFormat f on m.FormatId = f.Id
                inner join tblDirector d on m.DirectorId = d.Id
 
                RETURN 0");

            migrationBuilder.Sql(@"CREATE PROCEDURE [dbo].[spGetMoviesByGenre]
                     @GenreName VARCHAR(20)
                AS
                     select m.Id, m.RatingId, m.DirectorId, m.FormatId, m.Cost, m.Title, m.Description, m.Quantity,
                     r.Description RatingDescription,
                     f.Description FormatDescription,
                     d.FirstName, d.LastName
                     from tblMovie m
                     inner join tblRating r on m.RatingId = r.Id
                     inner join tblFormat f on m.FormatId = f.Id
                     inner join tblDirector d on m.DirectorId = d.Id
                     inner join tblMovieGenre mg on m.Id = mg.MovieId
                     inner join tblGenre g on mg.GenreId = g.Id
                     WHERE g.Description Like '%' + @GenreName + '%'
                RETURN 0
                ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblCartItem");

            migrationBuilder.DropTable(
                name: "tblCustomer");

            migrationBuilder.DropTable(
                name: "tblMovieGenre");

            migrationBuilder.DropTable(
                name: "tblOrder");

            migrationBuilder.DropTable(
                name: "tblOrderItem");

            migrationBuilder.DropTable(
                name: "tblCart");

            migrationBuilder.DropTable(
                name: "tblGenre");

            migrationBuilder.DropTable(
                name: "tblMovie");

            migrationBuilder.DropTable(
                name: "tblUser");

            migrationBuilder.DropTable(
                name: "tblDirector");

            migrationBuilder.DropTable(
                name: "tblFormat");

            migrationBuilder.DropTable(
                name: "tblRating");
        }
    }
}
