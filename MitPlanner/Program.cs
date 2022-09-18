using MitPlanner.Data;
using MitPlanner.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddAntDesign();

builder.Services.AddSingleton<EncounterRepository>();
builder.Services.AddSingleton<JobRepository>();
builder.Services.AddSingleton<JobActionRepository>();
builder.Services.AddSingleton<ActionTimelineRepository>();

builder.Services.AddSingleton<ActionTimelineService>();
builder.Services.AddSingleton<JobActionService>();

var app = builder.Build();

var _ = app.Services.GetRequiredService<ActionTimelineService>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UsePathBase("/mit");

app.UseStaticFiles();
app.UseStaticFiles("/mit");

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
