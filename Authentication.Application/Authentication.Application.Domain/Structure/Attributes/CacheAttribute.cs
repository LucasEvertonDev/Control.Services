namespace Authentication.Application.Domain.Structure.Attributes;

public class CacheAttribute : Attribute
{
    public CacheAttribute(string key, long slidingExpirationInMinutes, long absoluteExpirationInMinutes)
    {
        this.Key = key;
        this.SlidingExpirationInMinutes = slidingExpirationInMinutes;
        this.AbsoluteExpirationInMinutes = absoluteExpirationInMinutes;
    }

    public string Key { get; set; }

    public long? SlidingExpirationInMinutes { get; set; }

    public long? AbsoluteExpirationInMinutes { get; set; }
}
