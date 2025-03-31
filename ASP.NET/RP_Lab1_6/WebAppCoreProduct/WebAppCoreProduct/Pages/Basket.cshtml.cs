using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppCoreProduct.Models;

namespace WebAppCoreProduct.Pages
{
    public class BasketModel : PageModel
    {
        public static Basket? Basket { get; set; } = new Basket();
        public static string? BasketMessageRezult { get; internal set; } = "������� �����";
              
         
        public void OnPostRemove(Product product)
        {
            if (Basket is not null)
            {
                Basket.RemoveProduct(product);
                Basket.GetTotalPrice();
                Basket.GetTotalDiscontPrice();
                BasketModel.BasketMessageRezult = $"����� {product.Name} ������ �� �������. ����� ��������� �������:{BasketModel.Basket.TotalPrice}.\n " +
                $"� ������ ������: {BasketModel.Basket.TotalDiscontPrice}.";
                if (Basket.products.Count == 0) BasketMessageRezult = $"������� �����";
               
                
            }
            else return;
        }
        public void OnPostPrint()
        {
            var param = Request.Form["sender"];
            switch (param)
            {
                case "EMail": BasketModel.PrintBasketMessage(new MailSender());
                    break;
                case "Telegram": BasketModel.PrintBasketMessage(new TelegramSender());
                    break;
                case "SMS": BasketModel.PrintBasketMessage(new SmsSender());
                    break;
            }
        }

        private static void PrintBasketMessage(ISender printer)
        {
            printer.Print();
        }

        
    }
}

