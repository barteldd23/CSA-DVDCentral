namespace DDB.DVDCentral.BL
{
    public class CustomerManager : GenericManager<tblCustomer>
    {
        public CustomerManager(DbContextOptions<DVDCentralEntities> options) : base(options)
        {

        }

        public int Insert(Customer Customer, bool rollback = false)
        {
            try
            {
                try
                {
                    tblCustomer row = new tblCustomer();
                    row.Id = Guid.NewGuid();
                    row.FirstName = Customer.FirstName;
                    row.LastName = Customer.LastName;
                    row.Address = Customer.Address;
                    row.City = Customer.City;
                    row.State = Customer.State;
                    row.ZIP = Customer.ZIP;
                    row.Phone = Customer.Phone;
                    row.UserId = Customer.UserId;
                    return base.Insert(row, rollback);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Update(Customer customer, bool rollback = false)
        {
            try
            {
                try
                {
                    return base.Update(new tblCustomer
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
                    }, rollback);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

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
                return base.Delete(id, rollback);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Customer> Load()
        {
            try
            {
                List<Customer> rows = new List<Customer>();
                base.Load()
                .ForEach(c => rows.Add(
                    new Customer
                    {
                        Id = c.Id,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        Address = c.Address,
                        City = c.City,
                        State = c.State,
                        ZIP = c.ZIP,
                        Phone = c.Phone,
                        UserId = c.UserId
                    }));
                return rows;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Customer LoadById(Guid id)
        {
            try
            {
                tblCustomer row = base.LoadById(id);

                if (row != null)
                {
                    Customer customer = new Customer
                    {
                        Id = row.Id,
                        FirstName = row.FirstName,
                        LastName = row.LastName,
                        Address = row.Address,
                        City = row.City,
                        State = row.State,
                        ZIP = row.ZIP,
                        Phone = row.Phone,
                        UserId = row.UserId
                    };
                    return customer;
                }
                else
                {
                    throw new Exception("Row was not found.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Customer LoadByUserId(Guid userId)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities(options))
                {

                    var row = (from c in dc.tblCustomers
                               where c.UserId == userId
                               orderby c.Id descending
                               select c).FirstOrDefault();

                    var customer = new Customer();
                    if (row != null)
                    {
                        customer.Id = row.Id;
                        customer.FirstName = row.FirstName;
                        customer.LastName = row.LastName;
                        customer.Address = row.Address;
                        customer.City = row.City;
                        customer.State = row.State;
                        customer.ZIP = row.ZIP;
                        customer.Phone = row.Phone;
                        customer.UserId = row.UserId;

                        return customer;
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


    }
}
