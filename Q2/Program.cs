var builder = WebApplication.CreateBuilder(args);
// Add 
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Edit
// app.MapGet("/", () => "Hello World!");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Order}/{action=Index}/{id?}"
    );

app.Run();