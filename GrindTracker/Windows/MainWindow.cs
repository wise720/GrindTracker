using System;
using System.Numerics;
using Dalamud.Interface.Internal;
using Dalamud.Interface.Utility;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using System.Collections.Generic;

namespace GrindTracker.Windows;

public partial class MainWindow : Window, IDisposable
{
    private Plugin Plugin;

    // We give this window a hidden ID using ##
    // So that the user will see "My Amazing Window" as window title,
    // but for ImGui the ID is "My Amazing Window##With a hidden ID"
    public MainWindow(Plugin plugin)
        : base("Grind Tracker##dutyTracker")
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(200, 100),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };


        Plugin = plugin;
    }

    public void Dispose() { }

    public override void Draw()
    {
        var buttonHeight = ImGui.CalcTextSize("RRRR").Y + (20.0f * ImGuiHelpers.GlobalScale);
        if (ImGui.BeginChild("SubContent", new Vector2(0, -buttonHeight)))
        {
            if (ImGui.BeginTabBar("##DutyTrackerTabBar"))
            {
                MainTab();
                ItemLogTab();
                ExportTab();
                DebugTab();
                ImGui.EndTabBar();
            }
        }
        ImGui.EndChild();
        ImGui.Separator();
        ImGuiHelpers.ScaledDummy(1.0f);
        if (ImGui.BeginChild("BottomBar", new Vector2(0, 0), false, 0))
        {
            if (!Plugin.Tracker.Running)
            {
                if (ImGui.Button("Start Tracker"))
                {
                    Plugin.Tracker.Start();
                }
            }
            else
            {
                if (ImGui.Button("Stop Tracker"))
                {
                    Plugin.Tracker.Stop();
                }
            }

            ImGui.SameLine();
            if (ImGui.Button("Reset Tracker"))
            {
                Plugin.Tracker = new Tracker();
            }
            
        }
        ImGui.EndChild();
    }

   
}
