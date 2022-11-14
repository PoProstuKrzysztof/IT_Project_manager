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
        private static AppDbContext context = new AppDbContext();


        // Local data
        private static List<Member> memberList = new List<Member>()
        {
            new Member() {  Name = "Krzysztof", Surname="Palonek", Email="krzysiek.palonek@gmail.com"},
            new Member() {  Name = "Marzena", Surname="Kołodziej", Email="marz.koł@gmail.com" },
            new Member() {  Name = "Jan", Surname="Kowalski", Email="jan.kow@gmail.com" },
            new Member() {  Name = "Natalia", Surname="Urodek", Email="Nat.uro@gmail.com" }
        };
       
        // List of members
        //public IActionResult Member(List<Member> list)
        //{
            
        //    list = context.Members.ToList();
        //    return View( "MemberList", list );
        //}

        public IActionResult Index()
        {

            
            return View(context.Members.ToList() );
        }







        // Adding 
        [HttpPost]
        public IActionResult MemberForm(Member member)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.Members.Add( member );
                    context.SaveChanges();
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

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost, ActionName("Edit")]
        public IActionResult Edit( int? id)
        {

            if(id == null)
            {
                return BadRequest();
            }


            int? memberId = id;
            Member? member = context.Members.Find( memberId );
            if (ModelState.IsValid)
            {
                return View( "EditMember", member );
            }

            return View();
        }






        //Deleting

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (memberList.Count == 0)
                return View( "MemberForm" );
            
            if (id == null)
            {
                return NotFound();
            }
            
            Member? member = context.Members.Find(id);
            if (ModelState.IsValid)
            {
                context.Members.Remove( context.Members.Find(id));
                context.SaveChanges();
                foreach (Member m in context.Members)
                {
                    m.Delete();
                }

                return View( "DeleteConfirmation", member );
            }

            return View();
        }


        [HttpGet]
        public IActionResult DeleteMember()
        {
            return View();
        }


    }
}
