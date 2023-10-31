namespace Travel_Company.WPF.Models;

public partial class TourGuide
{
    public string FullName =>
        !string.IsNullOrEmpty(Patronymic)
            ? $"{LastName} {FirstName} {Patronymic}"
            : $"{LastName} {FirstName}";
}

public partial class Client
{
    public string FullName =>
        !string.IsNullOrEmpty(Patronymic)
            ? $"{LastName} {FirstName} {Patronymic}"
            : $"{LastName} {FirstName}";

    public string Passport => $"{PassportSeries} {PassportNumber}";
}