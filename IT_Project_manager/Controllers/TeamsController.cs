using IT_Project_manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace IT_Project_manager.Controllers;
public class TeamsController : Controller
{
    private readonly AppDbContext _context;

    public TeamsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        try
        {
            return View( _context.Teams.ToList() );
        }
        catch (Exception ex)
        {
            Console.WriteLine( ex );
            return StatusCode( 500, ex.Message );
        }

    }

    [HttpGet]
    public IActionResult Create()
    {
        try
        {
            TeamsViewModel newTeam = new TeamsViewModel();
            newTeam.Members = GetMembers();
            newTeam.Managers= GetManagers();
            return View( newTeam );
        }
        catch (Exception e)
        {
            Console.WriteLine( e );
            return StatusCode( 500, e.Message );
        }

    }

    [HttpPost]
    public IActionResult Create(TeamsViewModel tvm)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                tvm.Managers = GetManagers();
                tvm.Members= GetMembers();
                return View(tvm);
            }


            var newTeam = new Team()
            {
                Name = tvm.Name,
                Description = tvm.Description,
                AssigmentDate = tvm.AssigmentDate,
                DeadlineDate = tvm.DeadlineDate

            };

            foreach (var managerId in tvm.ManagersId)
            {
                if (int.TryParse( managerId, out int id ))
                {
                    var manager = _context.Managers.Find( id );
                    if(newTeam.Managers.Add( manager ))
                    {
                        continue;
                    }
                    else
                    {
                        return View( newTeam );
                    }
                }
                        
            }

            foreach (var memberId in tvm.MembersId)
            {
                if (int.TryParse( memberId, out int id ))
                {
                    var member = _context.Members.Find( id );
                    if (newTeam.Members.Add( member ))
                    {
                        _context.Teams.Add(newTeam);
                        _context.SaveChanges();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return View( newTeam );
                    }
                        
                }
            }
            return View( newTeam );
            
        }
        catch (Exception e)
        {
            Console.WriteLine( e );
            return View( "Index" );
            //return StatusCode( 500, e.Message );
        }

    }


    public List<SelectListItem> GetManagers()
    {
        return _context.Managers
            .Select( m => new SelectListItem()
            {
                Value = m.Id.ToString(),
                Text = $"{m.Name} {m.Surname}"
            } )
            .ToList();

    }



    public List<SelectListItem> GetMembers()
    {
        return _context.Members
            .Select( m => new SelectListItem()
            {
                Value = m.Id.ToString(),
                Text = $"{m.Name} {m.Surname}"
            } )
            .ToList();
    }

}
