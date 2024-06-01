using System;
using System.Linq;

namespace GrindTracker.Extractors
{
    public class ExpExtractor: IExtractor
    {
        private static ushort ExpChatType = 2112;
        public void extract(Message msg, Tracker tracker)
        {
            int exp = 0;
            
            if ((ushort) msg.Type != ExpChatType )
                return;
            String expString = msg.Content.TextValue;
            if (!expString.Contains("experience"))
                return;
            if (expString.Contains('('))
            {
                expString= msg.Content.TextValue.Split("(")[0];
            }
            exp = Utils.Utils.ParseInt(expString);
            tracker.AddExp(new Exp((ulong) exp));
        }
    }
}