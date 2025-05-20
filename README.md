# Web-crawler-Csharp

A Windows Forms application written in C# that crawls websites and stores the data in a Microsoft SQL Server database. This tool can extract links, images, and other elements from web pages for data mining purposes.

## Prerequisites

- Microsoft Visual Studio (compatible with the solution format)
- .NET Framework 4.0 (as indicated by the project files)
- Microsoft SQL Server (any version supporting T-SQL)
- SQL Server Management Studio (SSMS)
- HtmlAgilityPack (referenced in the project)

## Setup Instructions

### 1. Clone the Repository

```
git clone https://github.com/killua173/Web-crawler-Csharp.git
cd Web-crawler-Csharp
```

### 2. Database Setup

#### Option 1: Using the SQL Script

1. Open SQL Server Management Studio (SSMS)
2. Connect to your SQL Server instance
3. Open the SQL script file located in the repository at `CrawlerWin/SQLQuery1.sql`
4. Execute the script to create the necessary database and tables

#### Option 2: Restore from Backup (if available)

1. Open SQL Server Management Studio (SSMS)
2. Connect to your SQL Server instance
3. Right-click on "Databases" in the Object Explorer
4. Select "Restore Database..."
5. Choose the appropriate source and restore the database

### 3. Configure the Connection String

1. Open the solution file `CrawlerWin.sln` in Visual Studio
2. In Solution Explorer, locate and open `CrawlerWin/dbMethods.cs`

3. Find the connection string in this file. It will look similar to this:
   ```csharp
   string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=crawler;Integrated Security=True";
   ```

4. Update the connection string with your SQL Server details:
   ```csharp
   string connectionString = "Data Source=YOUR_SERVER_NAME;Initial Catalog=crawler;Integrated Security=True";
   ```

   For SQL Server authentication instead of Windows authentication:
   ```csharp
   string connectionString = "Data Source=YOUR_SERVER_NAME;Initial Catalog=crawler;User ID=YOUR_USERNAME;Password=YOUR_PASSWORD;";
   ```

### 4. Build and Run the Application

1. In Visual Studio, build the solution (Build > Build Solution or press F6)
2. Run the application (Debug > Start Debugging or press F5)

## Using the Web Crawler

1. In the main application window, enter the starting URL to crawl
2. Set the crawl depth limit
3. Choose whether to follow external links
4. Click the "Start" button to begin crawling
5. The application will display progress in the list view
6. The crawled data will be stored in the SQL Server database

## Features

- Crawl websites with specified depth
- Option to follow or ignore external links
- Extract page titles and links
- Track crawl history with timestamps
- Windows Forms UI with progress display
- Data storage in SQL Server database

## Troubleshooting

### Connection String Issues

If you encounter database connection errors:

1. Verify your SQL Server instance name
2. Check if SQL Server is running
3. Ensure the "crawler" database exists
4. Confirm your authentication method and credentials
5. Make sure the SQL Server user has appropriate permissions

### Build Errors

If you encounter build errors:

1. Ensure all NuGet packages are restored (right-click on the solution and select "Restore NuGet Packages")
2. Make sure you have the correct .NET Framework version installed (4.5.2)
3. Check that HtmlAgilityPack is properly referenced
