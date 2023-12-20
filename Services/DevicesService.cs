using GamesMvc.Interfaces;

namespace GamesMvc.Services
{
    public class DevicesService : IDevicesService
    {
        private readonly AppDbContext _context;

        public DevicesService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetAllSelectListItemAsync()
        {
            return await _context.Devices
                .Select(d => new SelectListItem() { Value = d.Id.ToString(), Text = d.Name })
                .OrderBy(s => s.Text).AsNoTracking().ToListAsync();
        }
    }
}
