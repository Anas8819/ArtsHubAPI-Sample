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
using System.Web;

namespace ArtsHubAPI.Controllers.Custom
{
    [Authorize]
    public class AuctionController : ApiController
    {
        private ArtsDBEntities db = new ArtsDBEntities();

        // GET: api/Auction
        public IQueryable<tbl_Auction> Gettbl_Auction()
        {
            return db.tbl_Auction
                .Include(b => b.tbl_AuctionOrder)
                .Include(b => b.tbl_Bid)
                .Include(b => b.tbl_User);
        }

        // GET: api/Auction/5
        [ResponseType(typeof(tbl_Auction))]
        public IHttpActionResult Gettbl_Auction(int id)
        {
            tbl_Auction tbl_Auction = db.tbl_Auction
                .Include(b => b.tbl_AuctionDetial)
                .Include(b => b.tbl_AuctionOrder)
                .Include(b => b.tbl_Bid)
                .Include(b => b.tbl_User).FirstOrDefault(b => b.AuctionId == id);

            if (tbl_Auction == null)
            {
                return NotFound();
            }

            return Ok(tbl_Auction);
        }

        // PUT: api/Auction/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Auction(int id, tbl_Auction tbl_Auction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Auction.AuctionId)
            {
                return BadRequest();
            }

            db.Entry(tbl_Auction).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_AuctionExists(id))
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

        // POST: api/Auction
        [ResponseType(typeof(tbl_Auction))]
        public IHttpActionResult Posttbl_Auction(tbl_Auction tbl_Auction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_Auction.Add(tbl_Auction);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Auction.AuctionId }, tbl_Auction);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_AuctionExists(int id)
        {
            return db.tbl_Auction.Count(e => e.AuctionId == id) > 0;
        }
    }
}