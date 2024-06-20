using GrindTracker.Utils;
using ImGuiNET;
using Lumina.Excel.GeneratedSheets;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace GrindTracker.Windows
{
    public partial class MainWindow
    {
        private void DebugTab()
        {
            if (ImGui.BeginTabItem("Debug##DebugTab"))
            {
                Debug();

                ImGui.EndTabItem();
            }
        }

        private void Debug()
        {
            ImGui.TextWrapped("These might break the current Tracker use with care");
            if(ImGui.Button("Load Test Data"))
            {
                Plugin.Tracker.addGil(new Gil(1000));
                Plugin.Tracker.addGil(new Gil(234));
                Plugin.Tracker.AddExp(new Exp(1000));
                Plugin.Tracker.AddExp(new Exp(234));
                Item[] items = new[] { new Item("Test1", 2), new Item("Test2", 1), new Item("Test3", 2)   };
                foreach (Item item in items)
                {
                    Plugin.Tracker.AddItem(item);
                }
            }

            if (ImGui.Button("Set Time to 1h"))
            {
                
                DateTime date = DateTime.Now;
                List<Pair<DateTime, DateTime?>> tss = [new Pair<DateTime, DateTime?>(date, date + new TimeSpan(0,1,0,0))];

                PropertyInfo prop = Plugin.Tracker.GetType().GetProperty("Timespans", BindingFlags.NonPublic | BindingFlags.Instance )!;

                prop.SetValue(Plugin.Tracker, tss);

            }
            

        
        }
    }
}