using ImGuiNET;

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
                ;
            }
        }
    }
}