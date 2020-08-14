using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace StockMarket.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ChartController : Controller
  {
    private readonly stockdbContext _db;

    public ChartController(stockdbContext db)
    {
      _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      return Json(new { data = await _db.Stocks.ToListAsync() });
    }

    [HttpGet("{tradeCode}")]
    public async Task<IActionResult> GetAll(string tradeCode)
    {
      if (tradeCode == null) tradeCode = "ZEALBANGLA";
      return Json(new { data = await _db.Stocks.Where(stock => stock.TradeCode == tradeCode).ToListAsync() });
    }
  }
}