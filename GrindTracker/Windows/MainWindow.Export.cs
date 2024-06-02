using ImGuiNET;
using System;
using System.IO;
using System.Text;

namespace GrindTracker.Windows
{
    public partial class MainWindow
    {
        private void ExportTab()
        {
            if (ImGui.BeginTabItem("Export##ExportTab"))
            {
                Export();

                ImGui.EndTabItem();
            }
        }

        private void Export()
        {
            if (ImGui.Button("Export exp"))
            {
                string fileName = Path.Join(Plugin.Interface.ConfigDirectory.FullName, $"{DateTime.Now:yyyy-MM-ddTHH:mm:ss}-Exp.csv");
                StringBuilder sb = new ("Amount;Time");
                sb.AppendLine();
                foreach (Exp trackerExp in Plugin.Tracker.Exps)
                {
                    sb.Append(trackerExp.Amount)
                        .Append(';')
                        .Append(trackerExp.Time)
                        .AppendLine();
                }
                File.WriteAllText(fileName, sb.ToString());
            }
            if (ImGui.Button("Export gil"))
            {
                string fileName = Path.Join(Plugin.Interface.ConfigDirectory.FullName, $"{DateTime.Now:yyyy-MM-ddTHH:mm:ss}-Gils.csv");
                StringBuilder sb = new ("Count;Time");
                sb.AppendLine();
                foreach (Gil trackerGil in Plugin.Tracker.Gils)
                {
                    sb.Append(trackerGil.Count)
                        .Append(';')
                        .Append(trackerGil.Time)
                        .AppendLine();
                }
                File.WriteAllText(fileName, sb.ToString());
            }
            if (ImGui.Button("Export items"))
            {
                string fileName = Path.Join(Plugin.Interface.ConfigDirectory.FullName, $"{DateTime.Now:yyyy-MM-ddTHH:mm:ss}-Items.csv");
                StringBuilder sb = new ("Name;Count;Time");
                sb.AppendLine();
                foreach (Item trackerItem in Plugin.Tracker.Items)
                {
                    sb.Append(trackerItem.Name)
                        .Append(';')
                        .Append(trackerItem.Count)
                        .Append(';')
                        .Append(trackerItem.Time)
                        .AppendLine();
                }
                File.WriteAllText(fileName, sb.ToString());
            }
        }
    }
}