using System;
using GrindTracker.Utils;

namespace GrindTracker.Extractors
{
    public class GilExtractor : IExtractor
    {
        private static ushort GilChatType = 62;

        
        public void extract(Message msg, Tracker tracker)
        {
            if ((ushort)msg.Type == GilChatType)
            {

                int val = Utils.Utils.ParseInt(msg.Content.TextValue);
                
                tracker.addGil(new Gil((ulong) val));
            }
        }
    }
}