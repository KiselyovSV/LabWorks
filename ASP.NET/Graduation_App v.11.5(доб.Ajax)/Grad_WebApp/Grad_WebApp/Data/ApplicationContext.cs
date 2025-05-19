using Grad_WebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Grad_WebApp.Data
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
                    : base(options)
        {
            Database.EnsureCreated();
        }
    }

    /* Выполните следующую команду в консоли диспетчер пакетов(PMC) :
         Update-Database -Context 'ApplicationContext' */
}
