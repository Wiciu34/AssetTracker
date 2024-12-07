using Microsoft.EntityFrameworkCore;

namespace AssetTracker.Models;

public class PaginatedList<T> : List<T>
{
    public List<T>? Items { get; set; }
    public int TotalItems { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; private set; }

    public PaginatedList(List<T>? items, int count, int pageIndex, int pageSize)
    {
        Items = items;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        PageIndex = pageIndex;
    }

    public bool HasPreviousPage => (PageIndex > 1);
    public bool HasNextPage => (PageIndex < TotalPages);

    public int FirstItemIndex => (PageIndex - 1) * PageSize + 1;
    public int LastItemIndex => Math.Min(PageIndex * PageSize, TotalItems);

    public static PaginatedList<T> Create(IList<T> source, int pageIndex, int pageSize)
    {
        var count = source.Count();
        var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }
}
