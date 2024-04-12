using EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Data.Repository
{
    public class ProductoRepository : Repository<Producto>, IProductoRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly DbSet<Producto> _dbSet;

        public ProductoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
            _dbSet = _context.Set<Producto>();
        }

        public override IEnumerable<Producto> GetAll()
        {
            return _context.Producto
                .Include(categoria => categoria.categoria)
                .Include(tipo => tipo.tipoAplicacion);
        }
    }
}
