using NetworkService.Model;
using NetworkService.ViewModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Table_Tests
{
    [TestFixture]
    public class OnSearch_Tests
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
                new PressureInVentil(1, "Sensor1", "CableSensor", "path1"),
                new PressureInVentil(2, "Sensor2", "DigitalManometer", "path2"),
                new PressureInVentil(3, "Sensor3", "CableSensor", "path3")
            };
            });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }
        [Test]
        public void OnSearch_ValidTypeSelected_FiltersEntitiesByType()
        {
            // Arrange
            _viewModel.TypeSelected = true;
            _viewModel.SearchText = "CableSensor";

            // Act
            _viewModel.OnSearch();

            // Assert
            Assert.AreEqual(2, _viewModel.Entities.Count);
            Assert.IsTrue(_viewModel.Entities.All(e => e.Type.Contains("CableSensor")));
        }

        [Test]
        public void OnSearch_ValidNameSelected_FiltersEntitiesByName()
        {
            // Arrange
            _viewModel.TypeSelected = false;
            _viewModel.NameSelected = true;
            _viewModel.SearchText = "Sensor2";

            // Act
            _viewModel.OnSearch();

            // Assert
            Assert.AreEqual(1, _viewModel.Entities.Count);
            Assert.IsTrue(_viewModel.Entities.All(e => e.Name.Contains("Sensor2")));
        }

        [Test]
        public void OnSearch_InvalidSearch()
        {
            // Arrange
            _viewModel.SearchText = "";

            // Act
            _viewModel.OnSearch();

            // Assert
            Assert.AreEqual("Search input can't be empty!", _viewModel.SearchErrorLabel);
            Assert.AreEqual("Red", _viewModel.BorderBrushSearch);
        }
    }
}
