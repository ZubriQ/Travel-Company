using System;
using System.Collections.Generic;

namespace Travel_Company.WPF.Data;

public class EventAggregator
{
    private readonly Dictionary<Type, List<Action<object>>> _eventSubscribers;

    public EventAggregator()
    {
        _eventSubscribers = new Dictionary<Type, List<Action<object>>>();
    }

    public void Publish<TMessage>(TMessage message)
    {
        var messageType = typeof(TMessage);

        if (_eventSubscribers.ContainsKey(messageType))
        {
            var allSubscribers = _eventSubscribers[messageType];
            foreach (var subscriber in allSubscribers)
            {
                subscriber(message);
            }
        }
    }

    public void Subscribe<TMessage>(Action<TMessage> action)
    {
        var messageType = typeof(TMessage);

        if (!_eventSubscribers.ContainsKey(messageType))
        {
            _eventSubscribers[messageType] = new List<Action<object>>();
        }

        _eventSubscribers[messageType].Add(m => action((TMessage)m));
    }
}

