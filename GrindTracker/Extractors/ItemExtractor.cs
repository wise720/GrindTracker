using Dalamud.Game.Text.SeStringHandling.Payloads;
using System;

namespace GrindTracker.Extractors
{
    public class ItemExtractor : IExtractor{
        private static ushort ItemChatType = 2110;
        private static ushort GatherItemChatType = 2115;
        
        public void extract(Message msg, Tracker tracker)
        {
           
            if ((ushort) msg.Type == ItemChatType || (ushort) msg.Type == GatherItemChatType)
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
                
                Plugin.Log.Debug("Count: {0}", msg.Content.Payloads.Count);
                foreach (var payload in msg.Content.Payloads)
                {
                    Plugin.Log.Debug("payload: {0}", payload);
                }
                String itemName = ((ItemPayload)msg.Content.Payloads[3]).Item?.Name.RawString ?? arr[0];
                Item item = new Item(itemName, (ulong) count);
                tracker.AddItem(item);
                #if DEBUG
                Plugin.ChatGui.Print(string.Format("[DEBUG] Item name: {0} | {1}", itemName, count));
                #endif
                Plugin.Log.Debug("Item name: {0} | {1}", itemName, "count not impleneted");
            }
        }
    }
}