

namespace GamesMvc.Services
{
    public class GamesService : IGamesService
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _webHostEnvironment;
        private readonly string _ImagePath;
        public GamesService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _ImagePath = $"{_webHostEnvironment.WebRootPath}{FileSettings.GamesImagePath}";
        }

        public IEnumerable<Game> GetAll()
        {
            return _context.Games.Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(d => d.Device).AsNoTracking().ToList();
        }

        public async Task<Game> GetAsync(int id)
        {
            var game = await _context.Games.Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(d => d.Device).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return game;
        }

        public async Task AddNewAsync(AddGameFormViewModel model)
        {
            var gameCoverName = await SaveGameCover(model.Cover);

            Game game = new()
            {
                Name = model.Name,
                CategoryId = model.CategoryId,
                Description = model.Description,
                Cover = gameCoverName,
                Devices = model.SelectedDevices.Select(d => new GameDevices() { DeviceId = d }).ToList(),
            };

            _context.Add(game);
        }

        public async Task<Game?> UpdateAsync(EditGameFormViewModel model)
        {
            var game = _context.Games.Include(g => g.Devices)
                .SingleOrDefault(g => g.Id == model.Id);

            if (game == null)
            {
                return null;
            }
            var hasNewCover = model.Cover != null;
            var oldCover = game.Cover;

            game.Name = model.Name;
            game.CategoryId = model.CategoryId;
            game.Description = model.Description;
            game.Devices = model.SelectedDevices.Select(d => new GameDevices() { DeviceId = d }).ToList();

            if (hasNewCover)
            {
                game.Cover = await SaveGameCover(model.Cover!);
            }
            var effectedRows = _context.SaveChanges();
            if (effectedRows > 0)
            {
                if (hasNewCover)
                {
                    var cover = Path.Combine(_ImagePath, oldCover);
                    File.Delete(cover);
                }

                return game;
            }
            else
            {
                var cover = Path.Combine(_ImagePath, game.Cover);
                File.Delete(cover);
                return null;
            }

        }

        public bool Delete(int Id)
        {
            var isDeleted = false;
            var game = _context.Games.Find(Id);
            if (game == null)
                return isDeleted;

            _context.Remove(game);
            var effectedRows = _context.SaveChanges();
            if (effectedRows > 0)
            {
                isDeleted = true;
                var cover = Path.Combine(_ImagePath, game.Cover);
                File.Delete(cover);
            }

            return isDeleted;

        }


        private async Task<string> SaveGameCover(IFormFile Cover)
        {
            var gameCoverName = $"{Guid.NewGuid()}{Path.GetExtension(Cover.FileName)}";
            var path = Path.Combine(_ImagePath, gameCoverName);

            using var stream = File.Create(path);
            await Cover.CopyToAsync(stream);

            return gameCoverName;
        }

    }
}
