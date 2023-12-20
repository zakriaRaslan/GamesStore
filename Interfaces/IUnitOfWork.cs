namespace GamesMvc.Interfaces
{
    public interface IUnitOfWork
    {
        public ICategoriesService CategoriesService { get; }
        public IDevicesService DevicesService { get; }
        public IGamesService GamesService { get; }
        Task<bool> SaveAsync();
    }
}
