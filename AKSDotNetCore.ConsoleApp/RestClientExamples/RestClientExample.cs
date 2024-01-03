using AKSDotNetCore.ConsoleApp.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKSDotNetCore.ConsoleApp.RestClientExamples
{
    public class RestClientExample
    {
        private string _blogEndpoint = "https://localhost:7061/api/blog";

        public async Task Run()
        {
            //await Read();
            //await Edit(50);
            //await Edit(22);
            //await Create("rest2 title", "rest author", "rest content");
            await Update(20, "Rest Title", "Test Author", "Test Content");
            //await Delete(25);
        }

        private async Task Read()
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest(_blogEndpoint, Method.Get);
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                List<BlogDataModel> lst = JsonConvert.DeserializeObject<List<BlogDataModel>>(jsonStr)!;
                foreach (BlogDataModel item in lst)
                {
                    Console.WriteLine(item.Blog_Id);
                    Console.WriteLine(item.Blog_Title);
                    Console.WriteLine(item.Blog_Author);
                    Console.WriteLine(item.Blog_Content);
                    Console.WriteLine("________________________");
                }
            }
        }

        private async Task Edit(int id)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"{_blogEndpoint}/{id}", Method.Get);
            var response = await client.ExecuteAsync(request);
            
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                BlogDataModel item = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr)!;

                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
                Console.WriteLine("________________________");

            }
            else
            {
                Console.WriteLine(response.Content!);
            }
        }

        private async Task Create(string title, string author, string content)
        {
            var blog = new BlogDataModel
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };

            RestClient client = new RestClient();
            RestRequest request = new RestRequest(_blogEndpoint, Method.Post);
            request.AddJsonBody(blog);
            var response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content!);
        }

        private async Task Update(int id, string title, string author, string content)
        {
            var blog = new BlogDataModel
            {
                Blog_Id = id,
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };

            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"{_blogEndpoint}/{id}", Method.Put);
            request.AddJsonBody(blog);
            var response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content!);
        }

        private async Task Delete(int id)
        {
            var blog = new BlogDataModel
            {
                Blog_Id = id
            };

            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"{_blogEndpoint}/{id}", Method.Delete);
            request.AddJsonBody(blog);
            var response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content!);
        }
    }
}
