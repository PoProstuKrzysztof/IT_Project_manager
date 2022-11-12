using Microsoft.AspNetCore.Mvc;
using IT_Project_manager.Models;
using System.Reflection.Metadata.Ecma335;
using System.Diagnostics.Metrics;

namespace IT_Project_manager.Controllers
{
    public class MembersController : Controller
    {

        private static List<Member> memberList = new List<Member>()
        {
            new Member() {  Name = "Krzysztof", Surname="Palonek", Email="krzysiek.palonek@gmail.com"},
            //new Member() {  Name = "Marzena", Surname="Kołodziej", Email="marz.koł@gmail.com" },
            //new Member() {  Name = "Jan", Surname="Kowalski", Email="jan.kow@gmail.com" },
            //new Member() {  Name = "Natalia", Surname="Urodek", Email="Nat.uro@gmail.com" },
        };


        public IActionResult Member(List<Member> list)
        {
            list = memberList;
            return View( "MemberList", list );
        }

        [HttpPost]
        public IActionResult MemberForm(Member member)
        {

            if (ModelState.IsValid)
            {
                memberList.Add( member );
                return View( "MemberConfirmation", member );
            }
            return View();
        }

        [HttpGet]
        public IActionResult MemberForm()
        {
            return View();
        }




        [HttpPost]
        public IActionResult Edit([FromBody] int id)
        {

            Member member = memberList[id];
            if (ModelState.IsValid)
            {
                return View( "EditMember", member );
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }


        [HttpPost]
        public IActionResult DeleteMember([FromQuery] int id)
        {
            if (memberList.Count == 0)
                return View( "MemberForm" );

            Member member = memberList[id];
            if (ModelState.IsValid)
            {
                memberList.Remove( memberList[id] );
                foreach (Member m in memberList)
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
