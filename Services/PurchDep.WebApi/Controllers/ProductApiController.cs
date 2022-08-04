using Microsoft.AspNetCore.Mvc;
using PurchDep.Dal.Entities;
using PurchDep.Domain.Base;
using PurchDep.Interfaces.Base.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchDep.WebApi.Controllers
{
    [Route("/products")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly Service<Product, IProduct> _service;

        public ProductApiController(Service<Product, IProduct> service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var items = _service.GetAll();

            if (!items.Any()) return NoContent();

            return Ok(items);
        }

        [HttpGet("id:int")]
        public IActionResult GetById(int id)
        {
            IProduct item;
            try
            {
                item = _service.Get(id);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Add([FromBody] IProduct item)
        {
            try
            {
                _service.Add(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(item);
        }

        [HttpPut("id:int")]
        public IActionResult Edit(int id, [FromBody] IProduct itemToEdit)
        {
            IProduct product;
            try
            {
                product = _service.Update(id, itemToEdit);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }

            return Ok(product);
        }

        [HttpDelete("id:int")]
        public IActionResult Delete(int id)
        {
            IProduct item;
            try
            {
                item = _service.Delete(id);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }

            return Ok(item);
        }
    }
}
