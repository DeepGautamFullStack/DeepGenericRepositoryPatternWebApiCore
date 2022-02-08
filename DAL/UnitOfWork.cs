using DeepGenericRepositoryPatternWebApiCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeepGenericRepositoryPatternWebApiCore.DAL
{
    // Unit of work by Deep Gautam
    public class UnitOfWork:IDisposable,IUnitOfWork
    {
        
        private readonly RepositoryDbContext context;
        private GenericRepository<Customer> customerRepository;
        private GenericRepository<Order> orderRepository;

        public UnitOfWork(RepositoryDbContext _context)
        {
            this.context = _context;
        }
        public GenericRepository<Customer> CustomerRepository
        {
            get
            {

                if (this.customerRepository == null)
                {
                    this.customerRepository = new GenericRepository<Customer>(context);
                }
                return customerRepository;
            }
        }

        public GenericRepository<Order> OrderRepository
        {
            get
            {

                if (this.orderRepository == null)
                {
                    this.orderRepository = new GenericRepository<Order>(context);
                }
                return orderRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
