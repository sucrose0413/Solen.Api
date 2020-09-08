namespace Solen.Core.Application
{
    public abstract class QueryObject
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int OrderBy { get; set; }
    }
}