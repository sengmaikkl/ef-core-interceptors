// See https://aka.ms/new-console-template for more information
using EF_Core_Interceptors.DataAccess;
using EF_Core_Interceptors.Models;
using Microsoft.EntityFrameworkCore;


Console.WriteLine("Hello, World!");
//static async Task Main(string[] args)
//{
    AddStudents();

    Console.WriteLine("---------------------------------------------");
    Console.WriteLine("With Interceptor....");
    await GetAllStudents(applyInterceptor: true);
    Console.WriteLine("---------------------------------------------");

    Console.WriteLine("Without Interceptor....");
    await GetAllStudents(applyInterceptor: false);
    Console.WriteLine("---------------------------------------------");
//}
static void AddStudents()
{
    using var context = BuildUniversityContext();
    context.Add(
        new Student
        {
            FirstName = "John",
            LastName = "Doe",
            Address = "4 Privet Drive",
        });

    context.Add(
        new Student
        {
            FirstName = "Jane",
            LastName = "Doe",
            Address = "4 Privet Drive",
        });
    context.SaveChanges();
}
static async Task GetAllStudents(bool applyInterceptor)
{
    using var context = BuildUniversityContext();

    IList<Student> studentCollection = null;
    if (applyInterceptor)
    {
        studentCollection = await context.Students.TagWith("Apply OrderBy DESC").ToListAsync();
    }
    else
    {
        studentCollection = await context.Students.ToListAsync();
    }

    Console.WriteLine("Printing List of Students");
    foreach (var student in studentCollection)
    {
        Console.WriteLine(student);
    }
}
static Employee_DBContext BuildUniversityContext()
{
    var dbContextBuilder = new DbContextOptionsBuilder<Employee_DBContext>();

    // HARDCODED - For this demo.  
    //var connectionString = "Server=SENGMAIKOKOLAY;Database=Employee_DB;Trusted_Connection=False;User ID=sa;Password=123456";
    var connectionString = "Data Source=SENGMAIKOKOLAY;Initial Catalog=Employee_DB;Integrated Security=True;TrustServerCertificate=True;User ID=sa;Password=123456;";

    dbContextBuilder.UseSqlServer(connectionString);
    return new Employee_DBContext(dbContextBuilder.Options);
}