using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models
{
    /// <summary>
    /// The Cart class
    /// </summary>
    /// 
    public class Cart : ICart
    {

       
        /// <summary>
        /// Read-only property for dispaly only
        /// </summary>
        public IEnumerable<CartLine> Lines => GetCartLineList();

        List<CartLine> _cart;

        int sequenceOrder;

        public Cart()
        {
            _cart = new List<CartLine>();
           sequenceOrder = 1;
        }
        /// <summary>
        /// Return the actual cartline list
        /// </summary>
        /// <returns></returns>
        private List<CartLine> GetCartLineList()
        {
 
            return _cart;

            // return new List<CartLine>();
        }

        /// <summary>
        /// Adds a product in the cart or increment its quantity in the cart if already added
        /// </summary>//
        public void AddItem(Product product, int quantity)
        {
            /*
                        CartLine newcartLine = new CartLine();

                        newcartLine.OrderLineId = sequenceOrder;
                        newcartLine.Product = product;
                        newcartLine.Quantity = quantity;
                        sequenceOrder++;

                        _cart.Add(newcartLine);
                        */

            int idProductToFind = product.Id;

            bool idFound = false;

            foreach ( var tCartLine in _cart)
            {
                if (  tCartLine.Product.Id == idProductToFind)
                {
                    // fusion
                    tCartLine.Quantity = tCartLine.Quantity + quantity ;
                    idFound = true;

                }
            }

            if (idFound == false)
            {
                CartLine newcartLine = new CartLine();

                newcartLine.OrderLineId = sequenceOrder;
                newcartLine.Product = product;
                newcartLine.Quantity = quantity;
                sequenceOrder++;

                _cart.Add(newcartLine);

            }



        }

        /// <summary>
        /// Removes a product form the cart
        /// </summary>
        public void RemoveLine(Product product) =>
            GetCartLineList().RemoveAll(l => l.Product.Id == product.Id);

        /// <summary>
        /// Get total value of a cart
        /// </summary>
        public double GetTotalValue()
        {
            // TODO implement the method

            double totalValue = 0;


            foreach (var tCartLine in _cart)
            {
                totalValue = totalValue + tCartLine.Product.Price * tCartLine.Quantity;
            }

            return totalValue;
        }

        /// <summary>
        /// Get average value of a cart
        /// </summary>
        public double GetAverageValue()
        {
            // TODO implement the method
            double totalCart =0;
            double totalQuantite = 0;
            double totalAvg = 0;
            foreach (var tCartLine in _cart)
            {
               
                double totalCartLine = tCartLine.Quantity * tCartLine.Product.Price;
                totalCart = totalCart + totalCartLine;

                totalQuantite =totalQuantite + tCartLine.Quantity;             

            }

            if(totalQuantite==0) 
            {
                totalAvg = 0;
            }
            else
            {
                totalAvg = totalCart / totalQuantite;
            }

            // totalAvg = (totalQuantite == 0) ? 0 : totalCart / totalQuantite;
            

            return totalAvg;
        }

        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public Product FindProductInCartLines(int productId)
        {
            // TODO implement the method

            Product productToFind = null;

            foreach(var tCartLine in Lines )
            {
                if(tCartLine.Product.Id == productId)
                {
                    productToFind = tCartLine.Product;
                    
                }    

            }

            return productToFind;
        }

        /// <summary>
        /// Get a specifid cartline by its index
        /// </summary>
        public CartLine GetCartLineByIndex(int index)
        {
            return Lines.ToArray()[index];
        }

        /// <summary>
        /// Clears a the cart of all added products
        /// </summary>
        public void Clear()
        {
            List<CartLine> cartLines = GetCartLineList();
            cartLines.Clear();
        }
    }

    public class CartLine
    {
        public int OrderLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
