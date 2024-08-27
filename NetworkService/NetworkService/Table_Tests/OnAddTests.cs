using Castle.Core.Resource;
using Moq;
using NetworkService.Model;
using NetworkService.ViewModel;
using NUnit.Framework;
using System.Collections.ObjectModel;
using System.Linq;
using System.Resources;
using System.Threading;

namespace Table_Tests
{
    [TestFixture]
    public class OnAddTests
    {
        private TableViewModel _viewModel;

        [SetUp]
        public void SetUp()
        {
            var staThread = new Thread(() =>
            {
                ListEntities.pressureInVentils = new ObservableCollection<PressureInVentil>();

            _viewModel = new TableViewModel
            {
                ID = "1",
                NameText = "Test Name",
                TypeText = NetworkService.CableSensorString,
                Entities = ListEntities.pressureInVentils
            };
            });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }

        [Test]
        public void OnAdd_ValidInputs_AddsEntity()
        {
            // Arrange
            _viewModel.ID = "1";
            _viewModel.NameText = "Test Name";
            _viewModel.TypeText = NetworkService.CableSensorString;
            

            // Act
            _viewModel.OnAdd();

            // Assert
            Assert.AreEqual(1, ListEntities.pressureInVentils.Count);
            var addedEntity = ListEntities.pressureInVentils.First();
            Assert.AreEqual(1, addedEntity.Id);
            Assert.AreEqual("Test Name", addedEntity.Name);
            Assert.AreEqual(NetworkService.CableSensorString, addedEntity.Type);
            Assert.AreEqual("C:\\Users\\lukic\\Desktop\\fax3.godina\\2.semestar\\HCI\\PZ2Z\\PZ2-HCI\\NetworkService\\NetworkService\\NetworkService\\Images\\cable.jpg", addedEntity.Image);
        }

        [Test]
        public void OnAdd_InvalidInputs()
        {
            // Arrange
            _viewModel.ID = "1";
            _viewModel.NameText = ""; // Invalid input
            _viewModel.TypeText =NetworkService.CableSensorString;
            // Act
            _viewModel.OnAdd();

            // Assert
            Assert.AreEqual(0, ListEntities.pressureInVentils.Count);
        }

        [Test]
        public void OnAdd_EntitetSaIstimIdVecpostoji()
        {
            // Arrange
            ListEntities.pressureInVentils.Add(new PressureInVentil(1, "Existing Name",NetworkService.CableSensorString, "path"));
            _viewModel.ID = "1";
            _viewModel.NameText = "New Name";
            _viewModel.TypeText =NetworkService.CableSensorString;
            // Act
            _viewModel.OnAdd();

            // Assert
            Assert.AreEqual(1, ListEntities.pressureInVentils.Count); // No new entity should be added
            var existingEntity = ListEntities.pressureInVentils.First();
            Assert.AreEqual(1, existingEntity.Id); // Entity with ID 1 should already exist
            Assert.AreEqual("Existing Name", existingEntity.Name);
        }
    }
}