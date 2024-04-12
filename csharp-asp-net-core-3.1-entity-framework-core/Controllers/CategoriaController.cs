using Microsoft.AspNetCore.Mvc;
using EntityFramework.Data;
using EntityFramework.Models;
using EntityFramework.Data.Repository;

namespace EntityFramework.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class CategoriaController : Controller
    {
        private readonly IRepository<Categoria> _categoriaRepository;

        public CategoriaController(IRepository<Categoria> categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_categoriaRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Categoria? obj = _categoriaRepository.GetById(id);
            if (obj == null)
            {
                return NotFound();
            }

            return Ok(obj);
        }

        [HttpPost("create")]
        public IActionResult Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _categoriaRepository.Add(categoria);
                return Ok("created category");
            }

            return StatusCode(500, "cannot create category");
        }

        [HttpPost("update")]
        public IActionResult Update(Categoria categoria)
        {
            if(ModelState.IsValid)
            {
                _categoriaRepository.Update(categoria);
                return Ok("updated category");
            }

            return StatusCode(500, "cannot update category");
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Categoria? obj = _categoriaRepository.GetById(id);
            if (obj == null)
            {
                return NotFound();
            }

            _categoriaRepository.Delete(obj);
            return Ok("category deleted");
        }

    }
}
