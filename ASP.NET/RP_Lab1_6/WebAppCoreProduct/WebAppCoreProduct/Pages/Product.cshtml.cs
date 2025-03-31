using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppCoreProduct.Models;

namespace WebAppCoreProduct.Pages
{
    public class ProductModel : PageModel
    {
        public Product? Product { get; set; }

       
        public string? MessageRezult { get; private set; }
        public void OnPost(string name, decimal? price)
        {
            Product = new Product();
            if (price == null || price < 0 || string.IsNullOrEmpty(name))
            {
                MessageRezult = "�������� ������������ ������. ��������� ����";
                return;
            }
            var result = price * (decimal?)0.18;
            MessageRezult = $"��� ������ {name} � ����� {price} ������ �������� {result}";
            Product.DiscontPrice = result;
            Product.Price = price;
            Product.Name = name;
        }
        public void OnPostDiscont(string name, decimal? price, double discont)
        {
            Product = new Product();
            var result = price - (price * (decimal?)discont / 100);
            MessageRezult = $"��� ������ {name} � ����� {price} � ������� {discont} ��������� �������� ��������� {result}";
            Product.DiscontPrice = result;
            Product.Price = price;
            Product.Name = name;
        }
        public void OnPostAdd(Product product, double discont)
        {
            BasketModel.Basket?.AddProduct(product);
            var result = product.Price - (product.Price * (decimal?)discont / 100);
            product.DiscontPrice = result;
            MessageRezult = $"����� {product.Name} �������� � �������";
            if(BasketModel.Basket is not null)
            {
                BasketModel.Basket.GetTotalPrice();
                BasketModel.Basket.GetTotalDiscontPrice();
                BasketModel.BasketMessageRezult = $"����� ��������� �������:{BasketModel.Basket.TotalPrice}\n " +
                $"� ������ ������ {BasketModel.Basket.TotalDiscontPrice}";
            }
           
        }
    }
}
