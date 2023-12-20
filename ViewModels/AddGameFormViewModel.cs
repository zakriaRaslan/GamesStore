
// Ignore Spelling: Mvc
namespace GamesMvc.ViewModels
{
    public class AddGameFormViewModel : GameFormViewModel
    {

        [AllowedExtensions(FileSettings.AllowedImgExtensions),
            MaxImgSize(FileSettings.MaxImgSizeInByte)]
        public IFormFile Cover { get; set; } = default!;

    }
}
