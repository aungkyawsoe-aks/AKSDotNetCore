﻿using AKSDotNetCore.RestApi.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Xml;

namespace AKSDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "aksDotNetCore",
            UserID = "sa",
            Password = "sa@123"
        };
        

        [HttpGet]
        public IActionResult GetBlogs()
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = @"SELECT [Blog_Id]
      ,[Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content]
  FROM [dbo].[Tbl_Blog]";
            SqlCommand command = new SqlCommand(query, connection);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            connection.Close();

            List<BlogDataModel> lst = dt.AsEnumerable().Select(dr => new BlogDataModel
            {
                Blog_Id = Convert.ToInt32(dr["Blog_Id"]),
                Blog_Title = Convert.ToString(dr["Blog_Title"]),
                Blog_Author = Convert.ToString(dr["Blog_Author"]),
                Blog_Content = Convert.ToString(dr["Blog_Content"])
            }).ToList();
            return Ok(lst);
        }


        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = @"SELECT [Blog_Id]
      ,[Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content]
  FROM [dbo].[Tbl_Blog] where Blog_Id = @Blog_Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Id", id);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            connection.Close();

            if (dt.Rows.Count == 0)
            {
                return BadRequest("No data found");
            }

            BlogDataModel blogItem = new BlogDataModel()
            {
                Blog_Id = Convert.ToInt32(dt.Rows[0]["Blog_Id"]),
                Blog_Title = Convert.ToString(dt.Rows[0]["Blog_Title"]),
                Blog_Author = Convert.ToString(dt.Rows[0]["Blog_Author"]),
                Blog_Content = Convert.ToString(dt.Rows[0]["Blog_Content"]),
            };

            return Ok(blogItem);
        }


        [HttpPost]
        public IActionResult CreateBlog(BlogDataModel blog)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_Content])
     VALUES
           (@Blog_Title
           ,@Blog_Author
           ,@Blog_Content)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Title", blog.Blog_Title);
            command.Parameters.AddWithValue("@Blog_Author", blog.Blog_Author);
            command.Parameters.AddWithValue("@Blog_Content", blog.Blog_Content);

            int result = command.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            return Ok(message);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE Blog_Id = @Blog_Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Id", id);

            int result = command.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Deleting Successful" : "Deleting Failed";
            return Ok(message);
        }


        [HttpPut]
        public IActionResult UpdateBlogs(int id, BlogDataModel blog)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            #region Get By Id
            string query = @"SELECT [Blog_Id]
      ,[Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content]
  FROM [dbo].[Tbl_Blog] where Blog_Id = @Blog_Id";

            BlogDataModel? item = connection.Query<BlogDataModel>(query, new BlogDataModel { Blog_Id = id }).FirstOrDefault();

            if (item is null)
            {
                return NotFound("No data found");
            }
            #endregion

            #region Check Required Fields
            if (item is null)
            {
                return NotFound("No data found");
            }

            if (string.IsNullOrEmpty(blog.Blog_Title))
            {
                return BadRequest("Blog Title is required");
            }
            if (string.IsNullOrEmpty(blog.Blog_Author))
            {
                return BadRequest("Blog Author is required");
            }
            if (string.IsNullOrEmpty(blog.Blog_Content))
            {
                return BadRequest("Blog Content is required");
            }
            #endregion

            string queryUpdate = @"UPDATE [dbo].[Tbl_Blog]
   SET [Blog_Title] = @Blog_Title
      ,[Blog_Author] = @Blog_Author
      ,[Blog_Content] = @Blog_Content
 WHERE Blog_Id = @Blog_Id";
            SqlCommand command = new SqlCommand(queryUpdate, connection);
            command.Parameters.AddWithValue("@Blog_Id", id);
            command.Parameters.AddWithValue("@Blog_Title", blog.Blog_Title);
            command.Parameters.AddWithValue("@Blog_Author", blog.Blog_Author);
            command.Parameters.AddWithValue("@Blog_Content", blog.Blog_Content);

            int result = command.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            return Ok(message);
        }


        [HttpPatch]
        public IActionResult PatchBlogs(int id, BlogDataModel blog)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            #region Get By Id
            string query = @"SELECT [Blog_Id]
      ,[Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content]
  FROM [dbo].[Tbl_Blog] where Blog_Id = @Blog_Id";

            BlogDataModel? item = connection.Query<BlogDataModel>(query, new BlogDataModel { Blog_Id = id }).FirstOrDefault();

            if (item is null)
            {
                return NotFound("No data found");
            }
            #endregion

            string conditions = string.Empty;
            if (!string.IsNullOrEmpty(blog.Blog_Title))
            {
                conditions += @"[Blog_Title] = @Blog_Title, ";
            }
            if (!string.IsNullOrEmpty(blog.Blog_Author))
            {
                conditions += @"[Blog_Author] = @Blog_Author, ";
            }
            if (!string.IsNullOrEmpty(blog.Blog_Content))
            {
                conditions += @"[Blog_Content] = @Blog_Content, ";
            }
            if (conditions.Length == 0)
            {
                return BadRequest("Invalid Request");
            }
            conditions = conditions.Substring(0, conditions.Length - 2);

            blog.Blog_Id = id;

            string queryUpdate = $@"UPDATE [dbo].[Tbl_Blog]
   SET {conditions}
 WHERE Blog_Id = @Blog_Id";
            SqlCommand command = new SqlCommand(queryUpdate, connection);
            command.Parameters.AddWithValue("@Blog_Id", id);
            if (!string.IsNullOrEmpty(blog.Blog_Title))
            {
                command.Parameters.AddWithValue("@Blog_Title", blog.Blog_Title);
            }
            if (!string.IsNullOrEmpty(blog.Blog_Author))
            {
                command.Parameters.AddWithValue("@Blog_Author", blog.Blog_Author);
            }
            if (!string.IsNullOrEmpty(blog.Blog_Content))
            {
                command.Parameters.AddWithValue("@Blog_Content", blog.Blog_Content);
            }

            int result = command.ExecuteNonQuery();
            connection.Close();

            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            return Ok(message);
        }
    }
}
