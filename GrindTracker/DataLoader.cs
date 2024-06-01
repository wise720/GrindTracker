
using Dalamud.Plugin;
using System.IO;
using System.Text.Json;

namespace GrindTracker
{
    public class DataLoader
    {
        string DataPath
        {
            get
            {
                return Path.Join(Plugin.Interface.ConfigDirectory.FullName, "Tracker.json");
            }
        }

        

        public void loadData(Plugin plugin)
        {
            string fileName = DataPath;
            if (!File.Exists(fileName)) 
                return;
            string jsonString = File.ReadAllText(fileName);
            
            Tracker? tracker =JsonSerializer.Deserialize<Tracker>(jsonString);
            Plugin.Log.Debug("No saved Tracker found");
            if (tracker == null)
                return;
            
            plugin.Tracker = tracker;
        }

        public void saveData(Tracker tracker)
        {
            string fileName = DataPath;
            string jsonString = JsonSerializer.Serialize(tracker);
            File.WriteAllText(fileName, jsonString);

        }
        
    }
}