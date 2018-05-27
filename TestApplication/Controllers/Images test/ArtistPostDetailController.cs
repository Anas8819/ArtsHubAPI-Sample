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
using System.Net.Http;
using Newtonsoft.Json;
using System.Drawing;
using System.IO;
using System.Configuration;
using System.Web.Helpers;

namespace TestApplication.Controllers.Images_test
{
    public class ArtistPostDetailController : Controller
    {
        private ArtsDBEntities db = new ArtsDBEntities();

        //    public Image GetImage()
        //    {
        //        var path = ConfigurationManager.AppSettings["Path"].ToString();
        //        path = path + "\\ArtsHubAPI\\Images\\ArtistPost\\";
        //        path = Server.MapPath("~");
        //        tbl_ArtistPostDetail temp = db.tbl_ArtistPostDetail.FirstOrDefault(b => b.ArtistPostDetailId == 2025);
        //        path = temp.PostImage;
        //        Image image = Image.FromFile(path);
        //        return image;
        //    }
        //    // GET: ArtistPostDetail
        //    public async Task<ActionResult> Index()
        //    {
        //        String s = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;
        //        s = VirtualPathUtility.GetDirectory(Request.Path);
        //        var tbl_ArtistPostDetail = db.tbl_ArtistPostDetail.Include(t => t.tbl_ArtistPost);
        //        //Image image = GetImage();
        //        //ViewBag.Image = image;
        //        String fullAppName = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
        //        String fullAppPath = Path.GetDirectoryName(fullAppName);
        //        fullAppPath = Path.GetDirectoryName(fullAppPath);
        //        fullAppPath = Path.GetDirectoryName(fullAppPath);
        //        return View(await tbl_ArtistPostDetail.ToListAsync());
        //    }

        //    // GET: ArtistPostDetail/Details/5
        //    public async Task<ActionResult> Details(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        tbl_ArtistPostDetail tbl_ArtistPostDetail = await db.tbl_ArtistPostDetail.FindAsync(id);
        //        if (tbl_ArtistPostDetail == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        return View(tbl_ArtistPostDetail);
        //    }

        //    // GET: ArtistPostDetail/Create
        //    public ActionResult Create()
        //    {
        //        ViewBag.PostId = new SelectList(db.tbl_ArtistPost, "PostId", "PostDesc");
        //        return View();
        //    }

        //    // POST: ArtistPostDetail/Create
        //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Create(tbl_ArtistPostDetail tbl_ArtistPostDetail, HttpPostedFileBase file)
        //    {
        //        using (var client = new HttpClient())
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                var temp = ConfigurationManager.AppSettings["Path"].ToString();
        //                temp = temp + "\\ArtsHubAPI\\Images\\ArtistPost";
        //                temp = Server.MapPath("~") + "Photo";
        //                file.SaveAs(temp);
        //                var filename = Path.GetFileName(file.FileName);
        //                var path = Path.Combine(temp + "\\" + filename);
        //                tbl_ArtistPostDetail.PostImage = path;
        //                tbl_ArtistPostDetail.PostId = 1;
        //                db.tbl_ArtistPostDetail.Add(tbl_ArtistPostDetail);
        //                db.SaveChanges();
        //            }
        //            //tbl_ArtistPostDetail.PostId = 1;
        //            //client.BaseAddress = new Uri(baseURL);
        //            //client.DefaultRequestHeaders.Clear();
        //            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //            //HttpResponseMessage responce = await client.PostAsJsonAsync("/api/ArtistPostDetail", tbl_ArtistPostDetail);
        //            //if (responce.IsSuccessStatusCode)
        //            //{
        //            //    var postResponce = responce.Content.ReadAsStringAsync().Result;
        //            //    tbl_ArtistPostDetail result = JsonConvert.DeserializeObject<tbl_ArtistPostDetail>(postResponce);
        //            //}
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    public int A(tbl_ArtistPostDetail tbl_ArtistPostDetail, HttpPostedFileBase file)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var temp = ConfigurationManager.AppSettings["Path"].ToString();
        //            var filename = Path.GetFileName(file.FileName);
        //            var path = Path.Combine(temp + "\\Images\\ArtistPost\\", filename);
        //            file.SaveAs(path);
        //        }
        //        return 0;
        //    }
        //    public byte[] GetImg(Image img)
        //    {
        //        MemoryStream mstImage = new MemoryStream();
        //        img.Save(mstImage, System.Drawing.Imaging.ImageFormat.Jpeg);
        //        Byte[] bytImage = mstImage.GetBuffer();
        //        return bytImage;

        //    }

        //    // GET: ArtistPostDetail/Edit/5
        //    public async Task<ActionResult> Edit(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        tbl_ArtistPostDetail tbl_ArtistPostDetail = await db.tbl_ArtistPostDetail.FindAsync(id);
        //        if (tbl_ArtistPostDetail == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        ViewBag.PostId = new SelectList(db.tbl_ArtistPost, "PostId", "PostDesc", tbl_ArtistPostDetail.PostId);
        //        return View(tbl_ArtistPostDetail);
        //    }

        //    // POST: ArtistPostDetail/Edit/5
        //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<ActionResult> Edit([Bind(Include = "PostId,ArtistPostDetailId,PostImage")] tbl_ArtistPostDetail tbl_ArtistPostDetail)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            db.Entry(tbl_ArtistPostDetail).State = EntityState.Modified;
        //            await db.SaveChangesAsync();
        //            return RedirectToAction("Index");
        //        }
        //        ViewBag.PostId = new SelectList(db.tbl_ArtistPost, "PostId", "PostDesc", tbl_ArtistPostDetail.PostId);
        //        return View(tbl_ArtistPostDetail);
        //    }

        //    // GET: ArtistPostDetail/Delete/5
        //    public async Task<ActionResult> Delete(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        tbl_ArtistPostDetail tbl_ArtistPostDetail = await db.tbl_ArtistPostDetail.FindAsync(id);
        //        if (tbl_ArtistPostDetail == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        return View(tbl_ArtistPostDetail);
        //    }

        //    // POST: ArtistPostDetail/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public async Task<ActionResult> DeleteConfirmed(int id)
        //    {
        //        tbl_ArtistPostDetail tbl_ArtistPostDetail = await db.tbl_ArtistPostDetail.FindAsync(id);
        //        db.tbl_ArtistPostDetail.Remove(tbl_ArtistPostDetail);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    protected override void Dispose(bool disposing)
        //    {
        //        if (disposing)
        //        {
        //            db.Dispose();
        //        }
        //        base.Dispose(disposing);
        //    }
        //}
    }
}