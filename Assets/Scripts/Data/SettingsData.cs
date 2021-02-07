using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SettingsData : ISubject
{
    [SerializeField] private bool _vibrationOff;
    [SerializeField] private bool _soundOff;
    [SerializeField] private bool _musicOff;
    [SerializeField] private string _currentLanguage;

    [SerializeField] private string[] _languages =
    {
        "Russian",
        "English",
        "Ukrainian"
    };

    private List<IObserver> _observers = new List<IObserver>();

    public bool VibrationOff
    {
        get => _vibrationOff;
        set
        {
            _vibrationOff = value;
            Notify();
        }
    }

    public bool SoundOff
    {
        get => _soundOff;
        set
        {
            _soundOff = value;
            Notify();
        }
    }

    public bool MusicOff
    {
        get => _musicOff;
        set
        {
            _musicOff = value;
            Notify();
        }
    }

    public string[] Languages => _languages;

    public string CurrentLanguage
    {
        get => _currentLanguage;
        set => _currentLanguage = value;
    }

    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in _observers)
            observer.Fetch(this);
    }

    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
    }
}

public interface IObserver
{
    void Fetch(ISubject subject);
}

public interface ISubject
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void Notify();
}