using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKSDotNetCore.RealtimeChartUsingSignalR
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TeamDataModel> Teams { get; set; }
    }

    [Table("Tbl_Team")]
    public class TeamDataModel
    {
        [Key]
        public int Id { get; set; }
        public string TeamName { get; set; }
        public int Score { get; set; } 

    }
}
