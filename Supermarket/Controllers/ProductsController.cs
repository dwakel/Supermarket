using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Supermarket.Models;

namespace Supermarket.Controllers
{
    public class ProductsController : Controller
    {
        private SupermarketContext db = new SupermarketContext();

        private async Task InitializeData() => db = TempData["products"] as SupermarketContext ?? new SupermarketContext();

        // GET: Products
        public async Task<ActionResult> Index()
        {
            try
            {
                await InitializeData().ContinueWith(x => TempData.Keep());
                return View(db.Products.ToList<Product>());
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            await InitializeData().ContinueWith(x => TempData.Keep());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int pId = id ?? new int();
            Product product = db.Products[pId];
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public async Task<ActionResult> Create()
        {
            await InitializeData().ContinueWith(x => TempData.Keep());
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,Description,Price,Code")] Product product)
        {
            await InitializeData().ContinueWith(x => TempData.Keep());
            if (ModelState.IsValid)
            {
                if (TempData["count"] == null)
                    TempData["count"] = 1;
                else
                    TempData["count"] = Convert.ToInt32(TempData["count"]) + 1;

                product.Id = Convert.ToInt32(TempData["count"]);
                db.Products.Add(product);
                TempData["products"] = db;
                TempData.Keep();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            await InitializeData().ContinueWith(x => TempData.Keep());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int pId = (id - 1) ?? new int();
            Product product = db.Products[pId];
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,Price,Code")] Product product)
        {
            await InitializeData().ContinueWith(x => TempData.Keep());
            if (ModelState.IsValid)
            {
                db.Products.Insert(product.Id - 1, product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            await InitializeData().ContinueWith(x => TempData.Keep());
            TempData.Keep();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int pId = (id - 1) ?? new int();
            Product product = db.Products[pId];
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await InitializeData().ContinueWith(x => TempData.Keep());
            TempData.Keep();
            Product product = db.Products[id - 1];
            db.Products.Remove(product);
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}