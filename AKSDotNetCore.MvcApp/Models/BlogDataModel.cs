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

    #region ChartJs

    //ColumnChart
    public class ColumnChartModel
    {
        public List<string> Labels { get; set; }

        public List<int> Data { get; set; }
    }

    //ScatterChart
    public class Data
    {
        public double X { get; set; }

        public double Y { get; set; }
    }

    public class ScatterChartModel
    {
        public List<Data> Datas { get; set; }
    }

    //MixedChart
    public class MixedChartModel
    {
        public List<string> Labels { get; set; }

        public List<int> Bdata { get; set; }

        public List<int> Ldata { get; set; }   
    }

    #endregion

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

    #region CanvasJs
    //BarChart
    public class DataPoint
    {
        public int Y { get; set; }

        public string Label { get; set; }  

    }

    public class BarChartModel
    {
        public List<DataPoint> DataPoints { get; set; }
    }

    //SplineChart
    public class SpdataPoint
    {
        public DateTime X { get; set; }

        public Double Y { get; set; }
    }

    public class SplineChartModel
    {
        public List<SpdataPoint> SpdataPoints { get; set; }
    }
    #endregion
}
