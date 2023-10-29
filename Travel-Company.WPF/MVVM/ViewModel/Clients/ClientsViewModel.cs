using System.Collections.Generic;
using Travel_Company.WPF.Data.Base;
using Travel_Company.WPF.Models;

namespace Travel_Company.WPF.MVVM.ViewModel.Clients;

public sealed class ClientsViewModel : Core.ViewModel
{
    private readonly IRepository<Client, int> _clientsRepository;

    public ClientsViewModel(IRepository<Client, int> repository)
    {
        _clientsRepository = repository;
        _clients = _clientsRepository.GetAll();
    }

    private List<Client> _clients;
    public List<Client> Clients
    {
        get => _clients;
        set
        {
            _clients = value;
            OnPropertyChanged();
        }
    }
}
