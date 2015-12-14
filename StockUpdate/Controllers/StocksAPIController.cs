using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using StockUpdate.Models;

namespace StockUpdate.Controllers
{

//// StockAPI controller that CRUD to database

/*
* GET api/StocksAPI             get entry for stock                       GetStocks()
* GET api/StocksAPI/5           get entries for stock                     GetStock(id)
* POST: api/StocksAPI           add stocks entry                          PostStock(stock)
* PUT: api/StocksAPI/5          update entry for stock                    PutStock(id,stock)
* DELETE: api/StocksAPI/5       delete entry for stock                    DeleteStock(id)
*/
    public class StocksAPIController : ApiController
    {
        private StockUpdateContext db = new StockUpdateContext();

        // GET: api/StocksAPI
        public IQueryable<Stock> GetStocks()
        {
            return db.Stocks;
        }

        // GET: api/StocksAPI/5
        [ResponseType(typeof(Stock))]
        public async Task<IHttpActionResult> GetStock(int id)
        {
            // LINQ query, find matching entry for id and return stock
            Stock stock = await db.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock);
        }

        // PUT: api/StocksAPI/5
        [ResponseType(typeof(void))]

        // update an entry i.e. update the entry for specified stock
        public async Task<IHttpActionResult> PutStock(int id, Stock stock)  // listing will be in request body
        {
            if (!ModelState.IsValid)                 // model class validation ok?
            {
                return BadRequest(ModelState);
            }

            if (id != stock.StockReference)
            {
                return BadRequest();
            }

            db.Entry(stock).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();          // commit
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockExists(id))
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

        // POST: api/StocksAPI
        [ResponseType(typeof(Stock))]
        public async Task<IHttpActionResult> PostStock(Stock stock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Stocks.Add(stock);
            await db.SaveChangesAsync();        //commit

            // name of default route in WebApiConfig.cs
            return CreatedAtRoute("DefaultApi", new { id = stock.StockReference }, stock);
        }

        // DELETE: api/StocksAPI/5
        [ResponseType(typeof(Stock))]

        // delete the entry for specified stock
        public async Task<IHttpActionResult> DeleteStock(int id)
        {
            Stock stock = await db.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            db.Stocks.Remove(stock);
            await db.SaveChangesAsync();            // commit

            return Ok(stock);
        }

        //a framework design guideline 
        //Dispose is a pattern to allow your class to have the dispose method 
        // be able to be called multiple times without throwing an exception.
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StockExists(int id)
        {
            return db.Stocks.Count(e => e.StockReference == id) > 0;
        }
    }
}