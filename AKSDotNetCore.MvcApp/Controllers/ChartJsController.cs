using AKSDotNetCore.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AKSDotNetCore.MvcApp.Controllers
{
    public class ChartJsController : Controller
    {
        public IActionResult ColumnChart()
        {
            var model = new ColumnChartModel
            {
                Labels = new List<string>() { "Red", "Blue", "Yellow", "Green", "Purple", "Orange" },
                Data = new List<int>() { 12, 19, 3, 5, 2, 3 }
            };
            return View(model);
        }
    }
}
