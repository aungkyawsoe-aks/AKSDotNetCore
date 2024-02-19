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
    }
}
