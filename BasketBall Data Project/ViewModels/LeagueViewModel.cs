﻿using BasketBall_Data_Project.Constants;
using BasketBall_Data_Project.Models.LeagueModel;
using BasketBall_Data_Project.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Essentials;

namespace BasketBall_Data_Project.ViewModels
{
    public class LeagueViewModel : BaseViewModel
    {
        public override string Title { get; set; } = Config.LeagueTitle;

        public ObservableCollection<Datum> LeaguesData { get; set; }
        public bool IsBusy { get; set; }
        public bool IsDataVisible { get; set; }
        public ICommand GetLeagues { get; }
        ILeagueApiService _leagueApiService;

        public LeagueViewModel(INavigationService navigationService, IPageDialogService pageDialogService, LeagueApiService leagueApiService) : base(navigationService, pageDialogService)
        {
            _leagueApiService = leagueApiService;
            GetLeagues = new DelegateCommand(GetLeaguesAsync);
        }
        private async void GetLeaguesAsync()
        {
            if (!(Connectivity.NetworkAccess == NetworkAccess.Internet))
            {
                await App.Current.MainPage.DisplayAlert("Error", "No internet access.", "OK");
                return;
            }
            IsDataVisible = false;
            IsBusy = true;
            var leagues = await _leagueApiService.GetInfoAsync();
            LeaguesData = leagues.Data;
            IsBusy = false;
            IsDataVisible = true;
        }
    }
}
