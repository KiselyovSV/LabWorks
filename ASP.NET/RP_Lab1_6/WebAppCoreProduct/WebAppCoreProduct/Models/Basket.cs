using System;

namespace WebAppCoreProduct.Models
{
    public class Basket
    {
        internal decimal? TotalPrice { get; set; }
        internal decimal? TotalDiscontPrice { get; set; }

        internal List<Product> products = new List<Product>() { }; 

        public void GetTotalPrice()
        {
            decimal? total = (decimal)0.0;
            if (products?.Count != null)
            {
                foreach (Product prod in products)
                {
                    total += prod.Price;
                }
            }
            this.TotalPrice = total;
        }

        public void GetTotalDiscontPrice()
        {
            decimal? total = (decimal)0.0;
            if (products?.Count != null)
            {
                foreach (Product prod in products)
                {
                    total += prod.DiscontPrice;
                }
            }
            this.TotalDiscontPrice = total;
        }

        public void AddProduct(Product product)
        {
            if(product is not null) products.Add(product);
            else return;
        }

        //public void RemoveProduct(Product product)
        //{
        //    Product? prod = products.FirstOrDefault(p => p.Name == product.Name);
        //    if (prod is not null)products.Remove(prod);
        //}
        public void RemoveProduct(Product product)
        {
            Product? prod = products.Find(p => p.Name == product.Name);
            if (prod is not null) products.Remove(prod);
        }
    }
}
