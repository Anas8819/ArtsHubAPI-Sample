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
using System.IO;

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
        [ResponseType(typeof(ArtsHubAPI.Models.AuctionConversionModel))]
        public IHttpActionResult Gettbl_AuctionDetial_by_Auction(int id)
        {
            tbl_AuctionDetial tbl_AuctionDetial = db.tbl_AuctionDetial.FirstOrDefault(t => t.AuctionId == id);
            if (tbl_AuctionDetial == null)
            {
                return NotFound();
            }

            byte[] buffer = tbl_AuctionDetial.AuctionImage;
            ArtsHubAPI.Models.AuctionConversionModel UserConversionModel = new Models.AuctionConversionModel();
            string s = System.Text.Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            if (s != null)
            {
                UserConversionModel.AuctionImage = s;
            }
            else
            {
                return BadRequest();
            }
            UserConversionModel.AuctionDetailId = tbl_AuctionDetial.AuctionDetailId;
            UserConversionModel.AuctionId = tbl_AuctionDetial.AuctionId;

            return Ok(tbl_AuctionDetial);
        }

        // PUT: api/AuctionDetial/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_AuctionDetial(int id, tbl_AuctionDetial tbl_AuctionDetial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_AuctionDetial.AuctionDetailId)
            {
                return BadRequest();
            }

            db.Entry(tbl_AuctionDetial).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_AuctionDetialExists(id))
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

        // POST: api/AuctionDetial
        [ResponseType(typeof(ArtsHubAPI.Models.AuctionConversionModel))]
        public IHttpActionResult Posttbl_AuctionDetial(ArtsHubAPI.Models.AuctionConversionModel UserConversionModel)
        {
            tbl_AuctionDetial tbl_UserDetail = new tbl_AuctionDetial();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tbl_UserDetail.AuctionDetailId = UserConversionModel.AuctionDetailId;
            tbl_UserDetail.AuctionId = UserConversionModel.AuctionId;
            byte[] buffer = null;
            using (var ms = new MemoryStream())
            {
                buffer = System.Text.Encoding.UTF8.GetBytes(UserConversionModel.AuctionImage);
            }
            if (buffer != null)
            {
                tbl_UserDetail.AuctionImage = buffer;
            }
            else
            {
                return BadRequest();
            }
            db.tbl_AuctionDetial.Add(tbl_UserDetail);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tbl_AuctionDetialExists(tbl_UserDetail.AuctionId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = tbl_UserDetail.AuctionDetailId }, tbl_UserDetail);
        }

        // DELETE: api/AuctionDetial/5
        [ResponseType(typeof(tbl_AuctionDetial))]
        public IHttpActionResult Deletetbl_AuctionDetial(int id)
        {
            tbl_AuctionDetial tbl_AuctionDetial = db.tbl_AuctionDetial.Find(id);
            if (tbl_AuctionDetial == null)
            {
                return NotFound();
            }

            db.tbl_AuctionDetial.Remove(tbl_AuctionDetial);
            db.SaveChanges();

            return Ok(tbl_AuctionDetial);
        }

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