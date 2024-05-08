using MovieTaskPart1;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
ConnectionStrings.AzureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=movietask;AccountKey=E0rbZ/m2jGG8L1i/9wD5vPdvZJgRxQ8ISSbb9qrA/HDjNSV9HHXJQKZ9tm3JqXtT14SvN5+22EG7+AStqL1q7Q==;EndpointSuffix=core.windows.net";

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
