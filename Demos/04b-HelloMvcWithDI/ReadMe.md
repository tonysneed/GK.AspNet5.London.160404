# MVC with Dependency Injection and Entity Framework

1. Add an Entities class library Project (vNext package)
  - Add a Product class

2. Add a Patterns class library project (vNext package)
  - Add an IProductRepository interface
  - Add methods: GetProducts, GetProduct(id)

    ```csharp
    public interface IProductRepository
    {
    Task<IEnumerable<Product>> GetProducts();

    Task<Product> GetProduct(int id);
    }
    ```

3. Add a Patterns.EF class libary project (vNext package)
  - Add dependency to project.json: EntityFramework.SqlServer
  - Add a `ProductDb` class : `DbContext`
    + Add Products property of type `DbSet<Product>`
    + Add a ctor accepting `DbContextOptions` and passing
      then to the base ctor
    + Override `OnModelCreating` to configure the Product table

    ```charp
    public class ProductsDb : DbContext
    {
        public ProductsDb(DbContextOptions options) :
            base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.ProductName)
                .IsRequired();
        }
    }
    ```

4. Add ProductRepository class that implements IProductRepository
    + Add ctor accepting `ProductsDb`
    + Use EF to query the database
    + Use async / await

5. In main web project, add code to `Startup.ConfigureServices`
   to use Entity Framework with Sql Server.
    - Add dependency to project.json: EntityFramework.SqlServer
    - Add appsettings.json file with connection string
    - Add IConfiguration property to Startup
    - Add ctor to Startup with code that set Configuration
    using a new ConfigurationBuilder
    + Call AddJsonFile
    - In ConfigureServices method, call Configuration.GetSection
    to get the connection string.

6. In `Startup.ConfigureServices` register EF, Sql, DbContext

    ```csharp
    var connection = Configuration.GetSection("Data:ProductsDb:Connection").Value;
    services.AddEntityFramework()
        .AddSqlServer()
        .AddDbContext<ProductsDb>(options =>
            options.UseSqlServer(connection));
    ```

7. Then register the repository interfaces and implementation

    ```csharp
    services.AddScoped<IProductRepository, ProductRepository>();
    ```

8. Use EF migrations to create the database
  - Add a dependency for EntityFramework.Commands
  - Add an ef command to project.json

    ```json
    "commands": {
      "web": "Microsoft.AspNet.Server.Kestrel",
      "ef": "EntityFramework.Commands"
    },
    ```

  - Open a command prompt to create the database

    ```shell
    dnx ef database update -c ProductsDb
    ```

  - Add the first migration

    ```shell
    dnx ef migrations add first_migration -c ProductsDb -p HelloMvcWithDI.Patterns.EF
    ```

  - Update the database to the first migration

    ```shell
    dnx ef database update first_migration -c ProductsDb
    ```

  - Open the database using SQL Mant Studio
    + Connect using (localdb)\MSSQLLocalDB
    + Poppulate the database

9. Testing
