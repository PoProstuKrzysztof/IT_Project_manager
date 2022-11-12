using Microsoft.AspNetCore.Mvc;
using IT_Project_manager.Models;
using System.Reflection.Metadata.Ecma335;
using System.Diagnostics.Metrics;

namespace IT_Project_manager.Controllers
{
    public class MembersController : Controller
    {
        private static AppDbContext context = new AppDbContext();

        private static List<Member> memberList = new List<Member>()
        {
            new Member() {  Name = "Krzysztof", Surname="Palonek", Email="krzysiek.palonek@gmail.com"},
            new Member() {  Name = "Marzena", Surname="Kołodziej", Email="marz.koł@gmail.com" },
            new Member() {  Name = "Jan", Surname="Kowalski", Email="jan.kow@gmail.com" },
            new Member() {  Name = "Natalia", Surname="Urodek", Email="Nat.uro@gmail.com" }
        };
       

        public IActionResult Member(List<Member> list)
        {           
            list = context.Members.ToList();
            return View( "MemberList", list );
        }

        [HttpPost]
        public IActionResult MemberForm(Member member)
        {

            if (ModelState.IsValid)
            {
                context.Members.Add(member);
                context.SaveChanges();
                return View( "MemberConfirmation", member );
            }
            return View();
        }

        [HttpGet]
        public IActionResult MemberForm()
        {
            return View();
        }




        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit([FromBody] int id)
        {

            Member member = context.Members.Find( id );
            if (ModelState.IsValid)
            {
                return View( "EditMember", member );
            }

            return View();
        }








        [HttpPost]
        public IActionResult DeleteMember(int id)
        {
            if (memberList.Count == 0)
                return View( "MemberForm" );
            
            
            
            Member member = context.Members.Find(id);
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
