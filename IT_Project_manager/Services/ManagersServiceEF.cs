using IT_Project_manager.Data;
using IT_Project_manager.Models;
using IT_Project_manager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IT_Project_manager.Services;

public class ManagersServiceEF : IManagerService
{
    private readonly AppDbContext _context;

    public ManagersServiceEF(AppDbContext context)
    {
        _context = context;
    }

    //Create new manager instance
    public  Manager? CreateManager(Manager manager)
    {
        Manager m = new Manager()
        {
            Name = manager.Name,
            Surname = manager.Surname,
            Telephone = manager.Telephone
        };
        return m;
    }

    //Deleting
    public async Task<bool> Delete(int? id)
    {
        if (id == null)
        {
            return false;
        }

        var manager = await _context.Managers.FindAsync( id );
        if (manager is not null)
        {
            _context.Managers.Remove( manager );
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    //Find Manager
    public Manager? FindBy(int? id)
    {
        if (id is null)
        {
            throw new ArgumentNullException( "Manager not found" );
        }
        var manager = _context.Managers.Find( id );

        if (manager is not null)
        {
            return manager;
        }
        throw new ArgumentNullException( "Manager not found" );
    }

    //Managers to list
    public async Task<ICollection<Manager>> GetManagers()
    {
        return await _context.Managers.ToListAsync();
    }

    //Save manager in database
    public async Task<int> Save(Manager manager)
    {
        var entityEntry = await _context.Managers.AddAsync( manager );
        await _context.SaveChangesAsync();
        return entityEntry.Entity.Id;
    }

    // Update manager
    public async Task<bool> Update(Manager manager)
    {
        if (manager == null)
        {
            return false;
        }

        try
        {
            var findManager = await _context.Managers.FindAsync( manager.Id );
            if (manager is not null)
            {
                findManager.Name = manager.Name;
                findManager.Surname = manager.Surname;
                findManager.Telephone = manager.Telephone;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }
    }
}