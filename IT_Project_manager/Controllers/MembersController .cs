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
                _memberService.Save( newMember );
                return View( "MemberConfirmation", member );

            }

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
        public IActionResult Delete([FromRoute] int? id)
        {
            var member = _memberService.FindBy( id );
            return member is null ? NotFound() : RedirectToAction( "Index" );

        }


    }
}




