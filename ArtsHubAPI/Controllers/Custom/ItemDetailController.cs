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
        [ResponseType(typeof(ArtsHubAPI.Models.ItemConversionModel))]
        public IHttpActionResult Gettbl_ItemDetail_by_Item(int id)
        {
            tbl_ItemDetail tbl_ItemDetail = db.tbl_ItemDetail.FirstOrDefault(t => t.ItemId == id);
            if (tbl_ItemDetail == null)
            {
                return NotFound();
            }
            byte[] buffer = tbl_ItemDetail.ItemImage;
            ArtsHubAPI.Models.ItemConversionModel UserConversionModel = new Models.ItemConversionModel();
            string s = System.Text.Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            if (s != null)
            {
                UserConversionModel.ItemImage = s;
            }
            else
            {
                return BadRequest();
            }
            UserConversionModel.ItemDetailId = tbl_ItemDetail.ItemDetailId;
            UserConversionModel.ItemId = tbl_ItemDetail.ItemId;

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
        [ResponseType(typeof(ArtsHubAPI.Models.ItemConversionModel))]
        public IHttpActionResult Posttbl_ItemDetail(ArtsHubAPI.Models.ItemConversionModel UserConversionModel)
        {

            tbl_ItemDetail tbl_UserDetail = new tbl_ItemDetail();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tbl_UserDetail.ItemDetailId = UserConversionModel.ItemDetailId;
            tbl_UserDetail.ItemId = UserConversionModel.ItemId;
            byte[] buffer = null;
            using (var ms = new MemoryStream())
            {
                buffer = System.Text.Encoding.UTF8.GetBytes(UserConversionModel.ItemImage);
            }
            if (buffer != null)
            {
                tbl_UserDetail.ItemImage = buffer;
            }
            else
            {
                return BadRequest();
            }
            db.tbl_ItemDetail.Add(tbl_UserDetail);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tbl_ItemDetailExists(tbl_UserDetail.ItemId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }



            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //db.tbl_ItemDetail.Add(tbl_ItemDetail);
            //db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_UserDetail.ItemDetailId }, tbl_UserDetail);
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