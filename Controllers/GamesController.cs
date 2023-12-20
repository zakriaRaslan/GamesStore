namespace GamesMvc.Controllers
{
    public class GamesController : Controller
    {
        private readonly IUnitOfWork _UOW;
        public GamesController(IUnitOfWork UOW)
        {
            _UOW = UOW;
        }
        public IActionResult Index()
        {
            var games = _UOW.GamesService.GetAll();
            return View(games);
        }


        public async Task<IActionResult> Create()
        {
            AddGameFormViewModel game = new()
            {
                Categories = await _UOW.CategoriesService.GetAllSelectListItemAsync(),
                Devices = await _UOW.DevicesService.GetAllSelectListItemAsync(),
            };

            return View(game);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Devices = await _UOW.DevicesService.GetAllSelectListItemAsync();
                model.Categories = await _UOW.CategoriesService.GetAllSelectListItemAsync();
                return View(model);
            }

            await _UOW.GamesService.AddNewAsync(model);
            await _UOW.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var game = await _UOW.GamesService.GetAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var game = await _UOW.GamesService.GetAsync(id);

            if (game is null)
                return NotFound();

            EditGameFormViewModel viewModel = new()
            {
                Id = id,
                Name = game.Name,
                Description = game.Description,
                CategoryId = game.CategoryId,
                SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                Categories = await _UOW.CategoriesService.GetAllSelectListItemAsync(),
                Devices = await _UOW.DevicesService.GetAllSelectListItemAsync(),
                CurrentCover = game.Cover,
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Devices = await _UOW.DevicesService.GetAllSelectListItemAsync();
                model.Categories = await _UOW.CategoriesService.GetAllSelectListItemAsync();
                return View(model);
            }

            var updatedGame = await _UOW.GamesService.UpdateAsync(model);
            if (updatedGame == null)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = _UOW.GamesService.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }
    }
}
