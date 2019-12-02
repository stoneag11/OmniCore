﻿using OmniCore.Client.Services;
using OmniCore.Model.Interfaces;
using System.Threading;
using Xamarin.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using OmniCore.Model.Exceptions;
using OmniCore.Model.Utilities;
using Unity;
using OmniCore.Client.Interfaces;
using OmniCore.Client.Views;
using System.IO;
using System;
using OmniCore.Client.Views.RadioTesting;
using OmniCore.Client.ViewModels.Test;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using OmniCore.Model.Interfaces.Platform;
using OmniCore.Model.Constants;
using OmniCore.Model.Interfaces.Services;
using OmniCore.Model.Interfaces.Workflow;

namespace OmniCore.Client
{
    public partial class XamarinApp : Application, IUserInterface
    {
        public static XamarinApp Instance => Application.Current as XamarinApp;
        public IPodProvider PodProvider { get; }
        public IRadioProvider RileyLinkProvider { get;  }
        public IOmniCoreLogger Logger { get; }
        public IOmniCoreApplication OmniCoreApplication { get; }

        public SynchronizationContext SynchronizationContext { get; }

        private readonly ICoreServices Services;
        public XamarinApp(ICoreServicesProvider coreServicesProvider)
        {
            Services = coreServicesProvider.LocalServices;
            InitializeComponent();
            SynchronizationContext = SynchronizationContext.Current;
            MainPage = new NavigationPage(new RadiosPage().WithViewModel(new RadioTestingViewModel()));
            //OmniCoreServices.Publisher.Subscribe(new RemoteRequestHandler());
            Logger.Information("OmniCore App initialized");
        }

        public void GoBack()
        {
            MainPage.SendBackButtonPressed();
        }

        protected async override void OnStart()
        {
            AppCenter.Start("android=51067176-2950-4b0e-9230-1998460d7981;", typeof(Analytics), typeof(Crashes));
            //Crashes.ShouldProcessErrorReport = report => !(report.Exception is OmniCoreException);
            Logger.Debug("OmniCore App OnStart called");
            await EnsurePermissions();
            //await Services.RepositoryService.Initialize();
        }

        protected override void OnSleep()
        {
            MessagingCenter.Send(this, MessagingConstants.AppSleeping);
            Logger.Debug("OmniCore App OnSleep called");
        }

        protected override void OnResume()
        {
            OmniCoreApplication.State.TryRemove(AppStateConstants.ActiveConversation);
            Logger.Debug("OmniCore App OnResume called");
            MessagingCenter.Send(this, MessagingConstants.AppResuming);
        }

        private async Task EnsurePermissions()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationAlways);
                if (status != PermissionStatus.Granted)
                {
                    await MainPage.DisplayAlert("Missing Permissions", "Please grant the location permission to this application in order to be able to connect to bluetooth devices.", "OK");
                    var request = await CrossPermissions.Current.RequestPermissionsAsync(Permission.LocationAlways);
                    if (request[Permission.LocationAlways] != PermissionStatus.Granted)
                    {
                        await MainPage.DisplayAlert("Missing Permissions", "OmniCore cannot run without the necessary permissions.", "OK");
                        XamarinApp.Instance.OmniCoreApplication.Exit();
                    }
                }
            }
            catch (Exception e)
            {
                await MainPage.DisplayAlert("Missing Permissions", "Error while querying / acquiring permissions", "OK");
                Crashes.TrackError(e);
                XamarinApp.Instance.OmniCoreApplication.Exit();
            }
        }

    }
}