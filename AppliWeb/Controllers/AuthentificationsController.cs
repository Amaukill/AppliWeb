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
    public class AuthentificationsController : Controller
    {
        private DBACME db = new DBACME();

        // GET: Authentifications
        public async Task<ActionResult> Index()
        {
            return View(await db.Authentifications.ToListAsync());
        }

        // GET: Authentifications/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Authentification authentification = await db.Authentifications.FindAsync(id);
            if (authentification == null)
            {
                return HttpNotFound();
            }
            return View(authentification);
        }

        // GET: Authentifications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Authentifications/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,FirstName,LastName,Email,Address,Password,Role")] Authentification authentification)
        {
            if (ModelState.IsValid)
            {
                authentification.ID = Guid.NewGuid();
                db.Authentifications.Add(authentification);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(authentification);
        }

        // GET: Authentifications/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Authentification authentification = await db.Authentifications.FindAsync(id);
            if (authentification == null)
            {
                return HttpNotFound();
            }
            return View(authentification);
        }

        // POST: Authentifications/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,FirstName,LastName,Email,Address,Password,Role")] Authentification authentification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(authentification).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(authentification);
        }

        // GET: Authentifications/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Authentification authentification = await db.Authentifications.FindAsync(id);
            if (authentification == null)
            {
                return HttpNotFound();
            }
            return View(authentification);
        }

        // POST: Authentifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Authentification authentification = await db.Authentifications.FindAsync(id);
            db.Authentifications.Remove(authentification);
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
