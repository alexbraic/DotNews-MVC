## Installations

What software is required to run the projects?

We have designed our project to run on **Visual Studio 2022** on the **ASP.NET Core 6.0** framework, and it will require that to function as intended.

### Setup & Configuration

What needs to be added for running the project?

NuGet Packages to be installed:

	- microsoft.aspnetcore.diagnostics.entityframeworkcore\6.0.4\
	- microsoft.aspnetcore.identity.entityframeworkcore\6.0.4\
	- microsoft.aspnetcore.identity.ui\6.0.4\
	- microsoft.entityframeworkcore.sqlserver\6.0.4\
	- microsoft.entityframeworkcore.tools\6.0.4\
	- microsoft.visualstudio.web.codegeneration.design\6.0.3\
	- swashbuckle.aspnetcore\6.3.0\

## Running Locally

How can the project be run locally?

The application and the service APIs are all running locally on 3 different ports:

The DotNews MVC is on - *https://localhost:7040*

The ReportsService is on - *https://localhost:7290*

The CommentsService is on  - *https://localhost:7209*

**CRUD** operations for the APIs:

ReportsService - *https://localhost:7290/swagger/index.html*

CommenttsService - *https://localhost:7209/swagger/index.html*


**To have a fully functional project**:

First start the DotNews-MVC application.

Then start the Reports Service and Comments Service.

Once all three applications are started you can see the Reports Service API data in the 'API News Feed' tab from the application, and the comments data from the 'API Comments' tab.