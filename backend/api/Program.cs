using backend.api;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var databaseEngine = builder.Configuration.GetValue<DatabaseEngine>("DatabaseEngine");

if (databaseEngine == DatabaseEngine.Sqlite)
{
    builder.Services.AddDbContext<SqliteTodoContext>(options =>
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"));
    });
    builder.Services.AddScoped<BaseTodoContext>(provider => provider.GetRequiredService<SqliteTodoContext>());
}
else if (databaseEngine == DatabaseEngine.Mssql)
{
    builder.Services.AddDbContext<MssqlTodoContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("MssqlConnection"));
    });
    builder.Services.AddScoped<BaseTodoContext>(provider => provider.GetRequiredService<MssqlTodoContext>());
}
else 
{
    throw new InvalidOperationException("Invalid database engine");
}

builder.Services.AddAutoMapper(typeof(TodoProfile));
builder.Services.AddScoped<ITodoService, TodoService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/todos", async (ITodoService todoService) =>
{
    var todos = await todoService.GetTodosAsync();

    return Results.Ok(todos);
})
.WithName("GetTodos")
.Produces<IEnumerable<TodoDto>>(StatusCodes.Status200OK);

var context = (app as IApplicationBuilder)
    .ApplicationServices
    .CreateScope().ServiceProvider
    .GetRequiredService<BaseTodoContext>();

if ((await context.Database.GetPendingMigrationsAsync()).Any())
{
    await context.Database.MigrateAsync();
}

app.Run();
