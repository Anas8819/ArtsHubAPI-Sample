using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;

namespace TestApplication.Controllers
{
    public class CategoryController : Controller
    {
        private ArtsDBEntities db = new ArtsDBEntities();

        // GET: Category
        public async Task<ActionResult> Index()
        {
            return View(await db.tbl_Category.ToListAsync());
        }

        // GET: Category/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Category tbl_Category = await db.tbl_Category.FindAsync(id);
            if (tbl_Category == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Category);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CategoryId,CategoryName,CategoryDesc")] tbl_Category tbl_Category)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Category.Add(tbl_Category);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tbl_Category);
        }

        // GET: Category/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Category tbl_Category = await db.tbl_Category.FindAsync(id);
            if (tbl_Category == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Category);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CategoryId,CategoryName,CategoryDesc")] tbl_Category tbl_Category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Category).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tbl_Category);
        }

        // GET: Category/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Category tbl_Category = await db.tbl_Category.FindAsync(id);
            if (tbl_Category == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tbl_Category tbl_Category = await db.tbl_Category.FindAsync(id);
            db.tbl_Category.Remove(tbl_Category);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
