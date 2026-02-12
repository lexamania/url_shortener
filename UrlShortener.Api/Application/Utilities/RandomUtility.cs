using HashidsNet;

namespace UrlShortener.Api.Application.Utilities;

public static class RandomUtility
{
    const string SALT = "rgbdl30";
    const int MIN_HASH_LENGTH = 6;

    public static string GetRandomHash(long id)
    {
        var hashids = new Hashids(SALT, minHashLength:MIN_HASH_LENGTH);
        return hashids.EncodeLong(id);
    }
}
