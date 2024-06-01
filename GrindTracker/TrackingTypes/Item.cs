using System;
using System.Text.Json.Serialization;

namespace GrindTracker
{
    public class Item
    {
        [JsonInclude]
        public string Name { get; }
        [JsonInclude]
        public DateTime Time { get; }
        [JsonInclude]
        public ulong Count { get; }

        
        [JsonConstructor]
        public Item(string name, DateTime time, ulong count) =>
            (Name, Time, Count) = (name, time, count);
    
        public Item(string name, ulong count)
        {
           Name = name;
            Time = DateTime.Now;
            Count = count;
        }
    }
}