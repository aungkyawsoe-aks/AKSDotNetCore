namespace AKSDotNetCore.RestApi.Models
{
    public class BlogListResponseModel
    {
        public bool IsEndofPage { get; set; }
        public int pageCount { get; set; }
        public int pageSize { get; set; }
        public int pageNo { get; set; }
        public List<BlogDataModel> Data { get; set; }
    }
}
