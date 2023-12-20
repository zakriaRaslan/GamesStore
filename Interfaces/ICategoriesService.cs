namespace GamesMvc.Interfaces
{
    public interface ICategoriesService
    {
        Task<IEnumerable<SelectListItem>> GetAllSelectListItemAsync();
    }
}
