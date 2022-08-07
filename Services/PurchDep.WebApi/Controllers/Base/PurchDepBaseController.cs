using Microsoft.AspNetCore.Mvc;
using PurchDep.Interfaces.Base.Services;

namespace PurchDep.WebApi.Controllers.Base
{
    public abstract class PurchDepBaseController<TSource, TResult> : ControllerBase where TSource : class where TResult : class
    {
        public Service<TSource, TResult> Service { get; }
        protected PurchDepBaseController(Service<TSource, TResult> service)
        {
            Service = service;
        }

        [HttpGet]
        public virtual IActionResult GetAll()
        {
            var items = Service.GetAll();

            if (!items.Any()) return NoContent();

            return Ok(items);
        }

        [HttpGet("id")]
        public virtual IActionResult GetById(int id)
        {
            TResult item;
            try
            {
                item = Service.Get(id);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            return Ok(item);
        }

        [HttpPost]
        public virtual IActionResult Add([FromBody] TResult item)
        {
            try
            {
                Service.Add(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(item);
        }

        [HttpPut("id")]
        public virtual IActionResult Edit(int id, [FromBody] TResult itemToEdit)
        {
            TResult item;
            try
            {
                item = Service.Update(id, itemToEdit);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }

            return Ok(item);
        }

        [HttpDelete("id")]
        public virtual IActionResult Delete(int id)
        {
            TResult item;
            try
            {
                item = Service.Delete(id);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }

            return Ok(item);
        }
    }
}
