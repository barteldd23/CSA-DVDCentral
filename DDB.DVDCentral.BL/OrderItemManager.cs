namespace DDB.DVDCentral.BL
{
    public class OrderItemManager : GenericManager<tblOrderItem>
    {
        public OrderItemManager(DbContextOptions<DVDCentralEntities> options) : base(options)
        {

        }
        public int Insert(OrderItem orderItem, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities(options))
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();
                    tblOrderItem row = new tblOrderItem();

                    row.Id = Guid.NewGuid();
                    row.OrderId = orderItem.OrderId;
                    row.MovieId = orderItem.MovieId;
                    row.Quantity = orderItem.Quantity;
                    row.Cost = orderItem.Cost;

                    orderItem.Id = row.Id;

                    dc.tblOrderItems.Add(row);

                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();
                }

                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Update(OrderItem orderItem, bool rollback = false)
        {
            try
            {
                int results;
                using (DVDCentralEntities dc = new DVDCentralEntities(options))
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblOrderItem row = dc.tblOrderItems.FirstOrDefault(oi => oi.Id == orderItem.Id);

                    if (row != null)
                    {
                        row.OrderId = orderItem.OrderId;
                        row.MovieId = orderItem.MovieId;
                        row.Quantity = orderItem.Quantity;
                        row.Cost = orderItem.Cost;

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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Delete(Guid id, bool rollback = false)
        {
            try
            {
                int results;
                using (DVDCentralEntities dc = new DVDCentralEntities(options))
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblOrderItem row = dc.tblOrderItems.FirstOrDefault(oi => oi.Id == id);

                    if (row != null)
                    {
                        dc.tblOrderItems.Remove(row);
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<OrderItem> Load()
        {
            try
            {
                List<OrderItem> rows = new List<OrderItem>();
                using (DVDCentralEntities dc = new DVDCentralEntities(options))
                {
                    dc.tblOrderItems
                        .ToList()
                        .ForEach(oi => rows.Add(
                            new OrderItem
                            {
                                Id = oi.Id,
                                OrderId = oi.OrderId,
                                MovieId = oi.MovieId,
                                Quantity = oi.Quantity,
                                Cost = oi.Cost
                            }));

                    return rows;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public OrderItem LoadById(Guid id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities(options))
                {
                    tblOrderItem row = dc.tblOrderItems.FirstOrDefault(oi => oi.Id == id);

                    if (row != null)
                    {
                        OrderItem orderItem = new OrderItem
                        {
                            Id = row.Id,
                            OrderId = row.OrderId,
                            MovieId = row.MovieId,
                            Quantity = row.Quantity,
                            Cost = row.Cost
                        };
                        return orderItem;
                    }
                    else
                    {
                        throw new Exception("Row was not found.");
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<OrderItem> LoadByOrderId(Guid orderId)
        {
            try
            {
                List<OrderItem> rows = new List<OrderItem>();
                using (DVDCentralEntities dc = new DVDCentralEntities(options))
                {
                    var results = (from oi in dc.tblOrderItems
                                   join m in dc.tblMovies on oi.MovieId equals m.Id
                                   where oi.OrderId == orderId
                                   select new
                                   {
                                       Id = oi.Id,
                                       OrderId = oi.OrderId,
                                       MovieId = oi.MovieId,
                                       Quantity = oi.Quantity,
                                       Cost = oi.Cost,
                                       MovieTitle = m.Title,
                                       ImagePath = m.ImagePath
                                   }).ToList();

                    results.ForEach(r => rows.Add(
                         new OrderItem
                         {
                             Id = r.Id,
                             OrderId = r.OrderId,
                             MovieId = r.MovieId,
                             Quantity = r.Quantity,
                             Cost = r.Cost,
                             MovieTitle = r.MovieTitle,
                             MovieImagePath = r.ImagePath
                         }
                        ));

                    return rows;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }
}
