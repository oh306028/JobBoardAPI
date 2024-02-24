namespace JobBoardAPI.Models
{
    public class QueryModel
    {
        public string searchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }   

    }
}
