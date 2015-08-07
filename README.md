MvcStarterKit
=============

Basic castle dependency injection installed mvcstarterkit sample.

#Basic Configuration Steps
After installing from NuGet package you need to do the followings

##Setting up global asax file
In global.asax.cs file or in the application start;

```csharp
   Bootstrapper
                .StartApplication(new IWindsorInstaller[] {new ApplicationControllersInstaller()})
                .InitializeDependencies(InstallDependency);
                .InstallDatabase(dbCreationAction: InstallDb);
```
###Controller Installer
ApplicationComponentsInstaller class is a basic installer class for Castle.Windsor. Following example is for registering every class that is inherited from ``` IController```.
```csharp
 public class ApplicationControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssembly(GetType().Assembly).BasedOn<IController>().LifestyleTransient());
        }
    }
```

###Dependency Initializer
InstallDependency method is a customization window where you can register your own dependencies to the Application Dependency Injection Container.

```csharp
        private static void InstallDependency(IWindsorContainer container)
        {
            container.Register(Component.For<IConfigurationService>().ImplementedBy<AppConfigurationService>());
            container.Register(Component.For<IUserInformationRepository>().ImplementedBy<UserInformationRepository>());
        }
```

After binding interfaces with classes then you'll be able to upload call them using the "Constructor Injection".

```csharp
public class HomeController : Controller
{
    public HomeController(IConfigurationService configurationService, IUserInformationRepository userInformationRepository)
    {
        _configurationService = configurationService;
        _userInformationRepository = userInformationRepository;
    }
}
```

###Install Database Components
MvcStartedKit's database layer is based on [ServiceStack.OrmLite](https://github.com/ServiceStack/ServiceStack.OrmLite). Therefore you can use the whole capabilities of the project simply by adding a delegate method to the application start event.

```csharp
        private static void InstallDatabase(IDbConnection connection)
        {
            connection.CreateTableIfNotExists<Category>();
            connection.CreateTableIfNotExists<CategoryItem>();
            connection.CreateTableIfNotExists<UserInformation>();
        }
```

##Setting up the database 
For setting up a default database connection you simply should put the following configuration item to the web.config file.
```xml
  <connectionStrings>
    <add name="DefaultConnection" 
          providerName="System.Data.SqlClient" 
          connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=HealthStore;" />
  </connectionStrings>
```

##Using the datalayer connection
When all the configuration steps are done you need to use the [DbHelper](https://github.com/msusur/MvcStarterKit/blob/master/Services/DbHelper.cs) class for initiating a new database connection and executing sql commands.

```csharp
            using (var c = DbHelper.CreateConnection())
            {
                return c.Select<Category>(x => x.IsActive && x.UserId == userId);
            }
```
