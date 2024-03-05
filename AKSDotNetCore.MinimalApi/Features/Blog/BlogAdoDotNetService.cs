using AKSDotNetCore.MinimalApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using AKSDotNetCore.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AKSDotNetCore.MinimalApi.Features.Blog
{
    public static class BlogAdoDotNetService
    {
        public static IEndpointRouteBuilder UseBlogService(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/blog/{id}", ([FromServicesAttribute] AdoDotNetService _adoDotNetService, int id) =>
            {
                string query = @"SELECT [Blog_Id]
      ,[Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content]
  FROM [dbo].[Tbl_Blog] where Blog_Id = @Blog_Id";

                DataTable dt = _adoDotNetService
                .Query(query, sqlParameters: new SqlParameter("@Blog_Id", id));

                List<BlogDataModel> lst = dt.AsEnumerable().Select(dr => new BlogDataModel
                {
                    Blog_Id = Convert.ToInt32(dr["Blog_Id"])!,
                    Blog_Title = Convert.ToString(dr["Blog_Title"])!,
                    Blog_Author = Convert.ToString(dr["Blog_Author"])!,
                    Blog_Content = Convert.ToString(dr["blog_content"])!
                }).ToList();

                return Results.Ok(lst);
            });

            app.MapPost("/api/blog", ([FromServicesAttribute] AdoDotNetService _adoDotNetService, BlogDataModel blog) =>
            {
                string query = @"INSERT INTO [dbo].[Tbl_blog]
    ([Blog_Title]
    ,[Blog_Author]
    ,[Blog_Content])
VALUES (@Blog_Title
           ,@Blog_Author
           ,@Blog_Content)";
                List<SqlParameter> lstParameters = new()
                {
                    new("@Blog_Title", blog.Blog_Title),
                    new("@Blog_Author", blog.Blog_Author),
                    new("@Blog_Content", blog.Blog_Content)
                };
                int result = _adoDotNetService.Execute(query, sqlParamters: lstParameters.ToArray());

                string message = result > 0 ? "Create Successfully!" : "Create failed!";

                return Results.Ok(message);
            });

            app.MapPut("/api/blog/{id}", ([FromServicesAttribute] AdoDotNetService _adoDotNetService, BlogDataModel blog, int id) =>
            {
                string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [Blog_Title] = @Blog_Title
      ,[Blog_Author] = @Blog_Author
      ,[Blog_Content] = @Blog_Content
 WHERE Blog_Id = @Blog_Id";

                List<SqlParameter> lstParameters = new()
                {
                    new("@Blog_Title", blog.Blog_Title),
                    new("@Blog_Author", blog.Blog_Author),
                    new("@Blog_Content", blog.Blog_Content)
                };

                int result = _adoDotNetService.Execute(query, sqlParamters: lstParameters.ToArray());

                string message = result > 0 ? "Updating Successful" : "Updating Failed";
                return Results.Ok(message);
            });

            app.MapDelete("/api/blog/{id}", ([FromServicesAttribute] AdoDotNetService _adoDotNetService,int id) =>
            {
                string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE Blog_Id = @Blog_Id";

                List<SqlParameter> lstParameters = new() { new("@Blog_Id", id) };

                int result = _adoDotNetService.Execute(query, sqlParamters: lstParameters.ToArray());
                string message = result > 0 ? "Deleting Successful" : "Deleting Failed";
                return Results.Ok(message);
            });

            return app;
        }
    }
}
