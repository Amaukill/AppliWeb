using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;
using ORM;

namespace AppliWeb.Controllers
{
    public class BasketsController : Controller
    {
        private DBACME db = new DBACME();

        // GET: Baskets
        public async Task<ActionResult> Index()
        {
            var baskets = db.Baskets.Include(b => b.Authentification);
            return View(await baskets.ToListAsync());
        }

        // GET: Baskets/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Basket basket = await db.Baskets.FindAsync(id);
            if (basket == null)
            {
                return HttpNotFound();
            }
            return View(basket);
        }

        // GET: Baskets/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Authentifications, "ID", "FirstName");
            return View();
        }

        // POST: Baskets/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Purchased,UserID,DateOfPurchased,Invoice,TotalPrice")] Basket basket)
        {
            if (ModelState.IsValid)
            {
                basket.ID = Guid.NewGuid();
                db.Baskets.Add(basket);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Authentifications, "ID", "FirstName", basket.UserID);
            return View(basket);
        }

        // GET: Baskets/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Basket basket = await db.Baskets.FindAsync(id);
            if (basket == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Authentifications, "ID", "FirstName", basket.UserID);
            return View(basket);
        }

        // POST: Baskets/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Purchased,UserID,DateOfPurchased,Invoice,TotalPrice")] Basket basket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(basket).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Authentifications, "ID", "FirstName", basket.UserID);
            return View(basket);
        }

        // GET: Baskets/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Basket basket = await db.Baskets.FindAsync(id);
            if (basket == null)
            {
                return HttpNotFound();
            }
            return View(basket);
        }

        // POST: Baskets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Basket basket = await db.Baskets.FindAsync(id);
            db.Baskets.Remove(basket);
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
