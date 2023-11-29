using System.ComponentModel.DataAnnotations;

namespace Dotnet;

public class Test
{
    [Key]
    public Guid Id { get; set; }
    public string Role { get; set; } = default!;
    public string Message { get; set; } = default!;
}