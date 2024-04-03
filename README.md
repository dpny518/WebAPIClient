# WebAPIClient

## Overview
The **WebAPIClient** application is a .NET Core console application designed to fetch repository data from the GitHub API for a specified organization and store it in both the terminal output and an Excel file. It demonstrates how to use HttpClient to make HTTP requests to a REST API, handle JSON responses, and interact with Excel files using the EPPlus library.

## Requirements
To run this application, ensure that you have the following installed:
- .NET Core SDK (version 3.1 or higher)

## Packages
The application uses the following NuGet packages:
- `System.Net.Http`: Provides HttpClient for sending HTTP requests and receiving HTTP responses from a resource identified by a URI.
- `EPPlus`: A .NET library for creating and reading Excel files.

## Installation
No explicit installation is required for the application itself. However, you need to have the .NET Core SDK installed on your system.

To install the required packages, you can use the following commands:
```
dotnet add package System.Net.Http
dotnet add package EPPlus
```

## Usage
To run the application, follow these steps:
Clone this repository to your local machine.
Navigate to the root directory of the WebAPIClient project.
Open a terminal or command prompt.
Run the following command to build and run the application:
```
dotnet run
```

The application will fetch repository data from the GitHub API for the specified organization (in this case, dotnet) and display it in the terminal. It will also create an Excel file named Repositories_<DateTime>.xlsx in the same directory, containing the repository data.

#### Configuration
You can modify the organization whose repositories are fetched by changing the value of the `orgName` variable in the `ProcessRepositoriesAsync` method in the Program.cs file.

