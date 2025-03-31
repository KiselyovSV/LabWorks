using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppCoreProduct.Models;

namespace WebAppCoreProduct.Pages
{
    public class BasketModel : PageModel
    {
        public static Basket? Basket { get; set; } = new Basket();
        public static string? BasketMessageRezult { get; internal set; } = "Корзина пуста";
              
         
        public void OnPostRemove(Product product)
        {
            if (Basket is not null)
            {
                Basket.RemoveProduct(product);
                Basket.GetTotalPrice();
                Basket.GetTotalDiscontPrice();
                BasketModel.BasketMessageRezult = $"Товар {product.Name} удален из корзины. Общая стоимость товаров:{BasketModel.Basket.TotalPrice}\n " +
                $"С учетом скидки {BasketModel.Basket.TotalDiscontPrice}";
                if (Basket.products.Count == 0) BasketMessageRezult = $"Корзина пуста";
            }
            else return;
        }

        
    }
}

