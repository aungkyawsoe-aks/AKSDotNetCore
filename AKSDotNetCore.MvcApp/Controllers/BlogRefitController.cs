using AKSDotNetCore.ConsoleApp.RefitExamples;
using AKSDotNetCore.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace AKSDotNetCore.MvcApp.Controllers
{
    public class BlogRefitController : Controller
    {
        private readonly IBlogApi _blogApi;

        public BlogRefitController(IBlogApi blogApi)
        {
            _blogApi = blogApi;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var lst = new List<BlogDataModel>();
            lst = await _blogApi.GetBlogs();
            return View(lst);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = new BlogDataModel();
            item = await _blogApi.GetBlog(id);
            return View(item);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Save(BlogDataModel model)
        {
            var message = await _blogApi.CreaeBlog(model);
            await Console.Out.WriteLineAsync(message);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id, BlogDataModel model)
        {
            var message = await _blogApi.UpdateBlog(id, model);
            await Console.Out.WriteLineAsync(message);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var message = await _blogApi.DeleteBlog(id);
            await Console.Out.WriteLineAsync(message);
            return RedirectToAction("Index");
        }
    }
}
