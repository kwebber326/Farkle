using Farkle.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farkle.File_IO
{
    public static class StatsIO
    {
        private const string PLAYER_STATS_FOLDER = "PlayerStats";
        public static List<PlayerStats> LoadPlayerStats()
        {
            try
            {
                if (CreateDirectoryIfNotPresent(PLAYER_STATS_FOLDER, out string path))
                {
                    using (FileStream fs = File.OpenRead(path))
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        string data = reader.ReadToEnd();
                        List<PlayerStats> playerStats = JsonConvert.DeserializeObject<List<PlayerStats>>(data);
                        return playerStats ?? new List<PlayerStats>();
                    }
                }
                else
                {
                    throw new Exception($"Could not create directory {path}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message} {ex.ToString()}");
                return new List<PlayerStats>();
            }
        }

        public static bool SavePlayerStats(List<PlayerStats> stats)
        {
            try
            {
                if (CreateDirectoryIfNotPresent(PLAYER_STATS_FOLDER, out string path))
                {
                    var currentStats = LoadPlayerStats();
                    foreach (var stat in currentStats)
                    {
                        if (!stats.Any(s => s.PlayerName == stat.PlayerName))
                        {
                            stats.Add(stat);
                        }
                    }
                    File.WriteAllText(path, string.Empty);
                    using (FileStream fs = File.OpenWrite(path))
                    using (StreamWriter writer = new StreamWriter(fs))
                    { 
                        string json = JsonConvert.SerializeObject(stats);
                        writer.Write(json);
                        return true;
                    }
                }
                else
                {
                    throw new Exception($"Could not create directory {path}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message} {ex.ToString()}");
                return false;
            }
        }

        private static bool CreateDirectoryIfNotPresent(string directory, out string path)
        {
            path = Environment.CurrentDirectory + @"/" + directory;
            string fileName = path + "/" + PLAYER_STATS_FOLDER + ".txt";
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                   
                    if (!File.Exists(fileName))
                    {
                        File.Create(fileName);
                    }
                }
                path = fileName;

                return true;
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"{ex.Message} {ex.ToString()}");
                path = null;
                return false;
            }
        }
    }
}
