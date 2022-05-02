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
    public class AddToBasketsController : Controller
    {
        private DBACME db = new DBACME();

        // GET: AddToBaskets
        public async Task<ActionResult> Index()
        {
            var addToBaskets = db.AddToBaskets.Include(a => a.Basket).Include(a => a.Product);
            return View(await addToBaskets.ToListAsync());
        }

        // GET: AddToBaskets/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddToBasket addToBasket = await db.AddToBaskets.FindAsync(id);
            if (addToBasket == null)
            {
                return HttpNotFound();
            }
            return View(addToBasket);
        }

        // GET: AddToBaskets/Create
        public ActionResult Create()
        {
            ViewBag.BasketID = new SelectList(db.Baskets, "ID", "Invoice");
            ViewBag.ProductID = new SelectList(db.Products, "Reference", "Reference");
            return View();
        }

        // POST: AddToBaskets/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProductID,BasketID,Quantity,Size,Width,Returned")] AddToBasket addToBasket)
        {
            if (ModelState.IsValid)
            {
                db.AddToBaskets.Add(addToBasket);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BasketID = new SelectList(db.Baskets, "ID", "Invoice", addToBasket.BasketID);
            ViewBag.ProductID = new SelectList(db.Products, "Reference", "Reference", addToBasket.ProductID);
            return View(addToBasket);
        }

        // GET: AddToBaskets/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddToBasket addToBasket = await db.AddToBaskets.FindAsync(id);
            if (addToBasket == null)
            {
                return HttpNotFound();
            }
            ViewBag.BasketID = new SelectList(db.Baskets, "ID", "Invoice", addToBasket.BasketID);
            ViewBag.ProductID = new SelectList(db.Products, "Reference", "Reference", addToBasket.ProductID);
            return View(addToBasket);
        }

        // POST: AddToBaskets/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProductID,BasketID,Quantity,Size,Width,Returned")] AddToBasket addToBasket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(addToBasket).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BasketID = new SelectList(db.Baskets, "ID", "Invoice", addToBasket.BasketID);
            ViewBag.ProductID = new SelectList(db.Products, "Reference", "Reference", addToBasket.ProductID);
            return View(addToBasket);
        }

        // GET: AddToBaskets/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddToBasket addToBasket = await db.AddToBaskets.FindAsync(id);
            if (addToBasket == null)
            {
                return HttpNotFound();
            }
            return View(addToBasket);
        }

        // POST: AddToBaskets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            AddToBasket addToBasket = await db.AddToBaskets.FindAsync(id);
            db.AddToBaskets.Remove(addToBasket);
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
