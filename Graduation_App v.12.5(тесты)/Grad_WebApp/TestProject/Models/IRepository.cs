using Grad_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Models
{
    internal interface IRepository
    {
        public interface IRepository
        {
            IEnumerable<Client> GetAll();
            Client Get(int id);
            void Create(Client client);
        }
    }
}
  