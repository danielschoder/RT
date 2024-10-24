﻿namespace Catalog.API.DTOs;

public class PaginatedResult<T>
{
    public IEnumerable<T> Data { get; set; } = new List<T>();

    public int CurrentPage { get; set; }

    public int PageSize { get; set; }

    public int TotalRecords { get; set; }
}
