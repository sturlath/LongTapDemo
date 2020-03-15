using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.ObjectModel;

/// <summary>
/// This demo is based on https://github.com/stesvis/LongTapDemo and https://forums.xamarin.com/discussion/174434/is-it-possible-to-detect-a-long-tap-on-a-collectionview-item
/// </summary>
namespace LongTapDemo.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private ObservableCollection<Car> _cars;
        public ObservableCollection<Car> Cars
        {
            get { return _cars; }
            set { SetProperty(ref _cars, value); }
        }

        public Car LastTappedItem { get; set; }

        private DelegateCommand<object> _tapCommand;
        public DelegateCommand<object> TapCommand =>
            _tapCommand ?? (_tapCommand = new DelegateCommand<object>(ExecuteTapCommand));

        private void ExecuteTapCommand(object parameter)
        {
            if (parameter is Car car)
            {
                DialogService.DisplayAlertAsync("Tap works", $"Long tapped on {car.Name}", "OK");
            }
        }

        private DelegateCommand<object> _longTapCommand;
        public DelegateCommand<object> LongTapCommand =>
            _longTapCommand ?? (_longTapCommand = new DelegateCommand<object>(ExecuteLongTapCommand));

        public IPageDialogService DialogService { get; }

        private void ExecuteLongTapCommand(object parameter)
        {
            if (parameter is Car car)
            {
                DialogService.DisplayAlertAsync("LongTap WORKS but not 'short-tap'", $"Long tapped on {car.Name}", "OK");
            }
        }

        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            Title = "Main Page";

            Cars = new ObservableCollection<Car>
            {
                new Car { Id = 0, Name = "Ford F-150" },
                new Car { Id = 1, Name = "Dodge Ram 2500" },
                new Car { Id = 2, Name = "Toyota Tacoma" },
                new Car { Id = 3, Name = "Jeep Gladiator" },
                new Car { Id = 4, Name = "Chevrolet Silverado" },
            };
            DialogService = dialogService;
        }
    }

    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}