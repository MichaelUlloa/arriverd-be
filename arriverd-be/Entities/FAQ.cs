﻿namespace arriverd_be.Entities;

public class FAQ
{
    public int? Id { get; set; }
    public int? ExcursionId { get; set; }
    public string? Question { get; set; }
    public string? Answer { get; set; }
}
