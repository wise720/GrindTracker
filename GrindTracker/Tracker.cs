

using Dalamud.Game.Text;
using GrindTracker.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace GrindTracker
{
    public class Tracker
    {
        [JsonInclude]
        private List<Pair<DateTime, DateTime?>> Timespans { get; set; }
        [JsonInclude]
        public List<Item> Items { get; private set; }
        [JsonInclude]
        private List<Exp> Exps { get;  set; }
        [JsonInclude]
        private List<Gil> Gils { get;  set; }
     

        public Tracker()
        {
            
            Timespans = new List<Pair<DateTime, DateTime?>>();
            Items = new List<Item>();
            Exps = new List<Exp>();
            Gils = new List<Gil>();
            Start();
        }

        public void Start()
        {
            if (Timespans.Count > 0 && Timespans.Last().second == null)
            {
                return;
            }
            Plugin.ChatGui.Print("Tracker Started");
            Timespans.Add(new Pair<DateTime, DateTime?>(DateTime.Now, null));
        }

        public void AddItem(Item item)
        {
            Items.Add(item);
            Save();
        }

        public void addGil(Gil gil)
        {
            Gils.Add(gil);
            Save();
        }

        public void AddExp(Exp exp)
        {
            Exps.Add(exp);
            Save();
        }

        public void Stop()
        {
            Plugin.ChatGui.Print("Tracker Stopped");
            Timespans.Last().second = DateTime.Now;
        }

        public ulong TotalExp()
        {
            return Exps.Aggregate(0UL, (a,exp) => a+exp.Amount);
        }

        public Dictionary<string, Pair<int, double>> GroupItems()
        {
            Dictionary<string, Pair<int, double>> itemDict = new Dictionary<string, Pair<int, double>>();
            foreach (Item item in Items)
            {
                if (!itemDict.ContainsKey(item.Name))
                {
                    itemDict[item.Name] =new Pair<int, double>(0,0);
                }

                itemDict[item.Name].first += (int) item.Count;
            }

            foreach (KeyValuePair<string,Pair<int,double>> keyValuePair in itemDict)
            {
                keyValuePair.Value.second = keyValuePair.Value.first / totalTimeSpan().TotalMinutes;
            }

            return itemDict;
        }

        public ulong TotalGil()
        {
            return Gils.Aggregate(0UL,(a,gil) => a+gil.Count);
        }

        public double AverageGil()
        {
            return TotalGil() / totalTimeSpan().TotalMinutes;
        }

        public void Print()
        {
            Dictionary<string, Pair<int, double>> itemDict = GroupItems();
            StringBuilder builder = new StringBuilder();

            //builder.Append($"{startTime} - {endTime}").AppendLine();
            builder.Append($"Tracker time {totalTimeSpan().ToString("mm':'ss")}:").AppendLine();
            foreach (KeyValuePair<string,Pair<int,double>> keyValuePair in itemDict)
            {
                builder.Append($"Item {keyValuePair.Key}: {keyValuePair.Value.first} | {keyValuePair.Value.second.ToString("F2")}");
                builder.AppendLine();
            }
        
            builder.Append($"Gil {TotalGil().ToString("N0")} | {AverageGil().ToString("F2")}").AppendLine();
            builder.Append($"Exp {TotalExp().ToString("N0")} | {AverageExp().ToString("F2")}").AppendLine();
            String msg = builder.ToString();
            Plugin.ChatGui.Print(msg, "") ;
        }
        
        
        public double AverageExp()
        {
            double timespan = totalTimeSpan().TotalMinutes;
            return TotalExp() / timespan;
        }

        public TimeSpan totalTimeSpan()
        {
            TimeSpan ts = TimeSpan.Zero;
            foreach (Pair<DateTime,DateTime?> timespan in Timespans)
            {
                ts += (timespan.second ?? DateTime.Now) - timespan.first;
            }

            return ts;
        }

        private void Save()
        {
            Plugin.DataLoader.saveData(this);
        }
    }
}