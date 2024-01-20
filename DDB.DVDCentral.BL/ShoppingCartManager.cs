using DDB.DVDCentral.BL.Models;
using DVDCentral.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DDB.DVDCentral.BL
{
    public static class ShoppingCartManager
    {
        public static void Add(ShoppingCart cart, Movie item)
        {
            if (cart != null) 
            {
                Movie cartMovie = cart.Items.Where(i => i.Id == item.Id).FirstOrDefault();
                if(cartMovie != null)
                {
                    cart.Items.Where(i => i.Id == item.Id).FirstOrDefault().CartQty++;
                }
                else
                {
                    item.CartQty = 1;
                    cart.Items.Add(item);
                } 
            }
        }

        public static void Remove(ShoppingCart cart, Movie item)
        {
            if (cart != null) 
            {
                Movie cartMovie = cart.Items.Where(i => i.Id == item.Id).FirstOrDefault();
                if (cartMovie != null)
                {
                    if(cartMovie.CartQty > 1)
                    {
                        cart.Items.Where(i => i.Id == item.Id).FirstOrDefault().CartQty--;
                    }
                    else
                    {
                        cart.Items.Remove(item);
                    }
                }
                else
                {
                    
                }
            }
        }

        public static string Checkout(ShoppingCart cart, int custId)
        {
            if(cart.TotalItems <= 0) 
            {
                return "Put items in your Shopping Cart before Checking Out";
            }

            // Make a new order
            // Set the Order fields as needed.
            Order order = new Order
            {
                CustomerId = custId,
                OrderDate = DateTime.Now,
                ShipDate = DateTime.Now.AddDays(3),
                UserId = CustomerManager.LoadById(custId).UserId
            };


            // Make a new orderItem
            // Set the OrderItem fields from the item
            // order.OrderItems.Add(orderItem)
            foreach (Movie item in cart.Items)
            {
               
                try
                {
                    Movie inStkMovie = MovieManager.LoadById(item.Id);
                    if (inStkMovie.InStkQty >= item.CartQty)
                    {
                        OrderItem orderItem = new OrderItem
                        {
                            MovieId = item.Id,
                            Quantity = item.CartQty,
                            Cost = item.Cost,
                        };
                        order.OrderItems.Add(orderItem);
                       // inStkMovie.InStkQty -= 1;
                       // MovieManager.Update(inStkMovie);
                    }
                    else
                    {
                        throw new Exception("Not enough " + item.Title + " movies in stock.");
                    }
                    
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                
            }

            OrderManager.Insert(order);

            // Decrement the tblMovie.InStkQty appropriately.

            cart = new ShoppingCart();

            return "Thank You For Your Order";
        }
    }
}
