namespace Solen.Core.Application
{
    public class CommandResponse<T>
    {
        public T Value { get; set; }
    }

    public class CommandResponse : CommandResponse<string>
    {
    }
}