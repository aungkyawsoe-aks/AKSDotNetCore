using AKSDotNetCore.Services;
using System.Data;
using System.Data.SqlClient;

namespace AKSDotNetCore.ConsoleApp.AdoDotNetCoreExamples
{
    public class AdoDotNetCoreExample2
    {
        private readonly AdoDotNetService _adoDotNetService = new AdoDotNetService(new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "aksDotNetCore",
            UserID = "sa",
            Password = "sa@123"
        });
        public void Run()
        {
            Read();
            //Edit(2);
            //Edit(2000);
            //Create("ado title", "ado author", "ado content");
            //Update(13, "Test Title", "Test Author", "Test Content");
            //Delete(15);
        }

        private void Read()
        {
            string query = @"SELECT [Blog_Id]
      ,[Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content]
  FROM [dbo].[Tbl_Blog]";

            var dt = _adoDotNetService.Query(query);

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine("Id => " + dr["Blog_Id"]);
                Console.WriteLine("Title => " + dr["Blog_Title"]);
                Console.WriteLine("Author => " + dr["Blog_Author"]);
                Console.WriteLine("Content => " + dr["Blog_Content"]);
                Console.WriteLine("------------------------------------");
            }
        }

        private void Edit(int id)
        {
            string query = @"SELECT [Blog_Id]
      ,[Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content]
  FROM [dbo].[Tbl_Blog] where Blog_Id = @Blog_Id";

            List<SqlParameter> lstParameters = new List<SqlParameter>() 
            { 
                new("@Blog_Id", id)
            };

            var dt = _adoDotNetService.Query(query, sqlParameters: lstParameters.ToArray());

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No data Found");
                return;
            }

            DataRow dr = dt.Rows[0];
            Console.WriteLine("Id => " + dr["Blog_Id"]);
            Console.WriteLine("Title => " + dr["Blog_Title"]);
            Console.WriteLine("Author => " + dr["Blog_Author"]);
            Console.WriteLine("Content => " + dr["Blog_Content"]);
            Console.WriteLine("------------------------------------");

        }

        private void Create(string title, string author, string content)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_Content])
     VALUES
           (@Blog_Title
           ,@Blog_Author
           ,@Blog_Content)";

            List<SqlParameter> lstParameters = new List<SqlParameter>()
            {
                new("@Blog_Title", title),
                new("@Blog_Author", author),
                new("@Blog_Content", content),
            };

            int result = _adoDotNetService.Execute(query, sqlParameters: lstParameters.ToArray());

            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            Console.WriteLine(message);
        }

        private void Update(int id, string title, string author, string content)
        {
            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [Blog_Title] = @Blog_Title
      ,[Blog_Author] = @Blog_Author
      ,[Blog_Content] = @Blog_Content
 WHERE Blog_Id = @Blog_Id";

            List<SqlParameter> lstParameters = new List<SqlParameter>()
            {
                new("@Blog_Id", id),
                new("@Blog_Title", title),
                new("@Blog_Author", author),
                new("@Blog_Content", content),
            };

            int result = _adoDotNetService.Execute(query, sqlParameters: lstParameters.ToArray());

            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE Blog_Id = @Blog_Id";

            List<SqlParameter> lstParameters = new List<SqlParameter>()
            {
                new("@Blog_Id", id)
            };

            int result = _adoDotNetService.Execute(query, sqlParameters: lstParameters.ToArray());

            string message = result > 0 ? "Deleting Successful" : "Deleting Failed";
            Console.WriteLine(message);
        }
    }
}
