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
    public class ItemDetailController : ApiController
    {
        private ArtsDBEntities db = new ArtsDBEntities();

        // GET: api/ItemDetail
        public IQueryable<tbl_ItemDetail> Gettbl_ItemDetail()
        {
            return db.tbl_ItemDetail;
        }

        //// GET: api/ItemDetail/5
        //[ResponseType(typeof(tbl_ItemDetail))]
        //public IHttpActionResult Gettbl_ItemDetail(int id)
        //{
        //    tbl_ItemDetail tbl_ItemDetail = db.tbl_ItemDetail.Find(id);
        //    if (tbl_ItemDetail == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(tbl_ItemDetail);
        //}

        // GET: api/ItemDetail/5
        [ResponseType(typeof(tbl_ItemDetail))]
        public IHttpActionResult Gettbl_ItemDetail_by_Item(int id)
        {
            tbl_ItemDetail tbl_ItemDetail = db.tbl_ItemDetail.FirstOrDefault(t => t.ItemId == id);
            if (tbl_ItemDetail == null)
            {
                return NotFound();
            }

            return Ok(tbl_ItemDetail);
        }

        //// PUT: api/ItemDetail/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult Puttbl_ItemDetail(int id, tbl_ItemDetail tbl_ItemDetail)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != tbl_ItemDetail.ItemDetailId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(tbl_ItemDetail).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!tbl_ItemDetailExists(id))
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

        // POST: api/ItemDetail
        [ResponseType(typeof(tbl_ItemDetail))]
        public IHttpActionResult Posttbl_ItemDetail(tbl_ItemDetail tbl_ItemDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_ItemDetail.Add(tbl_ItemDetail);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_ItemDetail.ItemDetailId }, tbl_ItemDetail);
        }

        //// DELETE: api/ItemDetail/5
        //[ResponseType(typeof(tbl_ItemDetail))]
        //public IHttpActionResult Deletetbl_ItemDetail(int id)
        //{
        //    tbl_ItemDetail tbl_ItemDetail = db.tbl_ItemDetail.Find(id);
        //    if (tbl_ItemDetail == null)
        //    {
        //        return NotFound();
        //    }

        //    db.tbl_ItemDetail.Remove(tbl_ItemDetail);
        //    db.SaveChanges();

        //    return Ok(tbl_ItemDetail);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_ItemDetailExists(int id)
        {
            return db.tbl_ItemDetail.Count(e => e.ItemDetailId == id) > 0;
        }
    }
}