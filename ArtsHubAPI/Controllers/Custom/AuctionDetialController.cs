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
    public class AuctionDetialController : ApiController
    {
        private ArtsDBEntities db = new ArtsDBEntities();

        // GET: api/AuctionDetial
        public IQueryable<tbl_AuctionDetial> Gettbl_AuctionDetial()
        {
            return db.tbl_AuctionDetial;
        }

        //// GET: api/AuctionDetial/5
        //[ResponseType(typeof(tbl_AuctionDetial))]
        //public IHttpActionResult Gettbl_AuctionDetial(int id)
        //{
        //    tbl_AuctionDetial tbl_AuctionDetial = db.tbl_AuctionDetial.Find(id);
        //    if (tbl_AuctionDetial == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(tbl_AuctionDetial);
        //}

        // GET: api/AuctionDetial/5
        [ResponseType(typeof(tbl_AuctionDetial))]
        public IHttpActionResult Gettbl_AuctionDetial_by_Auction(int id)
        {
            tbl_AuctionDetial tbl_AuctionDetial = db.tbl_AuctionDetial.FirstOrDefault(t => t.AuctionId == id);
            if (tbl_AuctionDetial == null)
            {
                return NotFound();
            }

            return Ok(tbl_AuctionDetial);
        }

        //// PUT: api/AuctionDetial/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult Puttbl_AuctionDetial(int id, tbl_AuctionDetial tbl_AuctionDetial)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != tbl_AuctionDetial.AuctionDetailId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(tbl_AuctionDetial).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!tbl_AuctionDetialExists(id))
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

        // POST: api/AuctionDetial
        [ResponseType(typeof(tbl_AuctionDetial))]
        public IHttpActionResult Posttbl_AuctionDetial(tbl_AuctionDetial tbl_AuctionDetial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_AuctionDetial.Add(tbl_AuctionDetial);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_AuctionDetial.AuctionDetailId }, tbl_AuctionDetial);
        }

        //// DELETE: api/AuctionDetial/5
        //[ResponseType(typeof(tbl_AuctionDetial))]
        //public IHttpActionResult Deletetbl_AuctionDetial(int id)
        //{
        //    tbl_AuctionDetial tbl_AuctionDetial = db.tbl_AuctionDetial.Find(id);
        //    if (tbl_AuctionDetial == null)
        //    {
        //        return NotFound();
        //    }

        //    db.tbl_AuctionDetial.Remove(tbl_AuctionDetial);
        //    db.SaveChanges();

        //    return Ok(tbl_AuctionDetial);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_AuctionDetialExists(int id)
        {
            return db.tbl_AuctionDetial.Count(e => e.AuctionDetailId == id) > 0;
        }
    }
}