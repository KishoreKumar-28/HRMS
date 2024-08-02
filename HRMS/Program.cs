using HRMS.Data;
using HRMS.Provider;
using HRMS.Provider.Implementation;
using HRMS.Provider.Interface;
using HRMS.Repository;
using HRMS.Repository.Implementation;
using HRMS.Repository.Interface;
//using HRMS.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
//using NLog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAttendanceProviders, AttendanceProvider>();
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
builder.Services.AddScoped<IEmployeeProvider, EmployeeProvider>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentProvider, DepartmentProvider>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewProvider, ReviewProvider>();
builder.Services.AddScoped<ITrainingPro, TrainingPro>();
builder.Services.AddScoped<ITrackingPro, TrackingPro>();
builder.Services.AddScoped<ITrainingRepo, TrainingRepo>();
builder.Services.AddScoped<ITrackingRepo, TrackingRepo>();
builder.Services.AddScoped<ILeaveRepository, LeaveRepository>();
builder.Services.AddScoped<ILeaveProvider, LeaveProvider>();
builder.Services.AddScoped<ISalaryProvider, SalaryProviderClass>();
builder.Services.AddScoped<ISalaryRepo, SalaryRepoClass>();
/*LogManager.LoadConfiguration(System.String.Concat(Directory.GetCurrentDirectory(), "/nlog.config.xml"));
builder.Services.AddSingleton<ILog, Log>();*/
builder.Services.AddDbContext<HrmsdbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else 
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
