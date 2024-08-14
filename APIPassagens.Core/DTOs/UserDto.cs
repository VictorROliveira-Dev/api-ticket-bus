using System.ComponentModel.DataAnnotations;

namespace APIBusService.Core.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}
