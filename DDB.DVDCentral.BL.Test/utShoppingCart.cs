using DDB.DVDCentral.BL.Models;
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
            double total = cart.TotalCost;

            Assert.AreEqual(totalcost, total);
        }

        [TestMethod]
        public void CheckoutTest()
        {
            ShoppingCart cart = new ShoppingCart();
            List<Movie> movies = new MovieManager(options).Load();
            cart.Items.Add(movies.FirstOrDefault());
            cart.Items.Add(movies.LastOrDefault());
            cart.CustomerId = new CustomerManager(options).Load().FirstOrDefault().Id;
            cart.UserId = new UserManager(options).Load().FirstOrDefault().Id;
            int actual = new ShoppingCartManager(options).Checkout(cart, true);
            Assert.AreEqual(1, actual);

        }

    }
}
