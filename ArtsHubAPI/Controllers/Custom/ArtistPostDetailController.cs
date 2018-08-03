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

        [ResponseType(typeof(ArtsHubAPI.Models.ArtistPostConversionModel))]
        public IHttpActionResult Gettbl_ArtistPostDetail_by_Post(int id)
        {
            tbl_ArtistPostDetail tbl_ArtistPostDetail = db.tbl_ArtistPostDetail.FirstOrDefault(t => t.PostId == id);

            byte[] buffer = tbl_ArtistPostDetail.PostImage;
            ArtsHubAPI.Models.ArtistPostConversionModel UserConversionModel = new Models.ArtistPostConversionModel();
            string s = System.Text.Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            if (s != null)
            {
                UserConversionModel.PostImage = s;
            }
            else
            {
                return BadRequest();
            }
            UserConversionModel.ArtistPostDetailId = tbl_ArtistPostDetail.ArtistPostDetailId;
            UserConversionModel.PostId = tbl_ArtistPostDetail.PostId;

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
        [ResponseType(typeof(ArtsHubAPI.Models.ArtistPostConversionModel))]
        public IHttpActionResult Posttbl_ArtistPostDetail(ArtsHubAPI.Models.ArtistPostConversionModel UserConversionModel)
        {
            tbl_ArtistPostDetail tbl_UserDetail = new tbl_ArtistPostDetail();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tbl_UserDetail.ArtistPostDetailId = tbl_UserDetail.ArtistPostDetailId;
            tbl_UserDetail.PostId = tbl_UserDetail.PostId;
            byte[] buffer = null;
            using (var ms = new MemoryStream())
            {
                buffer = System.Text.Encoding.UTF8.GetBytes(UserConversionModel.PostImage);
            }
            if (buffer != null)
            {
                tbl_UserDetail.PostImage = buffer;
            }
            else
            {
                return BadRequest();
            }
            db.tbl_ArtistPostDetail.Add(tbl_UserDetail);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tbl_ArtistPostDetailExists(tbl_UserDetail.PostId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = tbl_UserDetail.ArtistPostDetailId }, tbl_UserDetail);
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