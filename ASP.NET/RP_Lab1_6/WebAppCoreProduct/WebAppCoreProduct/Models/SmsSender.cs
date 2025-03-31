using WebAppCoreProduct.Pages;

namespace WebAppCoreProduct.Models
{
    public class SmsSender: ISender
    {
        public void Print()
        {
            BasketModel.BasketMessageRezult += " Перечень товаров, находящихся в корзине, отправлен в SMS.";
        }
    }
}
