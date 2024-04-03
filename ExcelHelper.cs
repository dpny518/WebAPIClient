using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;

public class ExcelHelper
{
    // Method to write repository data to Excel
    public static void WriteToExcel(List<Repository> repositories, string filePath)
    {
        using var package = new ExcelPackage();

        // Add a new worksheet
        var worksheet = package.Workbook.Worksheets.Add("Repositories");

        // Add headers
        worksheet.Cells[1, 1].Value = "Name";
        worksheet.Cells[1, 2].Value = "Homepage";
        worksheet.Cells[1, 3].Value = "GitHub";
        worksheet.Cells[1, 4].Value = "Description";
        worksheet.Cells[1, 5].Value = "Watchers";
        worksheet.Cells[1, 6].Value = "Last Push";

        // Add data
        for (int i = 0; i < repositories.Count; i++)
        {
            var repo = repositories[i];
            worksheet.Cells[i + 2, 1].Value = repo.Name;
            worksheet.Cells[i + 2, 2].Value = repo.Homepage?.ToString();
            worksheet.Cells[i + 2, 3].Value = repo.GitHubHomeUrl?.ToString();
            worksheet.Cells[i + 2, 4].Value = repo.Description;
            worksheet.Cells[i + 2, 5].Value = repo.Watchers;
            worksheet.Cells[i + 2, 6].Value = repo.LastPush.ToString();
        }

        // Save the Excel file
        var fileInfo = new FileInfo(filePath);
        package.SaveAs(fileInfo);
    }

    // Method to read repository data from Excel
    public static List<Repository> ReadFromExcel(string filePath)
    {
        var repositories = new List<Repository>();

        try
        {
            using var package = new ExcelPackage(new FileInfo(filePath));
            var worksheet = package.Workbook.Worksheets["Repositories"];

            int rowCount = worksheet.Dimension.Rows;
            for (int row = 2; row <= rowCount; row++)
            {
                var name = worksheet.Cells[row, 1].Value.ToString();
                var homepage = worksheet.Cells[row, 2].Value?.ToString();
                var gitHub = worksheet.Cells[row, 3].Value?.ToString();
                var description = worksheet.Cells[row, 4].Value.ToString();
                var watchers = int.Parse(worksheet.Cells[row, 5].Value.ToString());
                var lastPush = DateTime.Parse(worksheet.Cells[row, 6].Value.ToString());

                repositories.Add(new Repository(name, description, gitHub != null ? new Uri(gitHub) : null, homepage != null ? new Uri(homepage) : null, watchers, lastPush));
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Error: The specified Excel file does not exist.");
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Error parsing data from Excel file: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error reading Excel file: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }

        return repositories;
    }
}
