using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Create a new HttpClient instance (HttpClient is designed to be long-lived)
        using HttpClient client = new HttpClient();

        // Set up request headers
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

        try
        {
            var repositories = await ProcessRepositoriesAsync(client);

            // Output repository data to terminal
            Console.WriteLine("Repository Data:");
            foreach (var repo in repositories)
            {
                Console.WriteLine($"Name: {repo.Name ?? "N/A"}");
                Console.WriteLine($"Homepage: {(repo.Homepage != null ? repo.Homepage.ToString() : "N/A")}");
                Console.WriteLine($"GitHub: {(repo.GitHubHomeUrl != null ? repo.GitHubHomeUrl.ToString() : "N/A")}");
                Console.WriteLine($"Description: {repo.Description ?? "N/A"}");
                Console.WriteLine($"Watchers: {repo.Watchers:#,0}");
                Console.WriteLine($"{repo.LastPush}");
                Console.WriteLine();
            }

            // Write repository data to Excel file
            string filePath = $"Repositories_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            ExcelHelper.WriteToExcel(repositories, filePath);
            Console.WriteLine($"Repository data written to Excel file: {filePath}");
        }
        catch (HttpRequestException ex)
        {
            // Handle HTTP request errors
            Console.WriteLine($"HTTP request failed: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // Async method to fetch repositories
    static async Task<List<Repository>> ProcessRepositoriesAsync(HttpClient client)
    {
        // Fetch JSON data from GitHub API
        await using Stream stream = await client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");

        // Deserialize JSON data into list of Repository objects
        var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(stream);

        // Return the list of repositories
        return repositories ?? new List<Repository>();
    }
}
