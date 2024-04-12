using Microsoft.AspNetCore.Mvc;
using EntityFramework.Data;
using EntityFramework.Models;

namespace EntityFramework.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TipoAplicacionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipoAplicacionController(ApplicationDbContext context)
        {
                _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<TipoAplicacion> tipos = _context.TipoAplicacion;
            return Ok(tipos);
        }

        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }

            TipoAplicacion? obj = _context.TipoAplicacion.Find(id);
            if(obj == null)
            {
                return NotFound();
            }

            return Ok(obj);
        }

        [HttpPost("create")]
        public IActionResult Create(TipoAplicacion tipoAplicacion)
        {
            if(ModelState.IsValid)
            {
                _context.TipoAplicacion.Add(tipoAplicacion);
                _context.SaveChanges();
                return Ok("created category");
            }

            return StatusCode(500, "cannot create category");
        }

        [HttpPost("update")]
        public IActionResult Update(TipoAplicacion tipoAplicacion)
        {
            if(ModelState.IsValid)
            {
                _context.TipoAplicacion.Update(tipoAplicacion);
                _context.SaveChanges();
                return Ok("updated category");
            }

            return StatusCode(500, "cannot update tipoAplication");
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }

            TipoAplicacion? obj = _context.TipoAplicacion.Find(id);
            if(obj == null)
            {
                return NotFound();
            }

            _context.TipoAplicacion.Remove(obj);
            _context.SaveChanges();
            return Ok("tipoAplicacion deleted");
        }
    }
}
