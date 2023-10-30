using System.Collections.ObjectModel;
using Travel_Company.WPF.Core;
using Travel_Company.WPF.Data.Base;
using Travel_Company.WPF.Data.Dto;
using Travel_Company.WPF.Models;
using Travel_Company.WPF.Services.Navigation;

namespace Travel_Company.WPF.MVVM.ViewModel.Catalogs;

public class CatalogsViewModel : Core.ViewModel
{
    private readonly IRepository<Country, int> _countriesRepository;
    private readonly IRepository<Street, long> _streetsRepository;
    private readonly IRepository<Hotel, long> _hotelsRepository;
    private readonly IRepository<PopulatedPlace, long> _placesRepository;

    private INavigationService _navigation = null!;
    public INavigationService Navigation
    {
        get => _navigation;
        set
        {
            _navigation = value;
            OnPropertyChanged();
        }
    }

    private CatalogType _catalogType = CatalogType.None;
    public CatalogType Employee
    {
        get => _catalogType;
        set
        {
            _catalogType = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<object> CatalogList { get; set; } = null!;

    public CatalogsViewModel(
        IRepository<Country, int> countriesRepository,
        IRepository<Street, long> streetsRepository,
        IRepository<Hotel, long> hotelsRepository,
        IRepository<PopulatedPlace, long> placesRepository)
    {
        _countriesRepository = countriesRepository;
        _streetsRepository = streetsRepository;
        _hotelsRepository = hotelsRepository;
        _placesRepository = placesRepository;
        App.EventAggregator.Subscribe<CatalogTypeMessage>(HandleCatalogTypeMessage);
        CatalogList = GetCatalog();
    }

    private void HandleCatalogTypeMessage(CatalogTypeMessage message)
    {
        _catalogType = message.CatalogType;
    }

    public ObservableCollection<object> GetCatalog()
    {
        var catalogList = new ObservableCollection<object>();

        switch (_catalogType)
        {
            case CatalogType.Country:
                catalogList = new ObservableCollection<object>(_countriesRepository.GetAll());
                break;
            case CatalogType.Street:
                catalogList = new ObservableCollection<object>(_streetsRepository.GetAll());
                break;
            case CatalogType.Hotel:
                catalogList = new ObservableCollection<object>(_hotelsRepository.GetAll());
                break;
            case CatalogType.Place:
                catalogList = new ObservableCollection<object>(_placesRepository.GetAll());
                break;
            default:
                break;
        }

        return catalogList;
    }
}