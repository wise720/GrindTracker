using ImGuiNET;

namespace GrindTracker.Windows
{
    public partial class MainWindow
    {
        private void ItemLogTab()
        {
            if (ImGui.BeginTabItem("ItemLog##ItemLogTab"))
            {
                ItemLog();

                ImGui.EndTabItem();
            }
        }

        private void ItemLog()
        {
            foreach (Item item in Plugin.Tracker.Items)
            {
                ImGui.Text($"{item.Count}x {item.Name} at {item.Time.ToString("f")}");
            }
        }
    }
}