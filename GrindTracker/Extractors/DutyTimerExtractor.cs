namespace GrindTracker.Extractors
{
    public class DutyTimerExtractor: IExtractor
    {
        private static ushort DutyTimeChatType = 2112;
        public void extract(Message msg, Tracker tracker)
        {
            if ((ushort) msg.Type != DutyTimeChatType )
                return;
            if (!msg.Content.TextValue.Contains("completion time"))
                return;
            Plugin.Log.Debug("time: {0}",  msg.Content.TextValue);
        }
    }
}