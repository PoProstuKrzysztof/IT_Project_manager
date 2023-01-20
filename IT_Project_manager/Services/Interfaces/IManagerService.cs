using IT_Project_manager.Models;

namespace IT_Project_manager.Services.Interfaces;

public interface IManagerService
{
    public Task<int> Save(Manager manager);

    public Task<bool> Delete(int? id);

    public Manager? CreateManager(Manager manager);

    public Task<bool> Update(Manager manager);

    public Manager? FindBy(int? id);

    public Task<ICollection<Manager>> GetManagers();
}