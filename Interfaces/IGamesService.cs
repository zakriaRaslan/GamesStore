namespace GamesMvc.Interfaces
{
    public interface IGamesService
    {
        IEnumerable<Game> GetAll();
        Task<Game> GetAsync(int id);
        Task AddNewAsync(AddGameFormViewModel model);
        Task<Game?> UpdateAsync(EditGameFormViewModel model);
        bool Delete(int Id);
    }
}
