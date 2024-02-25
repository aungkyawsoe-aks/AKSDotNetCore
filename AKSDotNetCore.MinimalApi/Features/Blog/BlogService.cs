using AKSDotNetCore.MinimalApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AKSDotNetCore.MinimalApi.Features.Blog
{
    public static class BlogService
    {
        public static IEndpointRouteBuilder UseBlogService(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/blog/{pageNo}/{pageSize}", async ([FromServicesAttribute] AppDbContext dbContext, int pageNo, int pageSize) =>
            {
                return await dbContext.Blogs
                    .AsNoTracking()
                    .OrderByDescending(x => x.Blog_Id)
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            })
            .WithName("GetBlogs")
            .WithOpenApi();

            app.MapGet("/api/blog/{id}", async ([FromServicesAttribute] AppDbContext dbContext, int id) =>
            {
                var item = await dbContext.Blogs
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Blog_Id == id);

                if (item is null)
                {
                    return Results.NotFound("No data found.");
                }
                return Results.Ok(item);
            })
                .WithName("GetBlog")
                .WithOpenApi();

            app.MapPost("/api/blog", async ([FromServicesAttribute] AppDbContext dbContext, BlogDataModel blog) =>
            {
                await dbContext.Blogs.AddAsync(blog);
                var result = await dbContext.SaveChangesAsync();
                var message = result > 0 ? "Saving Successful." : "Saving Failed";
                return Results.Ok(message);
            })
                .WithName("CreateBlog")
                .WithOpenApi();

            app.MapPut("/api/blog/{id}", async ([FromServicesAttribute] AppDbContext dbContext, int id, BlogDataModel blog) =>
            {
                BlogDataModel? item = await dbContext.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);

                if (item is null)
                {
                    return Results.NotFound("No data found.");
                }

                if (string.IsNullOrEmpty(blog.Blog_Title))
                {
                    return Results.BadRequest("Blog Title is required");
                }
                if (string.IsNullOrEmpty(blog.Blog_Author))
                {
                    return Results.BadRequest("Blog Author is required");
                }
                if (string.IsNullOrEmpty(blog.Blog_Content))
                {
                    return Results.BadRequest("Blog Content is required");
                }

                item.Blog_Title = blog.Blog_Title;
                item.Blog_Author = blog.Blog_Author;
                item.Blog_Content = blog.Blog_Content;
                int result = await dbContext.SaveChangesAsync();
                string message = result > 0 ? "Updating Successful" : "Updating Failed";
                return Results.Ok(message);
            })
                .WithName("UpdateBlog")
                .WithOpenApi();

            app.MapPatch("/api/blog/{id}", async ([FromServicesAttribute] AppDbContext dbContext, int id, BlogDataModel blog) =>
            {
                BlogDataModel? item = await dbContext.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);

                if (item is null)
                {
                    return Results.NotFound("No data found.");
                }

                if (!string.IsNullOrEmpty(blog.Blog_Title))
                {
                    item.Blog_Title = blog.Blog_Title;
                }
                if (!string.IsNullOrEmpty(blog.Blog_Author))
                {
                    item.Blog_Author = blog.Blog_Author;
                }
                if (!string.IsNullOrEmpty(blog.Blog_Content))
                {
                    item.Blog_Content = blog.Blog_Content;
                }

                int result = await dbContext.SaveChangesAsync();
                string message = result > 0 ? "Updating Successful" : "Updating Failed";
                return Results.Ok(message);
            })
                .WithName("PatchBlog")
                .WithOpenApi();

            app.MapDelete("/api/blog/{id}", async ([FromServicesAttribute] AppDbContext dbContext, int id) =>
            {
                BlogDataModel? item = await dbContext.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);
                if (item is null)
                {
                    return Results.NotFound("No data found");
                }
                dbContext.Blogs.Remove(item);
                int result = await dbContext.SaveChangesAsync();
                string message = result > 0 ? "Deleting Successful" : "Deleting Failed";
                return Results.Ok(message);
            })
                .WithName("DeleteBlog")
                .WithOpenApi();

            return app;
        }
    }
}
