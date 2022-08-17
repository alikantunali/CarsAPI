# CarsAPI Solution 
<details>
  <summary><strong>Table of Contents</strong> (click to expand)</summary>

<!-- toc -->

- [Overview](#overview)
  - [Features](#features)
  - [Dependencies](#dependencies)
- [Project Structure](#project-structure)
  - [ARM Templates](#arm-templates)
  - [Deploy](#deploy)
  - [Sql](#sql)
  - [Src](#src)
  - [Test](#test)
  - [Azure Pipelines](#azure-pipelines)
- [Prerequisites](#prerequisites)
  - [SQL Server](#sql-server)
- [Installation](#installation)
  - [With Docker Desktop](#with-docker-desktop)
- [Credits](#credits)

<!-- tocstop -->

</details>

## Overview

A RESTful API that is created with MVC design pattern.

CarsAPI project runs create, read, update, delete operations on SQL Database with C#. In addition, it contains a static json list for testing purposes. 
The api has two diffrent routes **CarsAPI** & **StaticCarsAPI**. Whilst CarsAPI injects repository using entity framework data context and its db set, StaticCarsAPI uses json list initialized in the start up. 
In development environment Swagger definition can be found at:
https://server_url:port(optional)/***swagger/v1/swagger.json***
- CarsAPI
- StaticCarsAPI

##### CarsAPI Service
CRUD operations on database table.
| Method | Path         | Body            |
| ------ | ------------ | --------------- |
| GET   | /CarsAPI/cars | no input |
| GET   | /CarsAPI/carId/{carId}| int carId |
| POST   | /CarsAPI/addCar| "brandName":"string", "manufactureYear":"string", "model":"string" |
| PUT   | /CarsAPI/updateCar| "id":"int", "brandName":"string", "manufactureYear":"string", "model":"string" |
| DELETE   | /CarsAPI/deleteCar/{carId}| int carId |
##### StaticCarsAPI Service
CRUD operations on static list.
| Method | Path         | Body            |
| ------ | ------------ | --------------- |
| GET   | /StaticCarsAPI/cars | no input |
| GET   | /StaticCarsAPI/carId/{carId}| int carId |
| POST   | /StaticCarsAPI/addCar|"id":"int", brandName":"string", "manufactureYear":"string", "model":"string" |
| PUT   | /StaticCarsAPI/updateCar| "id":"int", "brandName":"string", "manufactureYear":"string", "model":"string" |
| DELETE   | /StaticCarsAPI/deleteCar/{carId}| int carId |

### Features
- create a new car data in the specified database
- update an existing car data in the database
- retrieve all available data in the database
- delete a car from the database with given id.
### Dependencies
CarsAPI is a project written in C# language. It also uses Common Library project as a reference for SQL operations. CarsAPI solution and Tests have a number of external NuGet Packages to work properly: 

***CarsAPI***
- [Swashbuckle] - a Swagger object model and middleware to expose SwaggerDocument objects as JSON endpoints.
- [Microsoft.EntityFrameworkCore.Design] - Shared design-time components for Entity Framework Core tools.
- [AzureIdentity] - For Azure deployment.

***Common***
- [Microsoft.EntityFrameworkCore.SqlServer] - Microsoft SQL Server database provider for Entity Framework Core.
- [Microsoft.EntityFrameworkCore.Tools] - Entity Framework Core Tools for the NuGet Package Manager Console in Visual Studio.

***Unit Tests***
- [Xunit] - A developer testing framework, built to support Test Driven Development, with a design goal of extreme simplicity and alignment with framework features.
- [EntityFrameworkMock.Moq] - Easy Mock wrapper for mocking EF6 DbContext and DbSet using Moq. 
- [Moq] - Mocking framework for .NET

## Project Structure
CarsAPI project is split into these 5 main folders:
- **armtemplates :** In this folder you can find Azure Resource Manager Templates used to deploy resources on azure portal.
Sample folder contains resource manager template examples. 
- **deploy :** within this folder, you can find a set of yaml that are used during the build/release tasks when deploying from devops.
-Variables used in yaml files are located in subfolder **variables**. 
- **src :** CarsAPI solution resides within this 
**1-**  CarsAPI project - Net 6.0
**2-**  Common project - Net 6.0
- **test :** This folder contains unit test projects for both CarsAPI and Common projects. 


### ARM Templates
Azure resource templates are located in **armtemplates** folder in the root folder. 
- 1KeyVault.json & 1KeyVault.parameters.json files are used to create a key vault on azure to store SQL credentials.
- 2WebApp-SQLDatabase-AppPlan-AI.json & 2WebApp-SQLDatabase-AppPlan-AI.json files are used to create: 
1- Azure App Service Plan
2- Azure App Service
3- SQL Server
4- SQL Database
5- Application Insights

Variables for templates are tokenized and located in ***root/deploy/variables*** folder.
tokenized varialbes are replaced during deployment stage. This method also helps to keep versioning.

### Deploy 
Deploy folder contains files for Azure Deployment templates and its variables under the following folders:
#### Templates:
- ***deploy-arm-template.yaml*** file is the deployment template file which used within azure devops deployment pipeline. 

#### Variables:
Variables are segregated for each environment.Variable folder contains **release-information.yaml** file and 2 sub folders as ***apps & environments***. 
- **apps**
  - application environment variables and common app variables stored in **app-dev.yaml** and **app-shared.yaml**. 
- **environments**
  - azure environment variables and common azure portal variables stored in **environment-dev-yaml** and **environment-shared.yaml**.

### Sql 
SQL folder contains initial migrations script (initial.sql). In case of a migration automation, this script file can be used in deployment pipeline.

### Src
Root Source folder has two sub folders named **CarsAPI** & **Common**.
CarsAPI project uses Common project as reference in order to complete database operations. 

##### CarsAPI:
Content of CarsAPI folder consists of: 
**appsettings.json** file which determines the database info for the application.
**Dockerfile** for future containerized application deployment.
**Program** class file which sets up the startup of the app.
**Controllers** folder containing mvc app controller classes. These classes injects Interfaces belong to repository classes in Common project.

##### Common
Content of Common folder consists of: 
**DbDataContext**  folder contains datacontext class file which is used for Entitiy Framework operations.
**Entities** folder hosts the generic car class used in the project.
**Migrations** folder keeps entity framework migration class which helps to keep the database schema in sync with the data model.
**Models** folder includes initial data model class belongs to Entity Framework.
**Repositories** folder comprises two sub folder named as **CarDbListService** & **CarListService**. These folders contain repository class libraries. Repository classes and Interfaces are designed for resusability with only few configuration settings. (Dependency injection purpose)

### Test
Unit testing implemented for each project in this solution. (CarsAPI & Common).
**CarsAPI.Test** folder contains api controller class tests. 
**Common.Test** folder contains common repository tests.
Unit tests for the api which connects to database are done by using mocking framework. Repository classes are mocked to test controller classes.
Also data context (db context) logger used in repository classes and db set are mocked with Moq framework.

### ***Azure Pipelines***
azure build & deploy pipeline files are located in root folder. Deploy pipeline consumes artifacts created during build phase. It also uses variable files located in deploy folder. Build pipeline generates the artifacts below: 
- initial sql script
- arm templates
- application zip folders 

## Prerequisites
### SQL Server
SQL Server must be installed to run the project locally. Using SQL Server Docker image is recommended for an easier installation.
## Installation
### With Docker Desktop & SQL Server Image
You can download Docker Engine from ***[here]***

1- Installing SQL Server Image
```
# Pull SQL Server Image
docker pull mcr.microsoft.com/mssql/server:2022-latest

# Start a mssql-server instance for SQL Server 2022
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest

# Connect to Microsoft SQL Server to check server health.
docker exec -it <container_id|container_name> /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P <your_password>
```
2- Download the solution from GitHub
```
# Open GitBash in Visual Studio Code
# Change the current working directory to the location where you want the cloned directory. 
cd <desired_directory>

git clone https://github.com/alikantunali/CarsAPI.git
```
3- Setting connection string in appsettings.json 

***Go to CarsAPI project source directory and add SQL Server connection string to  appsettings.json file***

`
{ "ConnectionStrings": {    
    "CarsDbSQLConnection": "Data Source=localhost;Initial Catalog=<Your Database Name>;User ID=sa;Password=<your_password>;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"    
  }}`

4- Seeding Database
***via Git Bash***
```
# go to main project folder
cd <desired_directory>/src/CarsAPI/

#build project
dotnet build

# Update database - Initial Migration will be used by the below command.
dotnet ef database update 
```

5- Run Project
***via Git Bash***
```
# In CarsAPI main project folder
dotnet run
```
6- Browse Web API
You can edit your url in launchSettings.json file. Otherwise default url is: 
**https://localhost:7075/index.html**
## Credits
- https://hub.docker.com/_/microsoft-mssql-server
- https://docs.microsoft.com/en-us/ef/core/
- https://dotnet.microsoft.com/en-us/download/dotnet/6.0
- https://xunit.net/
- https://www.nuget.org/

Alikan TUNALI, 22.08.2022

[Swashbuckle]: <https://www.nuget.org/packages/Swashbuckle.AspNetCore.Swagger/>
[Microsoft.EntityFrameworkCore.Design]: <https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design/>
[AzureIdentity]: <https://www.nuget.org/packages/Azure.Identity/1.7.0-beta.1> 
[Microsoft.EntityFrameworkCore.SqlServer]: <https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/7.0.0-preview.7.22376.2>
[Microsoft.EntityFrameworkCore.Tools]: <https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools/7.0.0-preview.7.22376.2>
[Xunit]: <https://www.nuget.org/packages/xunit>
[EntityFrameworkMock.Moq]: <https://www.nuget.org/packages/EntityFrameworkMock.Moq>
[Moq]: <https://www.nuget.org/packages/Moq>
[here]: <https://www.docker.com/products/docker-desktop/>
