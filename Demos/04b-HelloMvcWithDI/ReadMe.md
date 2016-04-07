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
  - Add dependency to project.json: EntityFramework.MicrosoftSqlServer
  - Add references to both the Entities and Patterns projects
  - Add a `ProductDb` class : `DbContext`
    + Add Products property of type `DbSet<Product>`
    + Add a ctor accepting `DbContextOptions` and passing
      then to the base ctor
    + Override `OnModelCreating` to configure the Product table

    ```csharp
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
    
    ```csharp
    public class ProductRepository : IProductRepository
    {
        private readonly ProductsDb _dbContext;

        public ProductRepository(ProductsDb dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await _dbContext.Products
                .OrderBy(e => e.ProductName)
                .ToListAsync();
            return products;
        }

        public async Task<Product> GetProduct(int id)
        {
            var product = await _dbContext.Products
                .SingleOrDefaultAsync(e => e.Id == id);
            return product;
        }
    }
    ```    

5. In main web project, add code to `Startup.ConfigureServices`
   to use Entity Framework with Sql Server.
    - Add dependency to project.json: EntityFramework.MicrosoftSqlServer
    - Add appsettings.json file with connection string
      + Add a new item to the web project, select ASP.NET Configuration File
      + Change the keys to match:
      
    ```json
    {
        "Data": {
            "ProductsDb": {
                "Connection": "Server=(localdb)\\MSSQLLocalDB;Database=ProductsDb;Trusted_Connection=True;"
            }
        }
    }
    ```
      
    - Add a Configuration property of type IConfiguration to Startup
    - Add ctor to Startup with code that set Configuration
      using a new ConfigurationBuilder
      + Call AddJsonFile

    - In ConfigureServices method, call Configuration.GetSection
      to get the connection string.
    
    ```csharp
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }
    ```  

6. In `Startup.ConfigureServices` register Mvc, EF, Sql, DbContext

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

  - Open a command prompt in the main project directory to create the database

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

9. Add a new Class Library project (vNext) to the src folder in the solution.
  - Change frameworks to include just dnx451
  - Add the following dependencies:
    + "Microsoft.AspNet.Mvc":"6.0.0-rc1-final"
    + "xunit": "2.1.0"
    + "xunit.runner.dnx": "2.1.0-rc1-build204"
  - Add references to all of the other projects in the solution
  - Add a test command:
    + "test": "xunit.runner.dnx"

10. Add a FakeProductRepository class which implements IProductRepository
  - Create an in-memory list of products

    ```csharp
    public class FakeProductRepository : IProductRepository
    {
        private List<Product> _products = new List<Product>
        {
            new Product
            {
                Id = 1,
                ProductName = "Espresso",
                Price = 10
            },
            new Product
            {
                Id = 2,
                ProductName = "Capuccino",
                Price = 20
            },
            new Product
            {
                Id = 3,
                ProductName = "Latte",
                Price = 30
            },
        };

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await Task.FromResult(_products);
            return products;
        }

        public async Task<Product> GetProduct(int id)
        {
            return await Task.FromResult(_products[id - 1]);
        }
    }
    ```

11. Add a HomeControllerTests class
  - Add a public `Index_action_should_return_products` method returning `void`
  - Adorn the method with a `[Fact]` attribute
  - Create a new ProductsController, passing a FakeProductRepository
  - Invoke the Index action
  - Assert that the result is a ViewResult

    ```csharp
    public class HomeControllerTests
    {
        [Fact]
        public void Index_action_should_return_products()
        {
            // Arrange
            IProductRepository repo = new FakeProductRepository();
            var controller = new ProductsController(repo);

            // Act
            IActionResult result = controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }
    }
    ```

12. Run the test
  - Open the Test Explorer (Test, Other Windows)
  - Building the solution should reveal the test
  - Run and/or debug the test
  - Open a command prompt and execute the tests there
    + dnx test
