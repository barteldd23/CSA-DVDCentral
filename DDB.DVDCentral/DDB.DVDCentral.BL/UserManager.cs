using DDB.DVDCentral.PL;
using DVDCentral.BL.Models;
using Humanizer;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DDB.DVDCentral.BL
{
    public class LoginFailureException : Exception
    {
        public LoginFailureException() : base("Cannot log in with these credentials. Your IP Address has been saved.")
        {

        }
        public LoginFailureException(string message) : base(message)
        {

        }

    }

    public static class UserManager
    {

        public static string GetHash(string password)
        {
            using(var hasher = SHA1.Create())
            {
                var hashbytes = Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(hasher.ComputeHash(hashbytes));
            }
        }

		public static int Insert(User user, bool rollback=false)
		{
            int results;
			using(DVDCentralEntities dc = new DVDCentralEntities())
			{
                IDbContextTransaction transaction = null;
                if (rollback) transaction = dc.Database.BeginTransaction();

                tblUser entity = new tblUser();
                entity.Id = dc.tblUsers.Any() ? dc.tblUsers.Max(s => s.Id) + 1 : 1;  // get last ID in table and add 1, or set Id to 1 because there are no Values in the table.
                entity.FirstName = user.FirstName;
                entity.LastName = user.LastName;
                entity.UserName = user.UserName;
                entity.Password = GetHash(user.Password);


                // IMPORTANT - BACK FILL THE ID
                user.Id = entity.Id; //do this because the first Insert is id by reference


                //Add entity to database
                dc.tblUsers.Add(entity);
                // commit the changes
                results = dc.SaveChanges();

                //only for ut files
                if (rollback) transaction.Rollback();

                return results;
            }
		}
		public static int Update(User user, bool rollback=false) 
		{
            try
            {
                int results;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();
                    tblUser entity = dc.tblUsers.Where(e => e.Id == user.Id).FirstOrDefault();
                    if (entity != null)
                    {
                        entity.FirstName = user.FirstName;
                        entity.LastName = user.LastName;
                        entity.UserName = user.UserName;
                        entity.Password = GetHash(user.Password);

                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();
                        return results;
                    }
                    else
                    {
                        throw new LoginFailureException("User Does not exist.");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            
		}
		public static int Delete(int id, bool rollback = false) 
		{
            try
            {
                int results;
                using(DVDCentralEntities dc =new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if(rollback) transaction = dc.Database.BeginTransaction();

                    tblUser entity = dc.tblUsers.Where(e => e.Id==id).FirstOrDefault();
                    if (entity != null)
                    {
                        dc.tblUsers.Remove(entity);
                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();
                        return results;
                    }
                    else
                    {
                        throw new Exception("User Not Found");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
		}

		public static void Seed()
		{
            try
            {
                using(DVDCentralEntities dc = new DVDCentralEntities())
                {
                        tblUser bfoote = dc.tblUsers.Where(e => e.UserName == "bfoote").FirstOrDefault();
                        tblUser dbartel = dc.tblUsers.Where(e => e.UserName == "dbartel").FirstOrDefault();
                        if (bfoote == null) 
                        {
                            User user1 = new User
                            {
                                Id = 1,
                                FirstName = "Brian",
                                LastName = "Foote",
                                UserName = "bfoote",
                                Password = "maple"
                            };

                            Insert(user1);
                        }
                        
                        if(dbartel == null)
                        {
                            User user2 = new User
                            {
                                Id = 2,
                                FirstName = "Dean",
                                LastName = "Bartel",
                                UserName = "dbartel",
                                Password = "password"
                            };

                            Insert(user2);
                        }
                    
                }
            }
            catch (Exception)
            {

                throw;
            }
		}
        public static bool Login(User user)
        {
			try
			{
                if(!string.IsNullOrEmpty(user.UserName))
                {
                    if(!string.IsNullOrEmpty(user.Password))
                    {
                        using(DVDCentralEntities dc = new DVDCentralEntities())
                        {
                            tblUser tblUser = dc.tblUsers.Where(e => e.UserName == user.UserName).FirstOrDefault();
                            if(tblUser != null)
                            {
                                if(tblUser.Password == GetHash(user.Password))
                                {
                                    // Login successful
                                    //back fill user datat to the 'user' object that will be the session variable
                                    user.Id = tblUser.Id;
                                    user.FirstName = tblUser.FirstName;
                                    user.LastName = tblUser.LastName;
                                    return true;
                                }
                                else
                                {
                                    throw new LoginFailureException();
                                }
                            }
                            else
                            {
                                throw new LoginFailureException("User Id was not found.");
                            }
                        }
                    }
                    else
                    {
                        throw new LoginFailureException("Password was not entered");
                    }
                }
                else
                {
                    throw new LoginFailureException("User Id was not set.");
                }
			}
			catch (Exception)
			{

				throw;
			}
        }

        public static List<User> Load()
        {
            try
            {
                List<User> list = new List<User>();

                using(DVDCentralEntities dc = new DVDCentralEntities())
                {
                    (from e in dc.tblUsers
                     select new
                     {
                         e.Id,
                         e.UserName,
                         e.FirstName, 
                         e.LastName,
                         e.Password,
                     }).ToList()
                     .ForEach (user => list.Add(new User
                     {
                         Id = user.Id,
                         UserName = user.UserName,
                         FirstName = user.FirstName,
                         LastName = user.LastName,
                         Password = user.Password
                     }));
                }

                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static User LoadById(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblUser entity = dc.tblUsers.Where(e => e.Id == id).FirstOrDefault();

                    if (entity != null)
                    {
                        return new User
                        {
                            Id = entity.Id,
                            FirstName = entity.FirstName,
                            LastName = entity.LastName,
                            Password = entity.Password,
                            UserName= entity.UserName
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
    }
}
