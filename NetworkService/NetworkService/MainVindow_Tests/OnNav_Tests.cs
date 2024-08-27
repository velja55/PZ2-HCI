using System.Threading;
using NetworkService.Model;
using NetworkService.ViewModel;
using NUnit.Framework;

namespace MainVindow_Tests
{
    public class OnNav_Tests
    {
        private MainWindowViewModel _viewModel;
        [SetUp]
        public void Setup()
        {
            var staThread = new Thread(() =>
            {
                // Initialize the ViewModel
                _viewModel = new MainWindowViewModel();

                // Ensure that the ViewModel's navigation dictionary is initialized with mock data
                _viewModel.navigationDictionaty = new NavigationDictionary();

            });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }
        [Test]
        public void OnNav_HomeDestination_UpdatesViewModelToHome()
        {
            // Act
            _viewModel.OnNav("home");

            // Assert
            Assert.AreEqual("HOME VIEW", _viewModel.Title);
            Assert.AreEqual(Resources.ColorLightBlue, _viewModel.Colors[0]);
            Assert.AreEqual(NetworkService.ColorWhite, _viewModel.Colors[1]);
            Assert.AreEqual(NetworkService.ColorWhite, _viewModel.Colors[2]);
            Assert.AreEqual(NetworkService.ColorWhite, _viewModel.Colors[3]);
            //Assert.AreEqual(NetworkService.HomeHelp, _viewModel.Help);
            Assert.AreEqual(_viewModel.navigationDictionaty.collection["home"].ViewModel, _viewModel.CurrentViewModel);
        }

        [Test]
        public void OnNav_TableDestination_UpdatesViewModelToTable()
        {
            // Act
            _viewModel.OnNav("table");

            // Assert
            Assert.AreEqual("TABLE VIEW", _viewModel.Title);
            Assert.AreEqual(NetworkService.ColorWhite, _viewModel.Colors[0]);
            Assert.AreEqual(NetworkService.ColorLightBlue, _viewModel.Colors[1]);
            Assert.AreEqual(NetworkService.ColorWhite, _viewModel.Colors[2]);
            Assert.AreEqual(NetworkService.ColorWhite, _viewModel.Colors[3]);
            //Assert.AreEqual(NetworkService.TableHelp, _viewModel.Help);
            Assert.AreEqual(_viewModel.navigationDictionaty.collection["table"].ViewModel, _viewModel.CurrentViewModel);
        }

        [Test]
        public void OnNav_InvalidDestination_DoesNotUpdateViewModel()
        {
            // Arrange
            var previousViewModel = _viewModel.CurrentViewModel;

            // Act
            _viewModel.OnNav("invalid");

            // Assert
            Assert.AreEqual(previousViewModel, _viewModel.CurrentViewModel);
        }
    }
}