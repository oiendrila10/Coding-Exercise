using Coding_Exercise;

namespace CodingChallenge
{
    public class Program
    {
        private static readonly string logFile = @"C:\Code\Coding Exercise\Exercise\programming-task-example-data.log";

        public static void Main(string[] args)
        {
            if (File.Exists(logFile))
            {
                try
                {
                    string data = File.ReadAllText(logFile).TrimEnd();
                    List<string> lines = data.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList();
                    List<LogEntry> entries = new();
                    foreach (string line in lines)
                    {
                        string[] paths = line.Split(' ');
                        entries.Add(new LogEntry()
                        {
                            IP = paths[0],
                            URL = paths[6],
                            Status = Convert.ToInt32(paths[8]),
                        });
                    }
                    Console.WriteLine($"The number of unique IP addresses : {entries.Select(x => x.IP).Distinct().Count()}");

                    var Top3EntryURL = entries.GroupBy(entry => entry.URL)
                                                     .Select(group => new { URL = group.Key, Count = group.Count() })
                                                     .OrderByDescending(group => group.Count)
                                                     .Take(3);

                    Console.WriteLine("The top 3 most visited URLs");
                    foreach (var entry in Top3EntryURL)
                    {
                        Console.WriteLine($"URL: {entry.URL} Count: {entry.Count}");
                    }

                    var Top3EntryIP = entries.Where(x => x.Status == 200).GroupBy(entry => entry.IP)
                                                     .Select(group => new { IP = group.Key, Count = group.Count() })
                                                     .OrderByDescending(group => group.Count)
                                                     .Take(3);
                    Console.WriteLine("The top 3 most active IP addresses");
                    foreach (var entry in Top3EntryIP)
                    {
                        Console.WriteLine($"IP: {entry.IP} Count: {entry.Count}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("File not Found");
            }
        }
    }
}