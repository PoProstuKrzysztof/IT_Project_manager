using IT_Project_manager.Models;
using IT_Project_manager.Services;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace IT_Project_manager.Controllers
{
    public class MembersController : Controller
    {
        private readonly IMemberService? _memberService;
        private readonly ILogger<MembersController> _logger;

        public MembersController(AppDbContext context,ILogger<MembersController> logger, IMemberService memberService)
        {
            _memberService = memberService;
            _logger = logger;
        }

        // List of members
        public IActionResult Index()
        {
            return View(_memberService.GetMembers() );
        }

        // Creating [GET]
        
        [HttpGet]
        [Authorize( Roles = "Administrator" )]

        public IActionResult Create()
        {
            MembersViewModel model = new MembersViewModel();
            model.Managers = _memberService.GetManagers();
            return View( model );
        }

        //Creating [POST]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MembersViewModel member)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            Member newMember = new Member()
            {
                Name = member.Name,
                Surname = member.Surname,
                Email = member.Email,
                DateOfBirth = member.DateOfBirth
            };

            if (_memberService.AddManagerToMember( member, newMember ))
            {
                _memberService.Save( newMember );
                return View( "MemberConfirmation", member );
            }

            member.Managers = _memberService.GetManagers();
            return View( member );
        }

        //Editing [GET]
        
        [HttpGet]
        [Authorize( Roles = "Administrator" )]
        public IActionResult Edit([FromRoute] int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var member = _memberService.FindBy( id );

           

            return member is null ? NotFound() : View( member );
        }

        //Editing [POST]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(Member member)
        {


            if (ModelState.IsValid )
            {
                _memberService.Update( member );

                var username = HttpContext.User.Identity.Name;
                _logger.LogWarning( ( EventId )400, $"{member.Id} edited by {username} on {DateTime.Now}" );

                return RedirectToAction( "Index" );
            }




            return View( member );  
        }

        //Deleting [GET]
        [Authorize]
        [Authorize( Roles = "Administrator" )]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (_memberService.Delete( id ))
            {
                return RedirectToAction( "DeleteConfirmation" );
            }
            return Problem( "Trying to delete not existing member" );
        }

        //Details [GET]
        [Authorize]
        [HttpGet]
        [Authorize( Roles = "Administrator " )]
        public IActionResult Details(Member member)
        {
            if (member is null)
            {
                return NotFound();
            }

            var found = _memberService.FindBy( member.Id );
            return found is null ? NotFound() : View( found );
        }
    }
}