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
    public class BidController : ApiController
    {
        private ArtsDBEntities db = new ArtsDBEntities();

        // GET: api/Bid
        public IQueryable<tbl_Bid> Gettbl_Bid()
        {
            return db.tbl_Bid
                .Include(b => b.tbl_Auction)
                .Include(b => b.tbl_User);
        }

        // GET: api/Bid/1009/User
        [HttpGet]
        [Route("api/Bid/{id}/User")]
        [ResponseType(typeof(tbl_Auction))]
        public IHttpActionResult Gettbl_Bid_by_User(int id)
        {
            IQueryable<tbl_Bid> tbl_Bid = db.tbl_Bid
                .Include(b => b.tbl_Auction)
                .Include(b => b.tbl_User).Where(b => b.UserId == id);

            if (tbl_Bid == null)
            {
                return NotFound();
            }

            return Ok(tbl_Bid);
        }

        // GET: api/Bid/1009/Item
        [HttpGet]
        [Route("api/Bid/{id}/Item")]
        [ResponseType(typeof(tbl_Auction))]
        public IHttpActionResult Gettbl_Bid_by_Item(int id)
        {
            IQueryable<tbl_Bid> tbl_Bid = db.tbl_Bid
                .Include(b => b.tbl_Auction)
                .Include(b => b.tbl_User).Where(b => b.AuctionItemId == id);

            if (tbl_Bid == null)
            {
                return NotFound();
            }

            return Ok(tbl_Bid);
        }

        [HttpGet]
        [Route("api/Bid/{id}/Bidder")]
        [ResponseType(typeof(tbl_Auction))]
        public IHttpActionResult Gettbl_Highest_Bidder(int id)
        {
            List<tbl_Bid> tbl_Bid_List = db.tbl_Bid
                .Include(b => b.tbl_Auction)
                .Include(b => b.tbl_User).Where(b => b.AuctionItemId == id).ToList();

            string highest = tbl_Bid_List.Max(t => t.BidPrice);

            tbl_Bid tbl_Bid = tbl_Bid_List.FirstOrDefault(t => t.BidPrice == highest);
            if (tbl_Bid == null)
            {
                return NotFound();
            }

            return Ok(tbl_Bid);
        }

        // POST: api/Bid
        [ResponseType(typeof(tbl_Bid))]
        public IHttpActionResult Posttbl_Bid(tbl_Bid tbl_Bid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_Bid.Add(tbl_Bid);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tbl_BidExists(tbl_Bid.AuctionItemId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tbl_Bid.AuctionItemId }, tbl_Bid);
        }

        // DELETE: api/Bid/5/DeleteItem
        [HttpDelete, Route("api/Bid/DeleteItem/{id}")]
        [ResponseType(typeof(tbl_Bid))]
        public IHttpActionResult Deletetbl_Bid_by_Item(int id)
        {
            List<tbl_Bid> tbl_Bid = db.tbl_Bid.Where(t => t.AuctionItemId == id).ToList();
            if (tbl_Bid == null)
            {
                return NotFound();
            }

            db.tbl_Bid.RemoveRange(tbl_Bid);
            db.SaveChanges();

            return Ok(tbl_Bid);
        }

        // DELETE: api/Bid/5/DeleteUser
        [HttpDelete, Route("api/Bid/DeleteUser/{id}")]
        [ResponseType(typeof(tbl_Bid))]
        public IHttpActionResult Deletetbl_Bid_by_User(int id)
        {
            List<tbl_Bid> tbl_Bid = db.tbl_Bid.Where(t => t.UserId == id).ToList();
            if (tbl_Bid == null)
            {
                return NotFound();
            }

            db.tbl_Bid.RemoveRange(tbl_Bid);
            db.SaveChanges();

            return Ok(tbl_Bid);
        }

        // DELETE: api/Bid/5
        [Route("api/Bid/Delete/{Item_Id}/{User_Id}")]
        [ResponseType(typeof(tbl_Bid))]
        public IHttpActionResult Deletetbl_Bid(int Item_id,int User_id)
        {
            List<tbl_Bid> tbl_Bid = db.tbl_Bid.Where(t => t.AuctionItemId == Item_id).Where(t => t.UserId == User_id).ToList();
            if (tbl_Bid == null)
            {
                return NotFound();
            }

            db.tbl_Bid.RemoveRange(tbl_Bid);
            db.SaveChanges();

            return Ok(tbl_Bid);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_BidExists(int id)
        {
            return db.tbl_Bid.Count(e => e.AuctionItemId == id) > 0;
        }
    }
}