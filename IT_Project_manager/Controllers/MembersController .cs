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
        public async Task<IActionResult> Index(string searchString)
        {
            @ViewData["CurrentFilter"] = searchString;
            var m = from c in await _memberService.GetMembers()
                    select c;

            if(!string.IsNullOrEmpty( searchString ))
            {
                m = m.Where( c => c.Name.Contains( searchString ) );
            }

            return View(m );
        }

        // Creating [GET]
        
        [HttpGet]
        [Authorize( Roles = "Administrator" )]

        public async Task<IActionResult> Create()
        {
            MembersViewModel model = new MembersViewModel();
            model.Managers = await _memberService.GetManagers();
            return View( model );
        }

        //Creating [POST]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(MembersViewModel member)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           var newMember = await _memberService.CreateMember( member );

            if (await _memberService.AddManagerToMember( member, newMember ))
            {
                await _memberService.Save( newMember );
                return View( "MemberConfirmation", member );
            }

            member.Managers = await _memberService.GetManagers();
            return View( member );
        }

        //Editing [GET]
        
        [HttpGet]
        [Authorize( Roles = "Administrator" )]
        public async Task<IActionResult> Edit([FromRoute] int? id)
        {
            if (id is null)
            {
                return BadRequest( ModelState );
            }

            var member = await _memberService.FindBy( id );

           

            return member is null ? NotFound() : View( member );
        }

        //Editing [POST]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(Member member)
        {

            if (ModelState.IsValid )
            {
                await _memberService.Update( member );

                var username = HttpContext.User.Identity.Name;
                _logger.LogWarning( ( EventId )400, $"{member.Id} edited by {username} on {DateTime.Now}" );

                return RedirectToAction( "Index" );
            }

            return View( member );  
        }

        //Deleting [GET]
        [Authorize]
        [Authorize( Roles = "Administrator" )]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest( ModelState );
            }

            if (await _memberService.Delete( id ))
            {
                return RedirectToAction( "Index" );
            }

            return Problem( "Trying to delete not existing member" );

        }

        //Details [GET]
        [Authorize]
        [HttpGet]
        [Authorize( Roles = "Administrator " )]
        public async Task<IActionResult> Details(Member member)
        {
            if (member is null)
            {
                return BadRequest( ModelState );
            }

            var found = await _memberService.FindBy( member.Id );
            return found is null ? NotFound() : View( found );
        }
    }
}