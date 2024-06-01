using System;

namespace GrindTracker.Extractors
{
    public class ItemExtractor : IExtractor{
        private static ushort ItemChatType = 2110;
        
        public void extract(Message msg, Tracker tracker)
        {
            if ((ushort) msg.Type == ItemChatType)
            {
                String[] arr = msg.Content.TextValue.Split('\ue0bb');
                arr = arr[1].Split('.');
                Item item = new Item(arr[0], 1);
                tracker.AddItem(item);
                Plugin.Log.Debug("Item name: {0} | {1}", arr[0], "count not impleneted");
            }
        }
    }
}