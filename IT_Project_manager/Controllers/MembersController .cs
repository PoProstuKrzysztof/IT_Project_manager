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

        public MembersController(AppDbContext context, ILogger<MembersController> logger, IMemberService memberService)
        {
            _memberService = memberService;
            _logger = logger;
        }


        // List of members
        public async Task<IActionResult> Index(string searchString)
        {
            try
            {
                @ViewData["CurrentFilter"] = searchString;
                var m = from c in await _memberService.GetMembers()
                        select c;

                if (!string.IsNullOrEmpty( searchString ))
                {
                    m = m.Where( c => c.Name.Contains( searchString ) );
                }

                return View( m );
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex );
                return StatusCode( 500, ex.Message );
            }

        }

        // Creating [GET]

        [HttpGet]
        [Authorize( Roles = "Administrator" )]
        public async Task<IActionResult> Create()
        {
            try
            {
                MembersViewModel model = new MembersViewModel();
                model.Managers = await _memberService.GetManagers();
                return View( model );
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex );
                return StatusCode( 500, ex.Message );
            }

        }

        //Creating [POST]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MembersViewModel member)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View( member );
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
            catch (Exception ex)
            {
                Console.WriteLine( ex );
                return StatusCode( 500, ex.Message );
            }

        }

        //Editing [GET]

        [HttpGet]
        [Authorize( Roles = "Administrator" )]
        public async Task<IActionResult> Edit([FromRoute] int? id)
        {
            try
            {
                if (id is null)
                {
                    return BadRequest( ModelState );
                }

                var member = await _memberService.FindBy( id );
                

                return View( member );
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex );
                return StatusCode( 500, ex.Message );
            }

        }

        //Editing [POST]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(Member member)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _memberService.Update( member );

                    var username = HttpContext.User.Identity.Name;
                    _logger.LogWarning( ( EventId )400, $"{member.Id} edited by {username} on {DateTime.Now}" );

                    return RedirectToAction( "Index" );
                }

                return View( member );
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex );
                return StatusCode( 500, ex.Message );
            }

        }

        //Deleting [GET]        
        [Authorize( Roles = "Administrator" )]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest( "Trying to delete not existing member" );
                }

                if (await _memberService.Delete( id ))
                {
                    return RedirectToAction( "Index" );
                }

                return BadRequest( "Trying to delete not existing member" );
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex );
                return StatusCode( 500, ex.Message );
            }


        }

        //Details [GET]
        [Authorize]
        [HttpGet]
        [Authorize( Roles = "Administrator " )]
        public async Task<IActionResult> Details(Member member)
        {
            try
            {
                if (member is null)
                {
                    return BadRequest( ModelState );
                }

                var found = await _memberService.FindBy( member.Id );
                
                return View( found );
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex );
                return StatusCode( 500, ex.Message );
            }

        }
    }
}