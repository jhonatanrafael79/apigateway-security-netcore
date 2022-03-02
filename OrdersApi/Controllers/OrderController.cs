using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private List<Order> _orders;

        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            GenerateOrders();
            _logger = logger;
        }

        // GET: api/<OrderController>
        [Authorize]
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            var user = User.Claims.First(claim => claim.Type == "userType").Value;
            return _orders;
        }

        // GET: api/<OrderController>/{id}
        [HttpGet("{id}")]
        public Order Get(int id)
        {
            return _orders.Where(p => p.OrderId == id).FirstOrDefault();
        }

        // POST: api/<OrderController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private void GenerateOrders()
        {
            _orders = new List<Order>();

            _orders.Add(new Order
            {
                OrderId = 1,
                Amount = 500,
                Date = DateTime.Now
            });
            _orders.Add(new Order
            {
                OrderId = 2,
                Amount = 100,
                Date = DateTime.Now
            });
            _orders.Add(new Order
            {
                OrderId = 3,
                Amount = 1200,
                Date = DateTime.Now
            });
            _orders.Add(new Order
            {
                OrderId = 4,
                Amount = 5500,
                Date = DateTime.Now
            });
            _orders.Add(new Order
            {
                OrderId = 5,
                Amount = 7000,
                Date = DateTime.Now
            });
        }
    }
}
