
using AKSDotNetCore.BlazorWasm.Shared;
using AKSDotNetCore.RestApi.Models;
using MudBlazor;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using static MudBlazor.CategoryTypes;

namespace AKSDotNetCore.BlazorWasm.Pages.Blog
{
    public partial class PageBlog
    {
        private int _pageNo = 1;
        private int _pageSize = 10;

        private BlogListResponseModel? Model;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await List(_pageNo, _pageSize);
            }
        }

        private async Task List(int pageNo, int pageSize)
        {
            _pageNo = pageNo;
            _pageSize = pageSize;
            var result = await HttpClient.GetAsync($"api/Blog/{pageNo}/{pageSize}");
            if (result.IsSuccessStatusCode)
            {
                var jsonStr = await result.Content.ReadAsStringAsync();
                Model = JsonConvert.DeserializeObject<BlogListResponseModel>(jsonStr)!;
                StateHasChanged();
            }
        }

        private async Task PageChanged(int i)
        {
            _pageNo = i;

            await List(_pageNo, 10);
        }

        private async Task Delete(int id)
        {
            var parameters = new DialogParameters<ConfirmDialog>();
            parameters.Add(x => x.Message,
                "Are you sure want to delete?");

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = await DialogService.ShowAsync<ConfirmDialog>("Confirm", parameters, options);
            var result = await dialog.Result;
            if (result.Canceled) return;

            var responce = await HttpClient.DeleteAsync($"api/Blog/{id}");
            if (responce.IsSuccessStatusCode)
            {
                await List(_pageNo, _pageSize);
                Snackbar.Add("Blog deleted successfully", Severity.Success);
            }
        }
    }
}
