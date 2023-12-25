using AKSDotNetCore.RestApi.Models;
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
    }
}
