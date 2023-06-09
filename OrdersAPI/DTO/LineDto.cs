﻿namespace OrdersAPI.DTO;

/// <summary>
/// DTO связанной сущности.
/// </summary>
public class LineDto
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Количество.
    /// </summary>
    public int Quantity { get; set; }
}