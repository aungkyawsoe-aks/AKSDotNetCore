using Microsoft.AspNetCore.Mvc;

namespace AKSDotNetCore.MvcApp.Controllers
{
    public class CanvasJsController : Controller
    {
        public IActionResult BarChart()
        {
            return View();
        }
    }
}
