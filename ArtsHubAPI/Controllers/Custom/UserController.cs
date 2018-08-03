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
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace ArtsHubAPI.Controllers.Custom
{
    [Authorize]
    public class UserController : ApiController
    {
        private ArtsDBEntities db = new ArtsDBEntities();

        // GET: api/User
        public IQueryable<tbl_User> Gettbl_User()
        {
            return db.tbl_User
                .Include(b => b.tbl_AuctionOrder)
                .Include(b => b.tbl_Bid)
                .Include(b => b.tbl_Shipping)
                .Include(b => b.tbl_ItemOrder);
        }

        
        [HttpGet]
        [Route("api/User/UserData")]
        public async Task<string> UserData([FromUri]string userName)
        {
            var UserManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            try
            {
                var user = await UserManager.FindByEmailAsync(userName);
                return user.Id;
            }
            catch
            {
                return "0000";
            }
        }

        [HttpGet]
        [Route("api/User/SetRole")]
        public void SetRole(string userID,string roleID)
        {
            var UserManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            UserManager.AddToRole(userID, roleID);
            db.SaveChanges();
        }

        // GET: api/User/5
        [ResponseType(typeof(tbl_User))]
        public IHttpActionResult Gettbl_User(int id)
        {
            tbl_User tbl_User = db.tbl_User.Include(b => b.tbl_AuctionOrder)
                .Include(b => b.tbl_Bid)
                .Include(b => b.tbl_UserDetail)
                .Include(b => b.tbl_Shipping)
                .Include(b => b.tbl_ItemOrder)
                .Include(b=>b.tbl_ArtistPost)
                .SingleOrDefault(b => b.UserId == id);

            if (tbl_User == null)
            {
                return NotFound();
            }

            return Ok(tbl_User);
        }

        [ResponseType(typeof(tbl_User))]
        public IHttpActionResult Gettbl_User_by_Role(char role)
        {
            List<tbl_User> tbl_User = db.tbl_User.Where(t => t.Role == role.ToString()).ToList();

            if (tbl_User == null)
            {
                return NotFound();
            }

            return Ok(tbl_User);
        }

        // PUT: api/User/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_User(int id, tbl_User tbl_User)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_User.UserId)
            {
                return BadRequest();
            }

            db.Entry(tbl_User).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_UserExists(id))
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

        // POST: api/User
        [ResponseType(typeof(tbl_User))]
        public IHttpActionResult Posttbl_User(tbl_User tbl_User)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_User.Add(tbl_User);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_User.UserId }, tbl_User);
        }

        // DELETE: api/User/5
        [ResponseType(typeof(tbl_User))]
        public IHttpActionResult Deletetbl_User(int id)
        {
            tbl_User tbl_User = db.tbl_User.Find(id);
            if (tbl_User == null)
            {
                return NotFound();
            }

            db.tbl_User.Remove(tbl_User);
            db.SaveChanges();

            return Ok(tbl_User);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_UserExists(int id)
        {
            return db.tbl_User.Count(e => e.UserId == id) > 0;
        }
    }
}