using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Owner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {

        public static List<Model.Owner> Owners = new List<Model.Owner>()
        {
            new Model.Owner
            {
                ID =1,
                Name="tugba",
                Surname="yazıcı",
                Date=new DateTime(1996,04,20),
                Comment="tugbayazıcı",
                Type="owner"
            },

            new Model.Owner
            {
                ID=2,
                Name="yagmur",
                Surname="yazıcı",
                Date=new DateTime(1994,07,31),
                Comment="yagmuryazıcı",
                Type="owner"
            },

            new Model.Owner
            {
                ID=3,
                Name="emel",
                Surname="yazıcı",
                Date=new DateTime(1971,12,21),
                Comment="emelyazıcı",
                Type="owner"
            }
        };

        [Route("All")]
        [HttpGet]
        public List<Model.Owner> Get()
        {
            var owners = Owners.OrderBy(x => x.ID).ToList();
            return owners;
        }


        [HttpGet("{id:int}")]
        public  IActionResult GetOwner(int id)
        {
            var owners = Owners.Where(x => x.ID == id).FirstOrDefault();
            if(owners==null)
            {
                throw new BadHttpRequestException("User not found.");
            }
            else
            {
                return Ok(owners);
            }
        }


        [HttpPost]
        [Route("Create")]
        [Consumes("application/json")]
        public IActionResult AddOwner([FromBody] Model.Owner newOwner)
        {
            var owner = Owners.SingleOrDefault(x => x.ID == newOwner.ID);
            if (owner is not null)
                return BadRequest("ID exist!");
            if (newOwner.Comment.Contains("hack"))
                return BadRequest("invalid description");
            Owners.Add(newOwner);
            return Ok("Created!");
        }


        [Route("Delete")]
        [HttpDelete]
        public IActionResult DeleteOwner(int ID)
        {
            var owner = Owners.SingleOrDefault(x => x.ID == ID);
            if (owner is null)
                return NotFound("ID not found");
            Owners.Remove(owner);
            return Ok("deleted!");

        }

        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateOwner(int id, [FromBody] Model.Owner updateOwner)
        {
            var owner = Owners.SingleOrDefault(x => x.ID == id);
            if (owner is null)
                return NotFound("ID not found");
            owner.ID = updateOwner.ID != default ? updateOwner.ID : owner.ID;
            owner.Name = updateOwner.Name != default ? updateOwner.Name : owner.Name;
            owner.Surname = updateOwner.Surname != default ? updateOwner.Surname : owner.Surname;
            owner.Date = updateOwner.Date != default ? updateOwner.Date : owner.Date;
            owner.Comment = updateOwner.Comment != default ? updateOwner.Comment : owner.Comment;
            owner.Type = updateOwner.Type != default ? updateOwner.Type : owner.Type;

            return Ok("updated!");
        }
    }
}