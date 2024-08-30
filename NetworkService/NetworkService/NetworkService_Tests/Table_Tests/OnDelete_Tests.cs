using NetworkService.Model;
using NetworkService.ViewModel;
using NUnit.Framework;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;

namespace Table_Tests
{
    [TestFixture]
    public class OnDelete_Tests
    {
        private TableViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            var staThread = new Thread(() =>
            {
                _viewModel = new TableViewModel();
            _viewModel.Entities = new ObservableCollection<PressureInVentil>
            {
                new PressureInVentil(1, "Entity1", "Type1", "Path1"),
                new PressureInVentil(2, "Entity2", "Type2", "Path2"),
            };
            });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }

        [Test]
        public void OnDelete_EntitySelected_DeletesEntity()
        {
            _viewModel.SelectedEntity = _viewModel.Entities[0];
            MessageBoxResult result = MessageBoxResult.Yes;
            MessageBoxResult expectedResult = MessageBoxResult.Yes;
            if (result == expectedResult)
            {
                _viewModel.OnDelete();
            }
            Assert.IsFalse(_viewModel.Entities.Contains(_viewModel.SelectedEntity));
        }

        [Test]
        public void OnDelete_NoEntitySelected()
        {
            _viewModel.SelectedEntity = null;
            MessageBoxResult result = MessageBoxResult.No;
            MessageBoxResult expectedResult = MessageBoxResult.No;
            if (result == expectedResult)
            {
                _viewModel.OnDelete();
            }
            Assert.AreEqual(2, _viewModel.Entities.Count);
        }

        [Test]
        public void OnDelete_EntitySelected()
        {
            _viewModel.SelectedEntity = _viewModel.Entities[1];
            MessageBoxResult result = MessageBoxResult.Yes;
            MessageBoxResult expectedResult = MessageBoxResult.Yes;
            if (result == expectedResult)
            {
                _viewModel.OnDelete();
            }
            Assert.AreEqual(1, _viewModel.Entities.Count);
            Assert.IsFalse(_viewModel.Entities.Contains(_viewModel.SelectedEntity));
        }
    }
}
