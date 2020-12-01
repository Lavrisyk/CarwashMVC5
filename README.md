# CarwashMVC5
Carwash web project using Asp.net mvc5 allows you to view a list of customers and 
their orders with a list of services provided.
  A completed order can be saved to a PDF file, 
  which is generated based on the InDesign template.
Loading a template and creating a PDF document is 
performed in a method of the static class WebCarWash.Models.OrderConvert.ToPdfFile
  
# Frameworks - Libraries  
 ``
-ASP.NET MVC 5
-Entity Framework
-Ninject
-MOQ
-InDesignServer
 ``
# Launch of the project
-Download Adobe IndesignServer(V2020) and install. Add reference in the project WebCarWash.

- Open the project in Visual Studio.
- in the `web.config` file, change the connection string according to your system.
    ``   <connectionString> <add name = "ServicesContext" providerName = "System.Data.SqlClient" connectionString = "Data Source = (LocalDb) \ MSSQLLocalDB; Home Directory = WebCarWashTest; Integrated Security = SSPI; AttachDBFilename = | DataDirectory | \ WebCarWashTest.mdf "/>
</connectionString>
    ``
-In file Global.asax remove comment on record 
 ``Database.SetInitializer(new ServiceDbInitializer()); ``
