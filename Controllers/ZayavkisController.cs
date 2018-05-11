using Newtonsoft.Json;
using PagedList;
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
    public class ZayavkisController : ApiController
    {
        private MyContext db = new MyContext();
        [Authorize]
        // GET: api/Zayavkis
        public IQueryable<Zayavki> GetZayavkis()
        {
            
            
            //конец  блока  определения
            int skippage = 0;
            int takepage = 20;
            return db.Zayavkis.OrderByDescending(q => q.Id).Skip(skippage).Take(takepage);

           

        }
        //вывод по инспекции ленивой  загрузкой
        [HttpGet]
        [Route("api/getinsp/{page}")]
        [Authorize]
        public IHttpActionResult GetInspa(int page)
        {
            var aduser = User.Identity.Name;
            var countusers = db.USERFKU.Count();
            var countzayavki = db.Zayavkis.Count();

            try
            {
                var result = db.USERFKU.Where(q => String.Equals(q.ADuser, aduser)).Select(t => t.Num_insp).Single();

                const int pageSize = 50;
                var itemsToSkip = page * pageSize;
                //find  inspection


                //вывод по инспекции
                var spisok = db.Zayavkis.Where(inspekcia => String.Equals(inspekcia.Num_insp, result))
                         .OrderByDescending(o => o.Id).Skip(itemsToSkip).Take(pageSize).ToList();
                return Ok(spisok);//явное преобразование quereble  ti string  // описать ислючение если такого пользователя  нет
            }
            catch (Exception ex)
            {
                return NotFound();
            }
            
           
        }
        //my page ленивая  загрузка
        [Route("api/zayavkis/get/{page}")]
        public IHttpActionResult GetItems(int page)
        {
            const int pageSize = 100;//'ktvtynjd  pf  hfp
            var itemsToSkip = page * pageSize;
            return Ok(db.Zayavkis.OrderByDescending(t => t.Id).Skip(itemsToSkip).Take(pageSize).ToList());
        }
        //
        
        //get back zayavki -1 list lazy loading
        [HttpGet]
        [Route("api/zayavkis/back/{page}")]
        [Authorize]
        public IHttpActionResult BackItems(int  page)
        {
            
            const int pageSize = 20;//количество выводимых заявок
            var itemsToSkip = page * pageSize;
            return Ok(db.Zayavkis.OrderByDescending(t => t.Id).Skip(itemsToSkip).Take(pageSize).ToList());
        }
        // GET: api/Zayavkis/5
        [ResponseType(typeof(Zayavki))]
        public async Task<IHttpActionResult> GetZayavki(int id)
        {
            Zayavki zayavki = await db.Zayavkis.FindAsync(id);
            if (zayavki == null)
            {
                return NotFound();
            }

            return Ok(zayavki);
        }

        // PUT: api/Zayavkis/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutZayavki(int id, Zayavki zayavki)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != zayavki.Id)
            {
                return BadRequest();
            }

            db.Entry(zayavki).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZayavkiExists(id))
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
        
        // POST: api/Zayavkis
        [ResponseType(typeof(Zayavki))]
        public async Task<IHttpActionResult> PostZayavki(Zayavki zayavki)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Zayavkis.Add(zayavki);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = zayavki.Id }, zayavki);
        }

        // DELETE: api/Zayavkis/5
        [ResponseType(typeof(Zayavki))]
        public async Task<IHttpActionResult> DeleteZayavki(int id)
        {
            Zayavki zayavki = await db.Zayavkis.FindAsync(id);
            if (zayavki == null)
            {
                return NotFound();
            }

            db.Zayavkis.Remove(zayavki);
            await db.SaveChangesAsync();

            return Ok(zayavki);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ZayavkiExists(int id)
        {
            return db.Zayavkis.Count(e => e.Id == id) > 0;
        }
    }
}