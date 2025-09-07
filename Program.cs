using System.ComponentModel.Design.Serialization;
using System.Text.Json;

namespace GithubActivityTracker
{
    class Program
    {
        private static readonly string BaseUrl = "https://api.github.com";
        private static readonly HttpClient client = new HttpClient();
         
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Usage: dotnet run <github-username>");
                Console.ResetColor();
                return;
            }
            
            string username = args[0];
            try
            {
                await GetUserActivity(username);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error {e.Message}");
                Console.ResetColor();
            }
        }

        static async Task GetUserActivity(string username)
        {
            string url = $"{BaseUrl}/users/{username}/events/public";
            if (!client.DefaultRequestHeaders.Contains("User-Agent"))
            {
                client.DefaultRequestHeaders.Add("User-Agent", "GithubActivityTracker");
            }
            
            HttpResponseMessage response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"User not found or API error: {response.StatusCode}");
                Console.ResetColor();
                return;
            }
            
            string json = await response.Content.ReadAsStringAsync();
            JsonDocument doc = JsonDocument.Parse(json);
            JsonElement root = doc.RootElement;

            if (root.ValueKind != JsonValueKind.Array)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Unexpected response from API:");
                Console.WriteLine($"Response: {json}");
                Console.ResetColor();
                return;
            }
            
            Console.WriteLine($"\nLast Activities of {username}:\n");

            int count = 0;

            foreach (JsonElement activity in root.EnumerateArray())
            {
                if (count >= 10) break;
                
                try
                {
                    if (!activity.TryGetProperty("type", out JsonElement typeElement) ||
                        !activity.TryGetProperty("created_at", out JsonElement dateElement))
                    {
                        continue;
                    }

                    string eventType = typeElement.GetString() ?? "unknown";
                    string createdAt = dateElement.GetString() ?? "unknown";
                    
                    string repoName = "Unknown Repository";
                    if (activity.TryGetProperty("repo", out JsonElement repoElement))
                    {
                        if (repoElement.TryGetProperty("name", out JsonElement repoNameElement))
                        {
                            repoName = repoNameElement.GetString() ?? "Unknown Repository";
                        }
                    }
                    
                    DateTime date = DateTime.Parse(createdAt);
                    string formattedDate = date.ToString("dd-MM-yyyy HH:mm");

                    string eventMessage = eventType switch
                    {
                        "PushEvent" => $"Pushed to {repoName}",
                        "CreateEvent" => $"Created {repoName}",
                        "WatchEvent" => $"Starred {repoName}",
                        "ForkEvent" => $"Forked {repoName}",
                        "IssuesEvent" => $"Opened/closed issue in {repoName}",
                        "PullRequestEvent" => $"Created pull request in {repoName}",
                        "FollowEvent" => $"Started following someone",
                        "DeleteEvent" => $"Deleted something from {repoName}",
                        _ => $"{eventType} in {repoName}"
                    };
                    
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{eventMessage}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($"{formattedDate}\n");
                    Console.ResetColor();
                    
                    count++; 
                }
                catch (Exception ex)
                {
                    continue;
                }
            }

            if (count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No recent public activities found.");
                Console.ResetColor();
            }
        }
    }
}