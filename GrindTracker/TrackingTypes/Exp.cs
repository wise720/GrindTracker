using System;
using System.Text.Json.Serialization;

namespace GrindTracker
{
    public class Exp
    {
        
        public ulong Amount { get; private set; }
        
        public DateTime Time { get; private set; }
        
        [JsonConstructor]
        public Exp(  ulong amount, DateTime time) =>
            ( Amount, Time) = ( amount, time);
        public Exp(ulong amount)
        {
            this.Amount = amount;
            this.Time = DateTime.Now;
        }
    }
}