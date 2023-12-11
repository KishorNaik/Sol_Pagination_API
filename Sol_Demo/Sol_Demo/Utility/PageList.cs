using Microsoft.EntityFrameworkCore;

namespace Sol_Demo.Utility;

public class PageList<T> : List<T>
{
    public int CurrentPage { get; private set; }
    public int TotalPages { get; private set; }
    public int PageSize { get; private set; }
    public int TotalCount { get; private set; }
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public PageList(List<T> items, int count, int pageNumber, int pageSize)
    {
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        AddRange(items);
    }

    public static async Task<PageList<T>> ToPagedListAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        Task<List<T>> itemsTask = Task.FromResult(source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());
        Task<int> countTask = Task.FromResult(source.Count());

        var counter = await countTask.ConfigureAwait(false);
        var items = await itemsTask.ConfigureAwait(false);

        return new PageList<T>(items, counter, pageNumber, pageSize);
    }
}