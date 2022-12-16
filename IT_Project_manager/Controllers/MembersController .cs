using IT_Project_manager.Models;
using IT_Project_manager.Services;
using Microsoft.AspNetCore.Mvc;

namespace IT_Project_manager.Controllers
{
    public class MembersController : Controller
    {
        private readonly IMemberService? _memberService;

        public MembersController(AppDbContext context, IMemberService memberService)
        {
            _memberService = memberService;
        }

        public IActionResult Index()
        {
            return View(  _memberService.GetMembers() );
        }

        // Adding
        [HttpGet]
        public IActionResult Create()
        {
            MembersViewModel model = new MembersViewModel();
            model.Managers = _memberService.GetManagers();
            return View( model );
        }

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

        //Editing
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
        public IActionResult Edit(Member member)
        {
            if (ModelState.IsValid || member != null)
            {
                _memberService.Update( member );
                return RedirectToAction( "Index" );
            }

            return View( member );
        }

        //Deleting
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

        //Details
        [HttpGet]
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