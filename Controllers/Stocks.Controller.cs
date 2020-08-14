using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace StocksVisualizer.Controllers
{
  [Route("api/Stock")]
  [ApiController]
  public class StockController : Controller
  {
    private readonly stockdbContext _db;

    public StockController(stockdbContext db)
    {
      _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      return Json(new { data = await _db.Stocks.ToListAsync() });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
      var OldStock = await _db.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);
      if (OldStock == null)
      {
        return Json(new { success = false, message = "Error while Deleting!" });
      }
      _db.Stocks.Remove(OldStock);
      await _db.SaveChangesAsync();
      return Json(new { success = true, message = "Deletion successful!" });
    }
  }
}