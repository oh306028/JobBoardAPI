namespace JobBoardAPI.Models
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalPages { get; set; } 
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }

        public int TotalItemsCount { get; set; }   

        public PagedResult(List<T> items, int PageNumber, int PageSize, int totalPages)
        {
            Items = items;
            TotalItemsCount = totalPages;
            ItemsFrom = PageNumber * (PageSize - 1);
            ItemsTo = ItemsTo + PageSize +1;
            TotalPages = (int)Math.Ceiling(totalPages / (double)PageSize);

        }
    }
}
