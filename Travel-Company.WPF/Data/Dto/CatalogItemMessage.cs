using Travel_Company.WPF.Models;

namespace Travel_Company.WPF.Data.Dto;

public class CatalogItemMessage
{
    public ICatalogItem CatalogItem { get; set; } = null!;
}