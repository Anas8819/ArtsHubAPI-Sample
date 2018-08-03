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
    public class ComplaintController : ApiController
    {
        private ArtsDBEntities db = new ArtsDBEntities();

    //    // GET: api/Complaint
    //    public IQueryable<tbl_Complaint> Gettbl_Complaint()
    //    {
    //        return db.tbl_Complaint;
    //    }

    //    // GET: api/Complaint/5
    //    [ResponseType(typeof(tbl_Complaint))]
    //    public IHttpActionResult Gettbl_Complaint(int id)
    //    {
    //        tbl_Complaint tbl_Complaint = db.tbl_Complaint.Find(id);
    //        if (tbl_Complaint == null)
    //        {
    //            return NotFound();
    //        }

    //        return Ok(tbl_Complaint);
    //    }

    //    // PUT: api/Complaint/5
    //    [ResponseType(typeof(void))]
    //    public IHttpActionResult Puttbl_Complaint(int id, tbl_Complaint tbl_Complaint)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }

    //        if (id != tbl_Complaint.ComplaintId)
    //        {
    //            return BadRequest();
    //        }

    //        db.Entry(tbl_Complaint).State = EntityState.Modified;

    //        try
    //        {
    //            db.SaveChanges();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!tbl_ComplaintExists(id))
    //            {
    //                return NotFound();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }

    //        return StatusCode(HttpStatusCode.NoContent);
    //    }

        // POST: api/Complaint
        [ResponseType(typeof(tbl_Complaint))]
        public IHttpActionResult Posttbl_Complaint(tbl_Complaint tbl_Complaint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_Complaint.Add(tbl_Complaint);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tbl_ComplaintExists(tbl_Complaint.ComplaintId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tbl_Complaint.ComplaintId }, tbl_Complaint);
        }

        //// DELETE: api/Complaint/5
        //[ResponseType(typeof(tbl_Complaint))]
        //public IHttpActionResult Deletetbl_Complaint(int id)
        //{
        //    tbl_Complaint tbl_Complaint = db.tbl_Complaint.Find(id);
        //    if (tbl_Complaint == null)
        //    {
        //        return NotFound();
        //    }

        //    db.tbl_Complaint.Remove(tbl_Complaint);
        //    db.SaveChanges();

        //    return Ok(tbl_Complaint);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_ComplaintExists(int id)
        {
            return db.tbl_Complaint.Count(e => e.ComplaintId == id) > 0;
        }
    }
}