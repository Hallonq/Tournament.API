namespace Tournament.Core.Entities;
public class PaginationParameters
{
    public int TotalPages { get; set; }

    private int pageSize = 20;
    public int PageSize
    {
        get {  return pageSize; }
        set
        {
            if (value > 100)
            {
                pageSize = 100;
            }
            else
            {
                pageSize = value;
            }
        }
    }
    public int CurrentPage { get; set; } = 1;
    public int TotalItems { get; set; }
}
