using CountryClubAPI.DataAccess;
using CountryClubAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CountryClubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly CountryClubContext _context;

        public MembersController(CountryClubContext context)
        {
            _context = context;
        }



        public IActionResult AllMembers()
        {
            var members = _context.Members;

            return new JsonResult(members);
        }



        [Route("/api/members/{id:int}")]
        public ActionResult SingleMember(int id)
        {
            var member = _context.Members.SingleOrDefault(m => m.Id == id);

            if (member == null)
            {
                return NotFound();
            }

            return new JsonResult(member);

        }



        [HttpPost]
        public ActionResult CreateMember(Member member)
        {
            if (member == null)
            {
                return BadRequest();
            }

            _context.Members.Add(member);
            _context.SaveChanges();

            return new JsonResult(member);
        }




        [HttpPut]
        [Route("/api/members/{id:int}")]
        public IActionResult UpdateMember(int id, Member member)
        {
            if (id != member.Id || member == null)
            {
                return BadRequest();
            }

            member.Id = id;

            _context.Members.Update(member);
            _context.SaveChanges();

            return Redirect($"/api/members/{member.Id}");

        }




        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var member = _context.Members.Find(id);

            if (member == null)
            {
                return NotFound();
            }

            _context.Members.Remove(member);
            _context.SaveChanges();

            return Content("Deleted");
        }
    }
}
