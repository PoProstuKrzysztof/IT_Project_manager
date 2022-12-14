using IT_Project_manager.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace IT_Project_manager.Services;

public class ManagersServiceEF : IManagerService
{
    private readonly AppDbContext _context = new AppDbContext();

    public ManagersServiceEF(AppDbContext context)
    {
        _context = context;
    }
    //Deleting
    public bool Delete(int? id)
    {
        if (id == null)
        {
            return false;
        }


        var manager = _context.Managers.Find( id );
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
            throw new ArgumentNullException( "Member not found" );
        }
        var manager = _context.Managers.Find( id );

        if (manager is not null)
        {
            return manager;
        }
        throw new ArgumentNullException( "Member not found" );
    }

    //Managers to list
    public ICollection<Manager> GetManagers()
    {
        return _context.Managers.ToList();
    }

    //Save manager in database
    public int Save(Manager manager)
    {
        var entityEntry = _context.Managers.Add( manager );
        _context.SaveChanges();
        return entityEntry.Entity.Id;
    }

    // Update manager
    public bool Update(Manager manager)
    {
        if (manager == null)
        {
            return false;
        }


        try
        {
            var findManager = _context.Managers.Find( manager.Id );
            if (manager is not null)
            {
                findManager.Name = manager.Name;
                findManager.Surname = manager.Surname;
                findManager.Telephone = manager.Telephone;
                _context.SaveChanges();
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

