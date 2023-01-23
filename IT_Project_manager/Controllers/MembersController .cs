using IT_Project_manager.Data;
using IT_Project_manager.Models;
using IT_Project_manager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace IT_Project_manager.Controllers
{
    public class MembersController : Controller
    {
        private readonly IMemberService? _memberService;
        

        public MembersController( IMemberService memberService)
        {
            _memberService = memberService;
            
        }


        // List of members
        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {
            try
            {
                @ViewData["CurrentFilter"] = search;
                var members = from c in await _memberService.GetMembers()
                        select c;

                if (!string.IsNullOrEmpty( search ))
                {
                    members = members.Where( c => c.Name.Contains( search ) );
                }

                return View( members );
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex );
                return StatusCode( 500, ex.Message );
            }

        }

        // Creating [GET]

        [HttpGet, Authorize( Roles = "Administrator" )]
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
        [HttpPost, Authorize( Roles = "Administrator" )]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MembersViewModel member)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View( member );
                }

                var newMember =  _memberService.CreateMember( member );

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

        [HttpGet, Authorize( Roles = "Administrator" )]
        
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
        [HttpPost, Authorize( Roles = "Administrator" )]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(Member member)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _memberService.Update( member );

                    var username = HttpContext.User.Identity.Name;

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
        [HttpGet, Authorize( Roles = "Administrator" )]
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
        [HttpGet, Authorize( Roles = "Administrator " )]        
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