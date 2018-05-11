using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Webnfwithapi1.Models;

namespace Webnfwithapi1.Controllers
{
   // [Authorize]
   // [AllowAnonymous]
    public class UsersFKUsController : ApiController
    {
       
        private MyContext db = new MyContext();

        // GET: api/UsersFKUs
        public IQueryable<UsersFKUs> GetUSERFKU()
        {
            // return db.USERFKU;
            int skippage = 0;
            int takepage = 20;
            //определение  инспекции
            
            var aduser = User.Identity.Name;
            string myinsp = "";
            var www = db.USERFKU.Where(q => String.Equals(q.ADuser, aduser));
            //
            var CountInsp = db.Zayavkis.Where(inspekcia => String.Equals(inspekcia.Num_insp, myinsp)).Count();
            var result = db.Zayavkis.Where(inspekcia => String.Equals(inspekcia.Num_insp, myinsp))
                    .OrderByDescending(o => o.Id).Skip(skippage).Take(takepage);
            return db.USERFKU.Where(q => String.Equals(q.ADuser, aduser)); // вернет юзера  вошедшего в систему
           
        }
        // GET: api/allUser
        [HttpGet]
        [Route("api/allusers")]
        public IHttpActionResult GetAllUSERFKU()
        { return Ok( db.USERFKU); }


        //all users lazy loading
        [HttpGet]
        [Route("api/userslazy/{page}")]
        [Authorize]
        public IHttpActionResult GetUser(int page)
        {
             
            const int pageSize = 50;
            var itemsToSkip = page * pageSize;
            //find  inspection
            var spisok = db.USERFKU.OrderBy(o=>o.Id).Skip(itemsToSkip).Take(pageSize).ToList();

            return Ok(spisok);



            
        }
        // GET: api/UsersFKUs/5
        [ResponseType(typeof(UsersFKUs))]
        public async Task<IHttpActionResult> GetUsersFKUs(int id)
        {
            UsersFKUs usersFKUs = await db.USERFKU.FindAsync(id);
            if (usersFKUs == null)
            {
                return NotFound();
            }

            return Ok(usersFKUs);
        }

        // PUT: api/UsersFKUs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUsersFKUs(int id, UsersFKUs usersFKUs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usersFKUs.Id)
            {
                return BadRequest();
            }

            db.Entry(usersFKUs).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersFKUsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/UsersFKUs
        [ResponseType(typeof(UsersFKUs))]
        public async Task<IHttpActionResult> PostUsersFKUs(UsersFKUs usersFKUs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.USERFKU.Add(usersFKUs);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = usersFKUs.Id }, usersFKUs);
        }

        // DELETE: api/UsersFKUs/5
        [ResponseType(typeof(UsersFKUs))]
        public async Task<IHttpActionResult> DeleteUsersFKUs(int id)
        {
            UsersFKUs usersFKUs = await db.USERFKU.FindAsync(id);
            if (usersFKUs == null)
            {
                return NotFound();
            }

            db.USERFKU.Remove(usersFKUs);
            await db.SaveChangesAsync();

            return Ok(usersFKUs);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsersFKUsExists(int id)
        {
            return db.USERFKU.Count(e => e.Id == id) > 0;
        }
        
    }
}