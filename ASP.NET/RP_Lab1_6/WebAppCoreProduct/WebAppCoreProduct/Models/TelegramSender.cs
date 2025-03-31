using WebAppCoreProduct.Pages;

namespace WebAppCoreProduct.Models
{
    public class TelegramSender: ISender
    {
        public void Print()
        {
            BasketModel.BasketMessageRezult += " Перечень товаров, находящихся в корзине, отправлен в Telegram.";
        }
    }
}
