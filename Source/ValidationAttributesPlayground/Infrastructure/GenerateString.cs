using System.Linq;

namespace ValidationAttributesPlayground.Infrastructure
{
    public static class GenerateString
    {
        public static string WithLength(int length)
        {
            return string.Join("", Enumerable
                .Range(1, length)
                .Select(_ => "a"));
        }
    }
}