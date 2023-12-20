namespace backend.Models;

public class User
{
    public string? Id { get;  set; }
    public string? Username { get;  set; }
    public string? Password { get;  set; }

    public static readonly List<User> TestUsers =
    [
        new User
        {
            Id = "1",
            Username = "admin",
            Password = "admin"
        },

        new User
        {
            Id = "2",
            Username = "user",
            Password = "user"
        }
    ];
}