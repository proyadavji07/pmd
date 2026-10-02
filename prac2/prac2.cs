// Step 1: Right click on FILE_NAME project → Add → Class(User.cs)
// Step 2: Write the code in User.cs
public int Id { get; set; }
public string? Name { get; set; }
public string? Address { get; set; }
public int Number { get; set; }

// Step 3: Right click on FILE_NAME→Add→New Folder(Database)
// Step 4: Right click on Database→Add→New Folder(Entity) →Then drag the User.cs file inside the Entity folder
// Step 5: Right click on Database→Add→Class (DatabaseContext.cs)
// Step 6: Install the dependencies: Click on Dependencies → Right click on Packages->Select Manage NuGet Package
//   1. Microsoft.EntityFrameworkCore 
//   2. Microsoft.EntityFrameworkCore.Tool
//   3. Microsoft.EntityFrameworkCore.SqlServer
//   4. Swashbuckle.AspNetCore.Swagger 
// Step 7: Go to view —> Select sql server object explorer
// Step 8: In SQL Server Object Explore —> Right click on database —> Add new database
// Step 10: Right click on table → Add new table (Users)
///   Right click on Users table and view data then fill the data
///   Rename Table Name → Change [dbo].[Table] to [dbo].[UserTable] → Execute
///   Update Database —> click on update → update database
// Step 11: In SQL Right click on created DataBase →go to properties →copy the connecting string
// Step 12: Write the code in DatabaseContext.cs file(paste the connecting string in this file)
using prac2.Database.Entity;
using Microsoft.EntityFrameworkCore;

namespace prac2.Database
{
    public class DatabaseContext:DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=db1;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"
                );
        }
    }
}

// Step 13:Go to tools tab →select NuGet package Manager →Package manager console
add-migration initial
update-database -verbose

// Step 14: Right click on controller→Add Controller →Select API →then select API Controllers with Read/Write actions
using prac2.Database;
using prac2.Database.Entity;
using Microsoft.AspNetCore.Mvc;

namespace prac2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        DatabaseContext db;

        public UsersController()
        {
            db = new DatabaseContext();
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return db.Users.ToList();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        // POST api/<UsersController>
        [HttpPost]
        public ActionResult Post([FromBody] User obj)
        {
            try
            {
                db.Users.Add(obj);
                db.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, obj);
            }
            catch (Exception e)
            {
                // Returning e.Message prevents JSON cyclic reference errors
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

// Then run the code
