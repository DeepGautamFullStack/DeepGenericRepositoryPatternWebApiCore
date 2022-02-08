using DeepGenericRepositoryPatternWebApiCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeepGenericRepositoryPatternWebApiCore.DAL
{
   public interface IUnitOfWork
    {
        public GenericRepository<Customer> CustomerRepository{ get; }
        public GenericRepository<Order> OrderRepository { get; }

        public void Save();

    }
}
