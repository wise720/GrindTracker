using GrindTracker.Utils;
using ImGuiNET;
using System.Collections.Generic;

namespace GrindTracker.Windows
{
    public partial class MainWindow
    {
        private void MainTab()
        {
            if (ImGui.BeginTabItem("Main##mainTab"))
            {
                Main();

                ImGui.EndTabItem();
            }
        }

   

   
        private void Main()
        {
            ImGui.Text($"Time expanded: {Plugin.Tracker.totalTimeSpan().ToString("mm':'ss")}");
            ImGui.Text($"Exp {Plugin.Tracker.TotalExp().ToString("N0")} | {Plugin.Tracker.AverageExp().ToString("F2")}");
            ImGui.Text($"Gil {Plugin.Tracker.TotalGil().ToString("N0")} | {Plugin.Tracker.AverageGil().ToString("F2")}");
            if (ImGui.TreeNode($"Items | {Plugin.Tracker.Items.Count}"))
            {
                foreach (KeyValuePair<string,Pair<int,double>> keyValuePair in Plugin.Tracker.GroupItems())
                {
                    ImGui.Text($"{keyValuePair.Key}: {keyValuePair.Value.first} | {keyValuePair.Value.second.ToString("F2")}");
                }

                ImGui.TreePop();
            }
        
            // if (ImGui.Button("Show Settings"))
            // {
            //     Plugin.ToggleConfigUI();
            // }
        }
    }
}