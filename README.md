
This Project uses a template .NUnit Project with Target .Net Framework 7.0. Project Automates Sample BNZ Demo application in BDD Format ( C# , Specflow and Selenium)

Nuget Packets Installed

1.Selenium WebDriver 2.Selenium WebDriver ChromeDriver 3.Selenium Support 4. Specflow 5. Specflow Nunit 6. Specflow MSBuild 7. Specflow.Tools.MsBuild.Generation 8. NUnit 9. Nunit3TestAdapter

Notes

Base Classes have been written for Selenium Methods. Step Defenitions for the Whole Project have been kept Generic whereas Pages have been separated. Drivers are Instantiated using Before Scenario Hooks and Diposed using [After Scenario] blocks. 

All scenarios to automate are kept as 4 Different Features
1.EndToEnd
2.Login
3.Registration
4.Voting
