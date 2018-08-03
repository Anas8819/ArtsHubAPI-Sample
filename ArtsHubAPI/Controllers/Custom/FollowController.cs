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
    [Authorize]
    public class FollowController : ApiController
    {
        private ArtsDBEntities db = new ArtsDBEntities();

        // GET: api/Follow
        public IQueryable<tbl_Follow> Gettbl_Follow()
        {
            return db.tbl_Follow;
        }

        // GET: api/Follow/5
        [Route("api/Follow/{id}/User")]
        [ResponseType(typeof(tbl_Follow))]
        public IHttpActionResult Gettbl_Follow_by_User(int id)
        {
            List<tbl_Follow> tbl_Follow = db.tbl_Follow.Where(t => t.UserId == id).ToList();
            if (tbl_Follow == null)
            {
                return NotFound();
            }

            return Ok(tbl_Follow);
        }

        // GET: api/Follow/5
        [Route("api/Follow/{id}/Artist")]
        [ResponseType(typeof(tbl_Follow))]
        public IHttpActionResult Gettbl_Follow_by_Artist(int id)
        {
            List<tbl_Follow> tbl_Follow = db.tbl_Follow.Where(t => t.ArtistId == id).ToList();
            if (tbl_Follow == null)
            {
                return NotFound();
            }

            return Ok(tbl_Follow);
        }


        //// PUT: api/Follow/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult Puttbl_Follow(int id, tbl_Follow tbl_Follow)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != tbl_Follow.FollowId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(tbl_Follow).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!tbl_FollowExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST: api/Follow
        [ResponseType(typeof(tbl_Follow))]
        public IHttpActionResult Posttbl_Follow(tbl_Follow tbl_Follow)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_Follow.Add(tbl_Follow);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tbl_FollowExists(tbl_Follow.FollowId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tbl_Follow.FollowId }, tbl_Follow);
        }

        // DELETE: api/Follow/5
        [ResponseType(typeof(tbl_Follow))]
        [Route("api/Follow/{id}/Artist")]
        public IHttpActionResult Deletetbl_Follow_by_Artist(int id)
        {
            List<tbl_Follow> tbl_Follow = db.tbl_Follow.Where(t => t.ArtistId == id).ToList();
            if (tbl_Follow == null)
            {
                return NotFound();
            }

            db.tbl_Follow.RemoveRange(tbl_Follow);
            db.SaveChanges();

            return Ok(tbl_Follow);
        }

        // DELETE: api/Follow/5
        [ResponseType(typeof(tbl_Follow))]
        [Route("api/Follow/{id}/Artist")]
        public IHttpActionResult Deletetbl_Follow_by_User(int id)
        {
            List<tbl_Follow> tbl_Follow = db.tbl_Follow.Where(t => t.UserId == id).ToList();
            if (tbl_Follow == null)
            {
                return NotFound();
            }

            db.tbl_Follow.RemoveRange(tbl_Follow);
            db.SaveChanges();

            return Ok(tbl_Follow);
        }

        // DELETE: api/Follow/5
        [ResponseType(typeof(tbl_Follow))]
        [Route("api/Follow/{ArtistId}/Artist/{UserId}/User")]
        public IHttpActionResult Deletetbl_Follow_by_User(int ArtistId, int UserId)
        {
            List<tbl_Follow> tbl_Follow = db.tbl_Follow.Where(t => t.UserId == UserId).Where(t => t.ArtistId == ArtistId).ToList();
            if (tbl_Follow == null)
            {
                return NotFound();
            }

            db.tbl_Follow.RemoveRange(tbl_Follow);
            db.SaveChanges();

            return Ok(tbl_Follow);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_FollowExists(int id)
        {
            return db.tbl_Follow.Count(e => e.FollowId == id) > 0;
        }
    }
}