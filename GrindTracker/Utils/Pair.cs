using System.Text.Json.Serialization;

namespace GrindTracker.Utils
{
    public class Pair<T,E>
    {
        [JsonInclude]
        public T first;
        [JsonInclude]
        public E second;

        public Pair(T first, E second)
        {
            this.first = first;
            this.second = second;
        }
    }
}