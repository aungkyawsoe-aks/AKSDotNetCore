using AKSDotNetCore.RestApi.Models;
using Microsoft.JSInterop;
using MudBlazor;
using Newtonsoft.Json;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace AKSDotNetCore.BlazorWasm.Pages.Blog
{
    public partial class PageBlogCreate
    {
        private BlogDataModel requestModel = new();

        private async Task Save()
        {
            var blogJson = JsonConvert.SerializeObject(requestModel);
            HttpContent content = new StringContent(blogJson, Encoding.UTF8, Application.Json);
            var response = await HttpClient.PostAsync("api/Blog", content);
            if (response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                //await JsRuntime.InvokeVoidAsync("alert", message);
                Snackbar.Add(message, Severity.Success);
                Nav.NavigateTo("/setup/blog");
            }
        }
    }
}
