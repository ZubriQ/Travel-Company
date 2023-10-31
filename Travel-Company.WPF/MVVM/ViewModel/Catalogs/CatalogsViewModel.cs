using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
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

    private Visibility _isClassColumnVisible = Visibility.Collapsed;
    public Visibility IsClassColumnVisible
    {
        get => _isClassColumnVisible;
        private set
        {
            _isClassColumnVisible = value;
            OnPropertyChanged();
        }
    }

    private CatalogType _catalogType = CatalogType.None;
    public CatalogType CatalogType
    {
        get => _catalogType;
        set
        {
            _catalogType = value;
            OnPropertyChanged();
        }
    }

    private ICatalogItem _selectedCatalogItem = null!;
    public ICatalogItem SelectedCatalogItem
    {
        get => _selectedCatalogItem;
        set
        {
            _selectedCatalogItem = value;
            OnPropertyChanged();
        }
    }

    private string _searchText = string.Empty;
    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            OnPropertyChanged();
            FilterCatalog();
        }
    }

    private List<ICatalogItem> _fetchedCatalogList = null!;
    private List<ICatalogItem> _catalogItems = null!;
    public List<ICatalogItem> CatalogItems
    {
        get => _catalogItems;
        set
        {
            _catalogItems = value;
            OnPropertyChanged();
        }
    }

    private void FilterCatalog()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            CatalogItems = _fetchedCatalogList;
        }
        else
        {
            CatalogItems = _fetchedCatalogList
                .Where(item => item.Name.Contains(SearchText, StringComparison.CurrentCultureIgnoreCase))
                .ToList();
        }
    }

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
        GetCatalog();
        SetCatalog();
    }

    private void HandleCatalogTypeMessage(CatalogTypeMessage message)
    {
        _catalogType = message.CatalogType;
    }

    private void GetCatalog()
    {
        switch (_catalogType)
        {
            case CatalogType.Country:
                _fetchedCatalogList = new(_countriesRepository.GetAll());
                break;
            case CatalogType.Street:
                _fetchedCatalogList = new(_streetsRepository.GetAll());
                break;
            case CatalogType.Hotel:
                _fetchedCatalogList = new(_hotelsRepository.GetAll());
                IsClassColumnVisible = Visibility.Visible;
                break;
            case CatalogType.Place:
                _fetchedCatalogList = new(_placesRepository.GetAll());
                break;
            default:
                break;
        }
    }

    private void SetCatalog()
    {
        CatalogItems = _fetchedCatalogList;
    }
}