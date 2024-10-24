using Microsoft.AspNetCore.Mvc.Rendering;
using WebMVC.DTOs;

namespace WebMVC.Models;

public class PaginatedPlatesViewModel
{
    public List<PlateBasicDto>? Plates { get; set; }

    public int CurrentPage { get; set; }

    public int PageSize { get; set; }

    public int TotalRecords { get; set; }

    public int TotalPages { get; set; }

    public bool HasNextPage { get; set; }

    public bool HasPreviousPage { get; set; }

    public string? SortOrder { get; set; }

    public IEnumerable<SelectListItem> SortOptions { get; set; } = Enumerable.Empty<SelectListItem>();
}
