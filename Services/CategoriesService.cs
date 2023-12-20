using GamesMvc.Interfaces;

namespace GamesMvc.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly AppDbContext _context;

        public CategoriesService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetAllSelectListItemAsync()
        {
            return await _context.Categories.Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Name })
                .OrderBy(x => x.Text).AsNoTracking().ToListAsync();
        }
    }
}
