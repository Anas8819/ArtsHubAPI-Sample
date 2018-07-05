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
    public class ArtistPostDetailController : ApiController
    {
        private ArtsDBEntities db = new ArtsDBEntities();

        // GET: api/ArtistPostDetail
        public IQueryable<tbl_ArtistPostDetail> Gettbl_ArtistPostDetail()
        {
            return db.tbl_ArtistPostDetail;
        }

        //// GET: api/ArtistPostDetail/5
        //[ResponseType(typeof(tbl_ArtistPostDetail))]
        //public IHttpActionResult Gettbl_ArtistPostDetail(int id)
        //{
        //    tbl_ArtistPostDetail tbl_ArtistPostDetail = db.tbl_ArtistPostDetail.Find(id);
        //    if (tbl_ArtistPostDetail == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(tbl_ArtistPostDetail);
        //}

        [ResponseType(typeof(tbl_ArtistPostDetail))]
        public IHttpActionResult Gettbl_ArtistPostDetail_by_Post(int id)
        {
            tbl_ArtistPostDetail tbl_ArtistPostDetail = db.tbl_ArtistPostDetail.FirstOrDefault(t => t.PostId == id);
            if (tbl_ArtistPostDetail == null)
            {
                return NotFound();
            }

            return Ok(tbl_ArtistPostDetail);
        }

        //// PUT: api/ArtistPostDetail/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult Puttbl_ArtistPostDetail(int id, tbl_ArtistPostDetail tbl_ArtistPostDetail)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != tbl_ArtistPostDetail.ArtistPostDetailId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(tbl_ArtistPostDetail).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!tbl_ArtistPostDetailExists(id))
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

        // POST: api/ArtistPostDetail
        [ResponseType(typeof(tbl_ArtistPostDetail))]
        public IHttpActionResult Posttbl_ArtistPostDetail(tbl_ArtistPostDetail tbl_ArtistPostDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_ArtistPostDetail.Add(tbl_ArtistPostDetail);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_ArtistPostDetail.ArtistPostDetailId }, tbl_ArtistPostDetail);
        }

        //// DELETE: api/ArtistPostDetail/5
        //[ResponseType(typeof(tbl_ArtistPostDetail))]
        //public IHttpActionResult Deletetbl_ArtistPostDetail(int id)
        //{
        //    tbl_ArtistPostDetail tbl_ArtistPostDetail = db.tbl_ArtistPostDetail.Find(id);
        //    if (tbl_ArtistPostDetail == null)
        //    {
        //        return NotFound();
        //    }

        //    db.tbl_ArtistPostDetail.Remove(tbl_ArtistPostDetail);
        //    db.SaveChanges();

        //    return Ok(tbl_ArtistPostDetail);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_ArtistPostDetailExists(int id)
        {
            return db.tbl_ArtistPostDetail.Count(e => e.ArtistPostDetailId == id) > 0;
        }
    }
}