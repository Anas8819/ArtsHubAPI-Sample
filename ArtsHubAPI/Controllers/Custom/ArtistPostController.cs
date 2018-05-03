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
    public class ArtistPostController : ApiController
    {
        private ArtsDBEntities db = new ArtsDBEntities();

        // GET: api/ArtistPost
        public IQueryable<tbl_ArtistPost> Gettbl_ArtistPost()
        {
            return db.tbl_ArtistPost
                .Include(b => b.tbl_User);
        }

        // GET: api/ArtistPost/5
        [ResponseType(typeof(tbl_ArtistPost))]
        public IHttpActionResult Gettbl_ArtistPost(int id)
        {
            tbl_ArtistPost tbl_ArtistPost = db.tbl_ArtistPost
                .Include(b => b.tbl_ArtistPostDetail)
                .Include(b => b.tbl_User).FirstOrDefault(b => b.PostId == id);

            if (tbl_ArtistPost == null)
            {
                return NotFound();
            }

            return Ok(tbl_ArtistPost);
        }

        // PUT: api/ArtistPost/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_ArtistPost(int id, tbl_ArtistPost tbl_ArtistPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_ArtistPost.PostId)
            {
                return BadRequest();
            }

            db.Entry(tbl_ArtistPost).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_ArtistPostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(tbl_ArtistPost);
        }

        // POST: api/ArtistPost
        [ResponseType(typeof(tbl_ArtistPost))]
        public IHttpActionResult Posttbl_ArtistPost(tbl_ArtistPost tbl_ArtistPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_ArtistPost.Add(tbl_ArtistPost);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_ArtistPost.PostId }, tbl_ArtistPost);
        }

        // DELETE: api/ArtistPost/5
        [ResponseType(typeof(tbl_ArtistPost))]
        public IHttpActionResult Deletetbl_ArtistPost(int id)
        {
            tbl_ArtistPost tbl_ArtistPost = db.tbl_ArtistPost.Find(id);
            if (tbl_ArtistPost == null)
            {
                return NotFound();
            }

            db.tbl_ArtistPost.Remove(tbl_ArtistPost);
            db.SaveChanges();

            return Ok(tbl_ArtistPost);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_ArtistPostExists(int id)
        {
            return db.tbl_ArtistPost.Count(e => e.PostId == id) > 0;
        }
    }
}