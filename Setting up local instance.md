Setting up your local and running it locally
========

1. Download and install MySql community
	- https://dev.mysql.com/downloads/installer/
2. Setup you MySql Server
3. Download and install .Net Sutdio 2019 Community
	- https://visualstudio.microsoft.com/downloads/
4. Go to GitHub Repo
	- https://github.com/rafakulit-git/D3hiring
5. Click 'Clone or Download'
6. Click 'Download ZIP'
7. Extract the ZIP file
8. Open your .Net Studio 2019 as 'Administrator'
	- Right click on the Visual Studio 2019 icon
	- Locate Visual Studio 2019 in the list of items and right click on it
	- Find 'Run as Administrator' and click on it
9. Open the solution in your extracted folder 'D3hiring\src\d3hiring.sln'
10. Open your MySql Workbench and connect to your DB
	- Restore DB using the files inside the extracted folder 'D3hiring\database\dumps\Dump20200301'
11. Inside VS 2019, you can see the 'Solution Explorer' on the right side panel.
	- Look for appsettings.json and open the file
	- Check and confirm the connection string.
12. Once all set, we can try running the application