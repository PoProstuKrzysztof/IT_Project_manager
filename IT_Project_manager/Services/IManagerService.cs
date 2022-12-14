using IT_Project_manager.Models;

namespace IT_Project_manager.Services;

public interface IManagerService
{
    public int Save(Manager manager);

    public bool Delete(int? id);

    public bool Update(Manager manager);

    public Manager? FindBy(int? id);

    public ICollection<Manager> GetManagers();
}