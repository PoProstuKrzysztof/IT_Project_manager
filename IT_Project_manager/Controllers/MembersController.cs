using Microsoft.AspNetCore.Mvc;
using IT_Project_manager.Models;
using System.Reflection.Metadata.Ecma335;
using System.Diagnostics.Metrics;
using System.Data;
using System.Net;

namespace IT_Project_manager.Controllers
{
    public class MembersController : Controller
    {
        private static AppDbContext _context = new AppDbContext();


        // Local data
        //private static List<Member> memberList = new List<Member>()
        //{
        //    new Member() {  Name = "Krzysztof", Surname="Palonek", Email="krzysiek.palonek@gmail.com"},
        //    new Member() {  Name = "Marzena", Surname="Kołodziej", Email="marz.koł@gmail.com" },
        //    new Member() {  Name = "Jan", Surname="Kowalski", Email="jan.kow@gmail.com" },
        //    new Member() {  Name = "Natalia", Surname="Urodek", Email="Nat.uro@gmail.com" }
        //};


        public IActionResult Index()
        {

            return View( _context.Members.ToList() );
        }




        // Adding 
        [HttpPost]
        public IActionResult MemberForm(Member member)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Members.Add( member );
                    _context.SaveChanges();
                    return View( "MemberConfirmation", member );
                }
            }
            catch (DataException ex)
            {
                ModelState.AddModelError( "", "Unable to save changes. Try again, and if the problem persists see your system administrator." );
            }

            return View();
        }

        [HttpGet]
        public IActionResult MemberForm()
        {
            return View();
        }


        //Editing 


        public IActionResult Edit([FromRoute] int id)
        {
            Member? foundMember = _context.Members.Find( id );
            if (foundMember is not null)
            {
                return View( foundMember );
            }

            return RedirectToAction( "Index" );

        }



        [HttpPost]
        public IActionResult Edit([FromForm] Member member)
        {

            if (ModelState.IsValid)
            {
                Member? foundMember = _context.Members.Find( member.Id );
                if (foundMember is not null)
                {
                    foundMember.Name = member.Name;
                    foundMember.Surname = member.Surname;
                    foundMember.Email = member.Email;
                    _context.SaveChanges();
                    return RedirectToAction( "Index" );
                }
            }
            return View();

        }






        //Deleting


        public IActionResult Delete([FromRoute] int id)
        {
            Member? foundMember = _context.Members.Find( id );
            foundMember.Delete();
            _context.Members.Remove( foundMember );
            _context.SaveChanges();
            
            
            return View( "DeleteConfirmation", foundMember );

        }




    }
}
