using AKSDotNetCore.ConsoleApp.RefitExamples;
using AKSDotNetCore.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AKSDotNetCore.MvcApp.Controllers
{
    public class BlogRefitController : Controller
    {
        private readonly IBlogApi __blogApi;

        public BlogRefitController(IBlogApi blogApi)
        {
            __blogApi = blogApi;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var lst = new List<BlogDataModel>();
            lst = await __blogApi.GetBlogs();
            return View(lst);
        }
    }
}
