using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeepGenericRepositoryPatternWebApiCore.Models;
using DeepGenericRepositoryPatternWebApiCore.DAL;

namespace DeepGenericRepositoryPatternWebApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {

        private readonly IUnitOfWork unitOfWork;

        public CustomersController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        // GET: api/Customers
        [HttpGet]
        public  IEnumerable<Customer> GetCustomer()
        {
            return  unitOfWork.CustomerRepository.GetEntity();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public Customer GetCustomer(int id)
        {
            var customer = unitOfWork.CustomerRepository.GetEntityById(id);

            if (customer == null)
            {
                return null;
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public  Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return null;
            }


            try
            {
                unitOfWork.CustomerRepository.UpdateEntity(customer);
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                    return null;
            }

            return null;
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public  ActionResult<Customer> PostCustomer(Customer customer)
        {
            unitOfWork.CustomerRepository.InsertEntity(customer);
            unitOfWork.Save();

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public  Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = unitOfWork.CustomerRepository.GetEntityById(id);
            unitOfWork.CustomerRepository.DeleteEntity(id);
            if (customer == null)
            {
                return null;
            }

            return null;
        }

       
    }
}
