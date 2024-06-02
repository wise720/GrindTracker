using GrindTracker.Utils;
using ImGuiNET;
using System;
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
            TimeSpan ts = Plugin.Tracker.totalTimeSpan();
            ImGui.Text($"Time elapsed: {((int) ts.TotalMinutes).ToString("D2")}:{ts.Seconds.ToString("D2")}");
            ImGui.Text($"Exp {Plugin.Tracker.TotalExp().ToString("N0")} | {Plugin.Tracker.AverageExp().ToString("F2")}/min");
            ImGui.Text($"Gil {Plugin.Tracker.TotalGil().ToString("N0")} | {Plugin.Tracker.AverageGil().ToString("F2")}/min");
            if (ImGui.TreeNode($"Items | {Plugin.Tracker.Items.Count}"))
            {
                foreach (KeyValuePair<string,Pair<int,double>> keyValuePair in Plugin.Tracker.GroupItems())
                {
                    ImGui.Text($"{keyValuePair.Key}: {keyValuePair.Value.first} | {keyValuePair.Value.second.ToString("F2")}/min");
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