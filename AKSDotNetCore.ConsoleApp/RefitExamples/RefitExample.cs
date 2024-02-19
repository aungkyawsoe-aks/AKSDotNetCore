using AKSDotNetCore.ConsoleApp.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKSDotNetCore.ConsoleApp.RefitExamples
{
    public class RefitExample
    {
        private readonly IBlogApi _gitHubApi = RestService.For<IBlogApi>("https://localhost:7061");
        public async Task Run()
        {
            await Read();
            //await Edit(200);
            //await Edit(24);
            //await Create("refit title", "refit author", "refit content");
            //await Update(28, "Title", "Test Author", "Test Content");
            //await Delete(28);
        }

        private async Task Read()
        {
            var lst = await _gitHubApi.GetBlogs();
            foreach (BlogDataModel item in lst)
            {
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
            }
        }

        private async Task Edit(int id)
        {
            try
            {
                var item = await _gitHubApi.GetBlog(id);
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
            }
            catch (ApiException ex)
            {
                Console.WriteLine(ex.ReasonPhrase!.ToString());
                Console.WriteLine(ex.Content!.ToString());
            }
        }

        private async Task Create(string title, string author, string content)
        {
            var message = await _gitHubApi.CreaeBlog(new BlogDataModel
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            });
            await Console.Out.WriteLineAsync(message);
        }

        private async Task Update(int id, string title, string author, string content)
        {
            var message = await _gitHubApi.UpdateBlog(id, new BlogDataModel
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            });
            await Console.Out.WriteLineAsync(message);
        }

        private async Task Delete(int id)
        {
            var message = await _gitHubApi.DeleteBlog(id);
            await Console.Out.WriteLineAsync(message);
        }
    }
}
