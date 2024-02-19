using AKSDotNetCore.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace AKSDotNetCore.MvcApp.Controllers
{
    public class BlogRestClientController : Controller
    {
        private readonly RestClient _restClient;

        public BlogRestClientController(RestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<IActionResult> Index()
        {
            var lst = new List<BlogDataModel>();
            RestRequest restRequest = new RestRequest("api/Blog", Method.Get);
            var response = await _restClient.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                lst = JsonConvert.DeserializeObject<List<BlogDataModel>>(jsonStr)!;
            }
            return View(lst);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = new BlogDataModel();
            RestRequest restRequest = new RestRequest($"api/Blog/{id}", Method.Get);
            var response = await _restClient.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                item = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr)!;
            }
            return View(item);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(BlogDataModel model)
        {
            RestRequest request = new RestRequest("api/Blog", Method.Post);
            request.AddJsonBody(model);
            var response = await _restClient.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content!);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, BlogDataModel model)
        {
            RestRequest request = new RestRequest($"api/Blog/{id}", Method.Put);
            request.AddJsonBody(model);
            var response = await _restClient.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content!);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            RestRequest request = new RestRequest($"api/Blog/{id}", Method.Delete);
            var response = await _restClient.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content!);
            }
            return RedirectToAction("Index");
        }
    }
}
