using PracticeWebAPIDemo.Infrastructure.Extensions;
using PracticeWebAPIDemo;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build<Startup>();
app.Run();