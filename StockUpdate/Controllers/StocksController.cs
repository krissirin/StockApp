using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using StockUpdate.Models;   //stock

// This MVC Controller, using Scaffolding with actions and Razor views to perform CRUD operations
//// Stock MVC controller that CRUD to database

/*
* GET: Stocks               get entry for stock                         Index()
* GET: Stocks/Details/5     get entries for stock                       Details(id)
* GET: Stocks/Create        add entry for stock                         Create()
* POST: Stocks/Create       add entries for stock                       Create(stock)
* GET: Stocks/Edit/5        update entry for stock                      Edit(id)
* GET: Stocks/Delete/5      delete entry for stock                      Delete(id)
* POST: Stocks/Edit/5       update entry for stock                      Edit(stock)
* POST: Stocks/Delete/5     delete entry for stock                      DeleteConfirmed(id)
*/

namespace StockUpdate.Controllers
{
    public class StocksController : Controller
    {
        private StockUpdateContext db = new StockUpdateContext();

        // GET ../Home/Index - default route
        public async Task<ActionResult> Index()
        {
            return View(await db.Stocks.ToListAsync());
        }

   
        // GET: Stocks/Details/5
        // Display details of stocks just entered 
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = await db.Stocks.FindAsync(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // GET: Stocks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "StockReference,Ticker,StockName,Price")] Stock stock)
        {
            // if client side validation in place then will have been completed prior to POST
            if (ModelState.IsValid)
            {
                db.Stocks.Add(stock);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");  // redirect to "Index" action on this controller
            }

            return View(stock);
        }

        // GET: Stocks/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = await db.Stocks.FindAsync(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "StockReference,Ticker,StockName,Price")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stock).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");   // redirect to "Index" action on this controller
            }
            return View(stock);
        }

        // GET: Stocks/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = await db.Stocks.FindAsync(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Stock stock = await db.Stocks.FindAsync(id);
            db.Stocks.Remove(stock);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //a framework design guideline 
        //Dispose is a pattern to allow your class to have the dispose method 
        //be able to be called multiple times without throwing an exception.
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
