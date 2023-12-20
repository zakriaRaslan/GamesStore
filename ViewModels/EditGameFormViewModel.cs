namespace GamesMvc.ViewModels
{
    public class EditGameFormViewModel : GameFormViewModel
    {
        public int Id { get; set; }

        public string? CurrentCover { get; set; }

        [AllowedExtensions(FileSettings.AllowedImgExtensions),
            MaxImgSize(FileSettings.MaxImgSizeInByte)]
        public IFormFile? Cover { get; set; } = default!;
    }
}
