﻿using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace OmniCore.Model.Interfaces.Platform
{
    public interface IViewModel : INotifyPropertyChanged, IDisposable
    {
        void InitializeModel(IView view);
    }

    public interface IViewModel<in T> : IViewModel
    {
        void InitializeModel(IView view, T parameter);
    }
}
