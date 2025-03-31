using WebAppCoreProduct.Pages;

namespace WebAppCoreProduct.Models
{
    public class MailSender: ISender
    {
        public void Print()
        {
            BasketModel.BasketMessageRezult += " Перечень товаров, находящихся в корзине, отправлен на E-mail.";
        }
    }
}
