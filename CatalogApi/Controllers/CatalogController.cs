using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        private List<Catalog> _catalogs;

        private readonly ILogger<CatalogController> _logger;

        public CatalogController(ILogger<CatalogController> logger)
        {
            GenerateCatalogs();
            _logger = logger;        
        }

        // GET: api/<CatalogController>
        [Authorize]
        [HttpGet]
        public IEnumerable<Catalog> Get()
        {
            var user = User.Claims.First(claim => claim.Type == "userType").Value;
            return _catalogs;
        }

        // GET: api/<CatalogController>/{id}
        [HttpGet("{id}")]
        public Catalog Get(int id)
        {
            return _catalogs.Where(p => p.CatalogId == id).FirstOrDefault();
        }

        // POST: api/<CatalogController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/<CatalogController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/<CatalogController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private void GenerateCatalogs()
        {
            _catalogs = new List<Catalog>();

            _catalogs.Add(new Catalog
            {
                CatalogId = 1,
                Description = "Catálogo de celulares",
                Date = DateTime.Now
            });
            _catalogs.Add(new Catalog
            {
                CatalogId = 2,
                Description = "Catálogo de televisores",
                Date = DateTime.Now
            });
            _catalogs.Add(new Catalog
            {
                CatalogId = 3,
                Description = "Catálogo de laptops",
                Date = DateTime.Now
            });
            _catalogs.Add(new Catalog
            {
                CatalogId = 4,
                Description = "Catálogo de videojuegos",
                Date = DateTime.Now
            });
            _catalogs.Add(new Catalog
            {
                CatalogId = 5,
                Description = "Catálogo de ropas",
                Date = DateTime.Now
            });
        }
    }
}
