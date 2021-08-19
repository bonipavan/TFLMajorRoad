# ProjectInfo
Able to fetch and display Major Road Status from TFL by using this application. Its built using .net core 3.1 and used NUnit for unit test cases

# Approach
Followed TDD and AAA for tests

builder pattern to call Api using httpclient

Dependency injection pattern for loosely coupled architecure.

Solid principles

TFLMajorRoadStatus - Console application to receive the input

TFLMajorRoadStatusBLL - Main system for retrieving the status of major roads

TFLMajorRoadStatusTest- Unit test solution

# Prerequisite
Install VS2019 Professional or community edition.
Install Powershell.

# AppId and AppKey
There are two json configs used in the solution.One in the main project and another in test project.
TFLMajorRoadStatus - AppSettings.json
TFLMajorRoadStatusTest - AppSettingstest.json

Please update the values in solution and published folder.

# Assumptions
Deployment mode as Frameworkdependent

git available

# How to build the code
1. Clone the repository using GitBash/Powershell from Github
2. https://github.com/bonipavan/TFLMajorRoadStatus.git
3. Open the .sln file using Visual Studio 2019
4. Press on Ctrl+Shift+B to build the code.

o	How to run the output
1. Before running the application from visual stidio make sure you update commandLineArgs value in launchSettings.json file
2. Run TFLMajorRoadStatus application in visual studio
3. This will open console window taking A2 as initial input
   
   To run application - ./TFLMajorRoadStatus A2
   In powershell, 
   ps C:\Users> cd C:\TFLMajorRoads\TFLMajorRoadStatus\bin\Release\netcoreapp3.1\publish\
   ps C:\TFLMajorRoads\TFLMajorRoadStatus\bin\Release\netcoreapp3.1\publish> ./TFLMajorRoadStatus.exe A2  
    
    The Status of the A2 is as follows
            Road Status is Good
            Road Status Description is No Exceptional Delays 
   
   To Run tests:
   ps C:\TFLMajorRoads\TFLMajorRoadStatus\bin\Release\netcoreapp3.1\publish> cd C:\TFLMajorRoads\TFLMajorRoadStatusTest\
   ps C:\TFLMajorRoads\TFLMajorRoadStatusTestpublish> dotnet test
   To run tests - dotnet vstest TFLMajorRoadStatusTest.dll

   Test Run Successful.
   Total tests: 9
	 Passed: 9   
 
4. Run
    
    cd C:\TFLMajorRoads\TFLMajorRoadStatus\bin\Release\netcoreapp3.1\publish\
    TFLMajorRoadStatus.exe A2
    
    Expected result
    
    The Status of the A2 is as follows
            Road Status is Good
            Road Status Description is No Exceptional Delays
     
4. UnitTest
     
     cd C:\TFLMajorRoads\TFLMajorRoadStatusTest\
     
     dotnet test
     
     Expected Result:
	Test Run Successful.
	Total tests: 9
	     Passed: 9      
