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
using System.Threading.Tasks;
using static ArtsHubAPI.Models.UserConversionModel;
using System.IO;

namespace ArtsHubAPI.Controllers.Custom
{
    [Authorize]
    public class UserDetailController : ApiController
    {
        private ArtsDBEntities db = new ArtsDBEntities();

        // GET: api/UserDetail
        public IQueryable<tbl_UserDetail> Gettbl_UserDetail()
        {
            return db.tbl_UserDetail;
        }

        // GET: api/UserDetail/5
        [ResponseType(typeof(ArtsHubAPI.Models.UserConversionModel))]
        public IHttpActionResult Gettbl_UserDetail_by_User(int id)
        {
            tbl_UserDetail tbl_UserDetail = db.tbl_UserDetail.FirstOrDefault(t => t.UserId == id);
            byte[] buffer = tbl_UserDetail.UserImage;
            ArtsHubAPI.Models.UserConversionModel UserConversionModel = new Models.UserConversionModel();
            string s = System.Text.Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            if (s != null)
            {
                UserConversionModel.UserImage = s;
            }
            else
            {
                return BadRequest();
            }
            UserConversionModel.Address = tbl_UserDetail.Address;
            UserConversionModel.Email= tbl_UserDetail.Email;
            UserConversionModel.Phone = tbl_UserDetail.Phone;
            UserConversionModel.UserId = tbl_UserDetail.UserId;
            if (tbl_UserDetail == null)
            {
                return NotFound();
            }

            return Ok(UserConversionModel);
        }

        // PUT: api/UserDetail/5
        [ResponseType(typeof(ArtsHubAPI.Models.UserConversionModel))]
        public IHttpActionResult Puttbl_UserDetail(int id, ArtsHubAPI.Models.UserConversionModel UserConversionModel)
        {
            tbl_UserDetail tbl_UserDetail = new tbl_UserDetail();
            tbl_UserDetail.Address = UserConversionModel.Address;
            tbl_UserDetail.Email = UserConversionModel.Email;
            tbl_UserDetail.Phone = UserConversionModel.Phone;
            tbl_UserDetail.UserId = UserConversionModel.UserId;
            byte[] buffer = null;
            using (var ms = new MemoryStream())
            {
                buffer = System.Text.Encoding.UTF8.GetBytes(UserConversionModel.UserImage);
            }
            if (buffer != null)
            {
                tbl_UserDetail.UserImage = buffer;
            }
            else
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_UserDetail.UserId)
            {
                return BadRequest();
            }

            db.Entry(tbl_UserDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_UserDetailExists(id))
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

        //POST: api/UserDetail
       [ResponseType(typeof(ArtsHubAPI.Models.UserConversionModel))]
        public IHttpActionResult Posttbl_UserDetail(ArtsHubAPI.Models.UserConversionModel UserConversionModel)
        {
            tbl_UserDetail tbl_UserDetail = new tbl_UserDetail();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tbl_UserDetail.Address = UserConversionModel.Address;
            tbl_UserDetail.Email = UserConversionModel.Email;
            tbl_UserDetail.Phone = UserConversionModel.Phone;
            tbl_UserDetail.UserId = UserConversionModel.UserId;
            byte[] buffer = null;
            using (var ms = new MemoryStream())
            {
                buffer = System.Text.Encoding.UTF8.GetBytes(UserConversionModel.UserImage);
            }
            if (buffer != null)
            {
                tbl_UserDetail.UserImage = buffer;
            }
            else
            {
                return BadRequest();
            }
            db.tbl_UserDetail.Add(tbl_UserDetail);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tbl_UserDetailExists(tbl_UserDetail.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tbl_UserDetail.UserId }, tbl_UserDetail);
        }

        //// DELETE: api/UserDetail/5
        //[ResponseType(typeof(tbl_UserDetail))]
        //public IHttpActionResult Deletetbl_UserDetail(int id)
        //{
        //    tbl_UserDetail tbl_UserDetail = db.tbl_UserDetail.Find(id);
        //    if (tbl_UserDetail == null)
        //    {
        //        return NotFound();
        //    }

        //    db.tbl_UserDetail.Remove(tbl_UserDetail);
        //    db.SaveChanges();

        //    return Ok(tbl_UserDetail);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_UserDetailExists(int id)
        {
            return db.tbl_UserDetail.Count(e => e.UserId == id) > 0;
        }
    }
}