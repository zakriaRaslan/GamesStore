namespace GamesMvc.Interfaces
{
    public interface IDevicesService
    {
        Task<IEnumerable<SelectListItem>> GetAllSelectListItemAsync();
    }
}
