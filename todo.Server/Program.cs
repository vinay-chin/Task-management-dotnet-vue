using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);
string connectionString = "SERVER=localhost;DATABASE=epam;UID=root;PASSWORD=Pass@123;";
MySqlConnection connection = new MySqlConnection(connectionString);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("https://localhost:5173",
                                "http://www.example.com")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});


var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
// Add services to the container.

// Other service configurations...


// Configure the HTTP request pipeline.
app.UseCors();


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

app.MapGet("/tasks/getall", async () =>
{
    List<TodoApi.Models.TodoTask> tasks = new List<TodoApi.Models.TodoTask>();
    await connection.OpenAsync();

    MySqlCommand command = new MySqlCommand("SELECT * FROM tasks", connection);
    using (var reader = await command.ExecuteReaderAsync())
    {
        while (await reader.ReadAsync())
        {
            var task = new TodoApi.Models.TodoTask
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                Complexity = reader.GetString(3)
            };
            tasks.Add(task);
        }
    }

    await connection.CloseAsync();
    return tasks;
});


app.MapPost("/tasks", async (TodoApi.Models.TodoTask newTask) =>
{
    if (connection.State != System.Data.ConnectionState.Open)
    {
        await connection.OpenAsync();
    }
    Console.WriteLine(newTask.Name);
    MySqlCommand command = new MySqlCommand("INSERT INTO tasks (name, description, complexity) VALUES (@name, @description, @complexity)", connection);
    command.Parameters.AddWithValue("@name", newTask.Name);
    command.Parameters.AddWithValue("@description", newTask.Description);
    command.Parameters.AddWithValue("@complexity", newTask.Complexity);

    await command.ExecuteNonQueryAsync();

    if (connection.State != System.Data.ConnectionState.Closed)
    {
        await connection.CloseAsync();
    }
});

app.MapPost("/login", async (TodoApi.Models.User loginUser) =>
{
    await connection.OpenAsync();

    MySqlCommand command = new MySqlCommand("SELECT * FROM user WHERE username = @username AND password = @password", connection);
    command.Parameters.AddWithValue("@username", loginUser.Username);
    command.Parameters.AddWithValue("@password", loginUser.Password); // Consider using hashed passwords

    TodoApi.Models.User user = null;

    using (var reader = await command.ExecuteReaderAsync())
    {
        while (await reader.ReadAsync())
        {
            user = new TodoApi.Models.User
            {
                Username = reader.GetString(0), // Assuming the username is the first column
                Password = reader.GetString(1), // Assuming the password is the second column
                // Add other fields as necessary
            };
        }
    }

    await connection.CloseAsync();

    if (user == null)
    {
        return null;
    }

    return user;
});

app.MapPost("/Signup", async (TodoApi.Models.User user) =>
{
    if (connection.State != System.Data.ConnectionState.Open)
    {
        await connection.OpenAsync();
    }

    Console.WriteLine(user.Username);
    MySqlCommand command = new MySqlCommand("INSERT INTO User (Username, Password) VALUES (@username, @password)", connection);
    command.Parameters.AddWithValue("@username", user.Username);
    command.Parameters.AddWithValue("@password", user.Password);

    await command.ExecuteNonQueryAsync();

    if (connection.State != System.Data.ConnectionState.Closed)
    {
        await connection.CloseAsync();
    }
});
app.MapFallbackToFile("/index.html");

app.Run();


internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

