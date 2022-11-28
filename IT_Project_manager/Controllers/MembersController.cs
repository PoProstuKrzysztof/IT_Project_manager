using Microsoft.AspNetCore.Mvc;
using IT_Project_manager.Models;
using System.Reflection.Metadata.Ecma335;
using System.Diagnostics.Metrics;
using System.Data;
using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace IT_Project_manager.Controllers
{
    public class MembersController : Controller
    {
        private static AppDbContext _context = new AppDbContext();



        public async Task<IActionResult> Index()
        {

            return View( await _context.Members.ToListAsync() );
        }




        // Adding 
        [HttpPost]
        public IActionResult Create([FromForm] MembersViewModel member)
        {

            if (ModelState.IsValid || member is not null)
            {
                Member newMember = new Member()
                {
                    Name = member.Name,
                    Surname = member.Surname,
                    Email = member.Email,
                    DateOfBirth = member.DateOfBirth
                };
                
                foreach (var strId in member.ManagersId)
                {
                    if (int.TryParse( strId, out int id ))
                    {
                        newMember.Managers.Add( _context.Managers.Find( id ) );
                    }
                }
                _context.Members.Add( newMember );
                _context.SaveChanges();
                return View( "MemberConfirmation", member );
            }

            member.Managers = GetManagers();

            return View(member);
        }

        [HttpGet]
        public IActionResult Create()
        {

            MembersViewModel model = new MembersViewModel();
            model.Managers = GetManagers();
            return View( model );
        }


        //Editing 

        [HttpGet]
        public IActionResult Edit([FromRoute] int? id)
        {
            if (id == null || _context.Members == null)
            {
                return  NotFound();
            }

            Member? foundMember = _context.Members.Find( id );

            if (foundMember is not null)
            {
                return View( foundMember );
            }

            return RedirectToAction( "Index" );

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Member member)
        {

            if (ModelState.IsValid || member != null)
            {
                Member? foundMember = await _context.Members.FindAsync( member.Id );
                if (foundMember is not null)
                {
                    foundMember.Name = member.Name;
                    foundMember.Surname = member.Surname;
                    foundMember.Email = member.Email;
                    foundMember.DateOfBirth = member.DateOfBirth;
                    await _context.SaveChangesAsync();
                    return RedirectToAction( "Index");
                }
            }
            return RedirectToAction( "Index" );

        }








        //Deleting


        public async Task<IActionResult> Delete([FromRoute] int? id)
        {

            Member? foundMember = await _context.Members.FindAsync( id );
            if (foundMember is not null)
            {
                foundMember.Delete();
                _context.Members.Remove( foundMember );
                await _context.SaveChangesAsync();


                return View( "DeleteConfirmation", foundMember );
            }

            return RedirectToAction( "Index" );


        }


        private List<SelectListItem> GetManagers()
        {
            return _context
                .Managers
                .Select( m => new SelectListItem()
                {
                    Value = m.Id.ToString(),
                    Text = $"{m.Name} {m.Surname} {m.Telephone}"
                } )
                .ToList();
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any( e => e.Id == id );
        }

    }
}




