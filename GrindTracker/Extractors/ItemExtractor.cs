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
                if (arr.Length < 1)
                {
                    #if DEBUG
                    Plugin.Log.Error("Message Error: {0}", msg.Content.TextValue);
                    #endif
                    return;
                }
                int count = Utils.Utils.ParseInt(arr[0]);
                if (count == 0) count = 1; 
                arr = arr[1].Split('.');
                
                Item item = new Item(arr[0], (ulong) count);
                tracker.AddItem(item);
                #if DEBUG
                Plugin.ChatGui.Print(string.Format("[DEBUG] Item name: {0} | {1}", arr[0], count));
                #endif
                Plugin.Log.Debug("Item name: {0} | {1}", arr[0], "count not impleneted");
            }
        }
    }
}