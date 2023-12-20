using System.Diagnostics;

namespace GamesMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGamesService _gamedService;

        public HomeController(IGamesService gameService)
        {
            _gamedService = gameService;
        }

        public IActionResult Index()
        {
            var games = _gamedService.GetAll();
            return View(games);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
