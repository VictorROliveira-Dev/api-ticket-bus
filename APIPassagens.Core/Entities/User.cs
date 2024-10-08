﻿using System.ComponentModel.DataAnnotations;

namespace APIBusService.Core.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public ICollection<Ticket> Tickets { get; set; }
}
