using DDB.DVDCentral.BL.Models;
using DVDCentral.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDB.DVDCentral.BL.Test
{
    [TestClass]
    public class utShoppingCart : utBase
    {
        [TestMethod]
        public void ShoppingCartCostTest()
        {
            ShoppingCart cart = new ShoppingCart();
            List<Movie> movies = new MovieManager(options).Load();
            Movie movie1 = movies.FirstOrDefault();
            Movie movie2 = movies.LastOrDefault();

            cart.Items.Add(movie1);
            cart.Items.Add(movie2);

            double totalcost = movie1.Cost + movie2.Cost;
            double total = cart.Total;

            Assert.AreEqual(totalcost, total);
        }

        [TestMethod]
        public void CheckoutTest()
        {
            ShoppingCart cart = new ShoppingCart();
            List<Movie> movies = new MovieManager(options).Load();
            Movie movie1 = movies.FirstOrDefault();
            Movie movie2 = movies.LastOrDefault();

            cart.Items.Add(movie1);
            cart.Items.Add(movie2);
            Guid userId = Guid.NewGuid();

            new ShoppingCartManager(options).Checkout(cart,userId);

            Order order = new OrderManager(options).Load().LastOrDefault();
            Assert.AreEqual(cart.Total, order.OrderItems.Sum(oi => oi.Cost));

        }

    }
}
