using DDB.DVDCentral.PL;
using DVDCentral.BL.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDB.DVDCentral.BL
{
    public static class CustomerManager
    {
        public static int Insert(Customer customer,
                             bool rollback = false)
        {
            int results = 0;

            try
            {
                using DVDCentralEntities dc = new DVDCentralEntities();
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblCustomer entity = new tblCustomer();
                    entity.Id = dc.tblCustomers.Any() ? dc.tblCustomers.Max(e => e.Id) + 1 : 1;
                    entity.FirstName = customer.FirstName;
                    entity.LastName = customer.LastName;
                    entity.Address = customer.Address;
                    entity.City = customer.City;
                    entity.State = customer.State;
                    entity.ZIP = customer.ZIP;
                    entity.Phone = customer.Phone;
                    entity.UserId = customer.UserId;

                    customer.Id = entity.Id;

                    dc.tblCustomers.Add(entity);
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

        public static int Update(Customer customer,
                                 bool rollback=false)
        {
            int results = 0;
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();
                    tblCustomer entity = dc.tblCustomers.Where(e => e.Id == customer.Id).FirstOrDefault();
                    if (entity != null)
                    {
                        entity.FirstName = customer.FirstName;
                        entity.LastName = customer.LastName;
                        entity.Address = customer.Address;
                        entity.City = customer.City;
                        entity.State = customer.State;
                        entity.ZIP = customer.ZIP;
                        entity.Phone = customer.Phone;
                        entity.UserId = customer.UserId;

                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row Does Not Exist.");
                    }

                }

                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Delete(int id,
                                 bool rollback = false)
        {
            int results = 0;
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if(rollback) transaction = dc.Database.BeginTransaction();

                    tblCustomer entity = dc.tblCustomers.Where(e =>e.Id == id).FirstOrDefault();
                    if (entity != null)
                    {
                        dc.tblCustomers.Remove(entity);
                        results = dc.SaveChanges();
                        
                    }
                    else
                    {
                        throw new Exception("Row Does Not Exist.");
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

        public static Customer LoadById(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblCustomer entity = dc.tblCustomers.Where(e => e.Id == id).FirstOrDefault();
                    if (entity != null)
                    {
                        Customer customer = new Customer
                        {
                            Id = entity.Id,
                            FirstName = entity.FirstName,
                            LastName = entity.LastName,
                            Address = entity.Address,
                            City = entity.City,
                            State = entity.State,
                            ZIP = entity.ZIP,
                            Phone = entity.Phone,
                            UserId = entity.UserId
                        };

                        return customer;
                    }
                    else
                    {
                        throw new Exception("Row Does Not Exist");
                    }
                }
                
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static List<Customer> Load()
        {
            List<Customer> list = new List<Customer>();

            using (DVDCentralEntities dc = new DVDCentralEntities())
            {
                (from e in dc.tblCustomers
                 select new
                 {
                     e.Id,
                     e.FirstName,
                     e.LastName,
                     e.Address,
                     e.City,
                     e.State,
                     e.ZIP,
                     e.Phone,
                     e.UserId
                 }).ToList()
                 .ForEach( customer => list.Add(new Customer
                 {
                     Id = customer.Id,
                     FirstName = customer.FirstName,
                     LastName = customer.LastName,
                     Address = customer.Address,
                     City = customer.City,
                     State = customer.State,
                     ZIP = customer.ZIP,
                     Phone = customer.Phone,
                     UserId = customer.UserId
                 }));
            }
            return list;
        }

        // Overload to return all customers associated with this userId
        public static List<Customer> Load(int userId)
        {
            List<Customer> list = new List<Customer>();

            using (DVDCentralEntities dc = new DVDCentralEntities())
            {
                (from e in dc.tblCustomers where e.UserId == userId
                 select new
                 {
                     e.Id,
                     e.FirstName,
                     e.LastName,
                     e.Address,
                     e.City,
                     e.State,
                     e.ZIP,
                     e.Phone,
                     e.UserId
                 }).ToList()
                 .ForEach(customer => list.Add(new Customer
                 {
                     Id = customer.Id,
                     FirstName = customer.FirstName,
                     LastName = customer.LastName,
                     Address = customer.Address,
                     City = customer.City,
                     State = customer.State,
                     ZIP = customer.ZIP,
                     Phone = customer.Phone,
                     UserId = customer.UserId
                 }));
            }
            return list;
        }


    }
}
