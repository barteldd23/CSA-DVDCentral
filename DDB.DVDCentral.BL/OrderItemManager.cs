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
    public static class OrderItemManager
    {
        public static int Insert(OrderItem orderItem,
                             bool rollback = false)
        {
            int results = 0;

            try
            {
                using DVDCentralEntities dc = new DVDCentralEntities();
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblOrderItem entity = new tblOrderItem();
                    entity.Id = dc.tblOrderItems.Any() ? dc.tblOrderItems.Max(e => e.Id) + 1 : 1;
                    entity.OrderId = orderItem.OrderId;
                    entity.MovieId = orderItem.MovieId;
                    entity.Quantity = orderItem.Quantity;
                    entity.Cost = orderItem.Cost;

                    orderItem.Id = entity.Id;

                    dc.tblOrderItems.Add(entity);
                    Movie movie = MovieManager.LoadById(orderItem.MovieId);
                    movie.InStkQty -= orderItem.Quantity;
                    MovieManager.Update(movie, rollback);
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

        public static int Update(OrderItem orderItem,
                                 bool rollback=false)
        {
            int results = 0;
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();
                    tblOrderItem entity = dc.tblOrderItems.Where(e => e.Id == orderItem.Id).FirstOrDefault();
                    if (entity != null)
                    {
                        entity.OrderId = orderItem.OrderId;
                        entity.MovieId = orderItem.MovieId;
                        entity.Quantity = orderItem.Quantity;
                        entity.Cost = orderItem.Cost;

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

                    tblOrderItem entity = dc.tblOrderItems.Where(e =>e.Id == id).FirstOrDefault();
                    if (entity != null)
                    {
                        dc.tblOrderItems.Remove(entity);
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

        public static OrderItem LoadById(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblOrderItem entity = dc.tblOrderItems.Where(e => e.Id == id).FirstOrDefault();
                    if (entity != null)
                    {
                        OrderItem orderItem = new OrderItem
                        {
                            Id = entity.Id,
                            OrderId = entity.OrderId,
                            MovieId = entity.MovieId,
                            Quantity = entity.Quantity,
                            Cost = entity.Cost
                        };

                        return orderItem;
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

        public static List<OrderItem> Load()
        {
            List<OrderItem> list = new List<OrderItem>();

            using (DVDCentralEntities dc = new DVDCentralEntities())
            {
                (from e in dc.tblOrderItems
                 join m in dc.tblMovies on e.MovieId equals m.Id
                 select new
                 {
                     e.Id,
                     e.OrderId,
                     e.MovieId,
                     e.Quantity,
                     e.Cost,
                     m.Title,
                     m.ImagePath
                 }).ToList()
                 .ForEach( orderItem => list.Add(new OrderItem
                 {
                     Id = orderItem.Id,
                     OrderId = orderItem.OrderId,
                     MovieId =orderItem.MovieId,
                     Quantity = orderItem.Quantity,
                     Cost = orderItem.Cost,
                     MovieTitle = orderItem.Title,
                     MovieImagePath = orderItem.ImagePath
                 }));
            }
            return list;
        }

        public static List<OrderItem> LoadByOrderId(int orderId)
        {
            // I think its finished 15Oct

            List<OrderItem> list = new List<OrderItem>();

            using (DVDCentralEntities dc = new DVDCentralEntities())
            {
                (from e in dc.tblOrderItems
                 join m in dc.tblMovies on e.MovieId equals m.Id
                 where e.OrderId == orderId
                 select new
                 {
                     e.Id,
                     e.OrderId,
                     e.MovieId,
                     e.Quantity,
                     e.Cost,
                     m.Title,
                     m.ImagePath
                 }).ToList()
                 .ForEach(orderItem => list.Add(new OrderItem
                 {
                     Id = orderItem.Id,
                     OrderId = orderItem.OrderId,
                     MovieId = orderItem.MovieId,
                     Quantity = orderItem.Quantity,
                     Cost = orderItem.Cost,
                     MovieTitle = orderItem.Title,
                     MovieImagePath = orderItem.ImagePath
                 }));
            }
  
            return list;
        }


    }
}
