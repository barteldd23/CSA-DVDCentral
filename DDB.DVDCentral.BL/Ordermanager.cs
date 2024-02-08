namespace DDB.DVDCentral.BL
{
    public class OrderManager : GenericManager<tblOrder>
    {
        public OrderManager(DbContextOptions<DVDCentralEntities> options) : base(options)
        {

        }
        public List<Order> Load(Guid? customerId = null)
        {
            try
            {
                List<Order> orders = new List<Order>();

                using (DVDCentralEntities dc = new DVDCentralEntities(options))
                {
                    var results = (from o in dc.tblOrders
                                   join c in dc.tblCustomers on o.CustomerId equals c.Id
                                   join u in dc.tblUsers on o.UserId equals u.Id
                                   where o.CustomerId == customerId || customerId == null
                                   select new
                                   {
                                       Id = o.Id,
                                       CustomerId = o.CustomerId,
                                       CustomerFisrtName = c.FirstName,
                                       CustomerLastName = c.LastName,
                                       UserName = u.UserName,
                                       OrderDate = o.OrderDate,
                                       UserId = o.UserId,
                                       UserFirstName = u.FirstName,
                                       UserLastName = u.LastName,
                                       ShipDate = o.ShipDate
                                   }
                                  ).ToList();

                    results.ForEach(o => orders.Add(new Order
                    {
                        Id = o.Id,
                        CustomerId = o.CustomerId,
                        FirstName = o.CustomerFisrtName,
                        LastName = o.CustomerLastName,
                        OrderDate = o.OrderDate,
                        UserId = o.UserId,
                        UserName = o.UserName,
                        ShipDate = o.ShipDate
                    }));

                }

                foreach (Order order in orders)
                {
                    order.OrderItems = new OrderItemManager(options).LoadByOrderId(order.Id);
                }

                return orders;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Order LoadById(Guid id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities(options))
                {
                    var row = (from o in dc.tblOrders
                               join c in dc.tblCustomers on o.CustomerId equals c.Id
                               join u in dc.tblUsers on o.UserId equals u.Id
                               where o.Id == id
                               select new
                               {
                                   Id = o.Id,
                                   CustomerId = o.CustomerId,
                                   CustomerFisrtName = c.FirstName,
                                   CustomerLastName = c.LastName,
                                   UserName = u.UserName,
                                   OrderDate = o.OrderDate,
                                   UserId = o.UserId,
                                   UserFirstName = u.FirstName,
                                   UserLastName = u.LastName,
                                   ShipDate = o.ShipDate
                               }).FirstOrDefault();

                    if (row != null)
                    {
                        Order order = new Order
                        {
                            Id = row.Id,
                            CustomerId = row.CustomerId,
                            FirstName = row.CustomerFisrtName,
                            LastName = row.CustomerLastName,
                            UserName = row.UserName,
                            OrderDate = row.OrderDate,
                            UserId = row.UserId,
                            ShipDate = row.ShipDate,
                            OrderItems = new OrderItemManager(options).LoadByOrderId(row.Id)
                        };

                        return order;
                    }
                    else
                    {
                        throw new Exception("Row not found");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Order> LoadByCustomerId(Guid customerId)
        {
            try
            {
                return Load(customerId);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public int Insert(Order order, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities(options))
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblOrder newRow = new tblOrder();
                    // Teranary operator
                    newRow.Id = Guid.NewGuid();
                    newRow.CustomerId = order.CustomerId;
                    newRow.OrderDate = DateTime.Now;
                    newRow.UserId = order.UserId;
                    newRow.ShipDate = newRow.OrderDate.AddDays(3);

                    // Insert the row
                    dc.tblOrders.Add(newRow);

                    // save order items ....
                    foreach (OrderItem item in order.OrderItems)
                    {
                        item.OrderId = newRow.Id;
                        //results += new OrderItemManager(options).Insert(item, rollback);
                        tblOrderItem row = new tblOrderItem();

                        row.Id = Guid.NewGuid();
                        row.OrderId = item.OrderId;
                        row.MovieId = item.MovieId;
                        row.Quantity = item.Quantity;
                        row.Cost = item.Cost;

                        item.Id = row.Id;

                        dc.tblOrderItems.Add(row);
                    }

                    // Backfill the id on the input parameter order
                    order.Id = newRow.Id;
                    // Commit the changes and get the number of rows affected
                    results += dc.SaveChanges();

                    if (rollback) transaction.Rollback();
                }
                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public int Update(Order order, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities(options))
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblOrder upDateRow = dc.tblOrders.FirstOrDefault(r => r.Id == order.Id);

                    if (upDateRow != null)
                    {
                        upDateRow.CustomerId = order.CustomerId;
                        upDateRow.OrderDate = order.OrderDate;
                        upDateRow.UserId = order.UserId;
                        upDateRow.ShipDate = order.ShipDate;

                        dc.tblOrders.Update(upDateRow);

                        // Commit the changes and get the number of rows affected
                        results = dc.SaveChanges();

                        if (rollback) transaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row was not found.");
                    }
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
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblOrder deleteRow = dc.tblOrders.FirstOrDefault(r => r.Id == id);

                    if (deleteRow != null)
                    {
                        dc.tblOrders.Remove(deleteRow);

                        var deleteOrderItems = dc.tblOrderItems.Where(r => r.OrderId == id);

                        dc.tblOrderItems.RemoveRange(deleteOrderItems);

                        // Commit the changes and get the number of rows affected
                        results = dc.SaveChanges();

                        if (rollback) transaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row was not found.");
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
