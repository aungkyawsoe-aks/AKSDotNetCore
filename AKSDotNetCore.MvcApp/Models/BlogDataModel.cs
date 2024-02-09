using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKSDotNetCore.MvcApp.Models
{
    [Table("Tbl_Blog")]
    public class BlogDataModel
    {
        [Key]
        public int Blog_Id { get; set; }
        public string? Blog_Title { get; set; }
        public string? Blog_Author { get; set; }
        public string? Blog_Content { get; set; }

    }

    public class PolarAreaChartModel
    {
        public List<int> Series { get; set;}
    }

    public class ColumnChartModel
    {
        public List<string> Labels { get; set;}

        public List<int> Data {  get; set;}
    }

    #region HighCharts
    public class WithDataLabelsChartModel
    {
        public List<string> Categories { get; set; }

        public List<double> RegData { get; set; }

        public List<double> TalData { get; set; }
    }

    public class RadialBarChartModel
    {
        public List<string> Categories { get; set; }

        public List<int> Gdata { get; set; }

        public List<int> Sdata { get; set; }

        public List<int> Bdata { get; set; }
    }

    #endregion

}
