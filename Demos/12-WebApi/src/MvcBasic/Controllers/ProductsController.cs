using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MvcBasic.Models;
using MvcBasic.Repositories;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MvcBasic.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _productsRepository.GetProductsAsync();

            // Don't do this:
            //return products;
            //return new ObjectResult(products);

            // Do this instead
            return Ok(products);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productsRepository.GetProductAsync(id);
            return Ok(product);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Product product)
        {
            product = await _productsRepository.CreateProductAsync(product);
            return CreatedAtAction("Get", new {id = product.Id}, product);
        }

        // PUT api/values/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]Product product)
        {
            product = await _productsRepository.UpdateProductAsync(product);
            return Ok(product);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productsRepository.DeleteAsync(id);
            return new NoContentResult();
        }
    }
}
