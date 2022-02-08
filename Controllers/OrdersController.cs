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
    public class OrdersController : ControllerBase
    {
        //This is beauty of unit of work.
        private readonly IUnitOfWork unitOfWork;

        public OrdersController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        // GET: api/Orders
        [HttpGet]
        public IEnumerable<Order> GetOrder()
        {
            return unitOfWork.OrderRepository.GetEntity();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public Order GetOrder(int id)
        {
            var order = unitOfWork.OrderRepository.GetEntityById(id);

            if (order == null)
            {
                return null;
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public  Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {

                return null;
            }


            try
            {
                unitOfWork.OrderRepository.UpdateEntity(order);
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }

            return null;
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public  ActionResult<Order> PostOrder(Order order)
        {
            unitOfWork.OrderRepository.InsertEntity(order);
            unitOfWork.Save();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public  Task<IActionResult> DeleteOrder(int id)
        {
            var customer = unitOfWork.OrderRepository.GetEntityById(id);
            unitOfWork.CustomerRepository.DeleteEntity(id);
            if (customer == null)
            {
                return null;
            }

            return null;
        }
    }
}
