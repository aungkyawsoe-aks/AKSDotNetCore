using AKSDotNetCore.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AKSDotNetCore.MvcApp.Controllers
{
    public class ApexChartController : Controller
    {
        public IActionResult PolarAreaChart()
        {
            var model = new PolarAreaChartModel
            {
                Series = new List<int>() { 14, 23, 21, 17, 15 }
            };
            return View(model);
        }
    }
}
