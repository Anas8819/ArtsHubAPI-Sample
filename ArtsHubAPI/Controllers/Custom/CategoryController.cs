using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;

namespace ArtsHubAPI.Controllers.Custom
{
    public class CategoryController : ApiController
    {
        private ArtsDBEntities db = new ArtsDBEntities();

        // GET: api/Category
        public IQueryable<tbl_Category> Gettbl_Category()
        {
            return db.tbl_Category
                .Include(b => b.tbl_Item);
        }

        // GET: api/Category/5
        [ResponseType(typeof(tbl_Category))]
        public IHttpActionResult Gettbl_Category(int id)
        {
            var tbl_Category = db.tbl_Category.Include(b => b.tbl_Item).FirstOrDefault(e => e.CategoryId == id);
            if (tbl_Category == null)
            {
                return NotFound();
            }

            return Ok(tbl_Category);
        }

        // PUT: api/Category/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Category(int id, tbl_Category tbl_Category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Category.CategoryId)
            {
                return BadRequest();
            }

            db.Entry(tbl_Category).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(tbl_Category);
        }

        // POST: api/Category
        [ResponseType(typeof(tbl_Category))]
        public IHttpActionResult Posttbl_Category(tbl_Category tbl_Category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_Category.Add(tbl_Category);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Category.CategoryId }, tbl_Category);
        }

        // DELETE: api/Category/5
        [ResponseType(typeof(tbl_Category))]
        public IHttpActionResult Deletetbl_Category(int id)
        {
            tbl_Category tbl_Category = db.tbl_Category.Find(id);
            if (tbl_Category == null)
            {
                return NotFound();
            }

            try
            {
                db.tbl_Category.Remove(tbl_Category);
                db.SaveChanges();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok(tbl_Category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_CategoryExists(int id)
        {
            return db.tbl_Category.Count(e => e.CategoryId == id) > 0;
        }
    }
}