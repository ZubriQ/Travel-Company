using System;
using System.Collections.Generic;

namespace Travel_Company.WPF.Models;

public partial class Route
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime StartDatetime { get; set; }

    public DateTime EndDatetime { get; set; }

    public decimal Cost { get; set; }

    public int CountryId { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<RoutesPopulatedPlace> RoutesPopulatedPlaces { get; set; } = new List<RoutesPopulatedPlace>();

    public virtual ICollection<TouristGroup> TouristGroups { get; set; } = new List<TouristGroup>();
}
