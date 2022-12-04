using Microsoft.AspNetCore.Mvc;
using IT_Project_manager.Models;
using System.Reflection.Metadata.Ecma335;
using System.Diagnostics.Metrics;
using System.Data;
using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IT_Project_manager.Services;

namespace IT_Project_manager.Controllers
{
    public class MembersController : Controller
    {
        private readonly IMemberService? _memberService;

        public MembersController(AppDbContext context, IMemberService memberService)
        {
            _memberService= memberService;
        }

        public IActionResult Index()
        {
            
            return View( _memberService.GetMembers());
        }

        // Adding 
        //---------------------------------------------------------------------------------------------------------------
        [HttpGet]
        public IActionResult Create()
        {

            MembersViewModel model = new MembersViewModel();
            model.Managers = _memberService.GetManagers();
            return View( model );
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
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
                foreach (var mId in member.ManagersId)
                {
                    if (int.TryParse( mId, out int id ))
                    {
                        
                        newMember.Managers.Add( _memberService.GetManager( id ) );
                    }
                    else continue;
                }
                _memberService.Save( newMember );
                return View( "MemberConfirmation", member );

            }
            member.Managers = _memberService.GetManagers();
            return View( member );
        }




        //Editing 
        //---------------------------------------------------------------------------------------------------------------
        [HttpGet]
        public IActionResult Edit([FromRoute] int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var member = _memberService.FindBy( id );
            return member is null ? NotFound() : View( member );

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Member member)
        {

            if (ModelState.IsValid || member != null)
            {
                _memberService.Update( member );
                return RedirectToAction( "Index" );
            }

            return View( member );

        }


        //Deleting
        //---------------------------------------------------------------------------------------------------------------
        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            if(_memberService.Delete( id ))
            {
                return RedirectToAction( "Index" );
            }
            return Problem("Trying to delete not existing member");
            

        }


    }
}




