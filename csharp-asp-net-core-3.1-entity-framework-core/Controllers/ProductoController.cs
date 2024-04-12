using EntityFramework.Data.Repository;
using EntityFramework.Models;
using Microsoft.AspNetCore.Mvc;

namespace EntityFramework.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepository _repository;

        public ProductoController(IProductoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpPost("/upsert")]
        public IActionResult Upsert(Producto producto)
        {
            var productoExistente = _repository.GetById(producto.Id);

            if(productoExistente == null)
            {
                // Create
                _repository.Add(producto);
                return Ok("producto created");
            } else
            {
                // Update
                _repository.Update(producto);
                return Ok("producto updated");
            }
        }

        [HttpGet("/delete/{id}")]
        public IActionResult Delete(int id)
        {
            var productoExistente = _repository.GetById(id);

            if( productoExistente != null)
            {
                _repository.Delete(productoExistente);
                return Ok("producto removed");
            }

            return NotFound();
        }
    }
}
