using GamesMvc.Interfaces;

namespace GamesMvc.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _webHostEnvironment;
        public UnitOfWork(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public ICategoriesService CategoriesService => new CategoriesService(_context);

        public IDevicesService DevicesService => new DevicesService(_context);

        public IGamesService GamesService => new GamesService(_context, _webHostEnvironment);

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
