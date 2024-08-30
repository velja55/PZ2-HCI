using NetworkService.Model;
using NetworkService.Properties;
using NetworkService.ViewModel;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;

namespace GraphTests
{
    [TestFixture]
    public class SelectionChanged
    {
        private GraphViewModel _viewModel;
        private PressureInVentil _pressureInVentil;
        [SetUp]
        public void Setup()
        {
            var staThread = new Thread(() =>
            {
                _viewModel = new GraphViewModel();
                _pressureInVentil = new PressureInVentil(1, "TestVentil", Resources.CableSensorString, "TestImage.png")
                {
                    lastFive = new List<double> { 5, 15, 25, 10, 4 },
                    lastFiveTime = new List<string> { "10:00", "11:00", "12:00", "13:00", "14:00" }
                };
            });

            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }

        [Test]
        public void OnSelectionChanged_UpdateSelectedEntityAndProperties()
        {
            // Act
            _viewModel.OnSelectionChanged(_pressureInVentil);

            // Assert
            Assert.AreEqual(1, _viewModel.SelectedId);
            Assert.AreEqual("TestVentil", _viewModel.SelectedName);
            Assert.AreEqual(0, _viewModel.SelectedValue);  // Početna vrednost je 0 jer se vrednost nije menjala
            Assert.AreEqual("TestImage.png", _viewModel.SelectedImage);
            Assert.AreEqual(11, _viewModel.Radius1);
            Assert.AreEqual(31, _viewModel.Radius2);
            Assert.AreEqual(51, _viewModel.Radius3);
            Assert.AreEqual(21, _viewModel.Radius4);
            Assert.AreEqual(9, _viewModel.Radius5);
            // Provera boja
            Assert.AreEqual(Resources.ColorBlue, _viewModel.Color1);
            Assert.AreEqual(Resources.ColorBlue, _viewModel.Color2);
            Assert.AreEqual(Resources.RedColor, _viewModel.Color3);
            Assert.AreEqual(Resources.ColorBlue, _viewModel.Color4);
            Assert.AreEqual(Resources.RedColor, _viewModel.Color5);
            // Provera tekstova
            Assert.AreEqual("10:00", _viewModel.Text1);
            Assert.AreEqual("11:00", _viewModel.Text2);
            Assert.AreEqual("12:00", _viewModel.Text3);
            Assert.AreEqual("13:00", _viewModel.Text4);
            Assert.AreEqual("14:00", _viewModel.Text5);
        }
        [Test]
        public void OnSelectionChanged_PodesenSelectedEntityCorrectly()
        {
            // Arrange
            var entity = new PressureInVentil(2, "Ventil 2", "Type B", "image.png");

            // Act
            _viewModel.OnSelectionChanged(entity);

            // Assert
            Assert.That(_viewModel.SelectedEntity, Is.EqualTo(entity));
        }

        [Test]
        public void OnSelectionChanged_SetColorsBasedOnValues()
        {
            // Arrange
            var entity = new PressureInVentil(3, "Ventil 3", "Type C", "image.png")
            {
                lastFive = new List<double> { 5, 10, 3, 15, 20 } 
            };

            // Act
            _viewModel.OnSelectionChanged(entity);

            // Assert
            Assert.That(_viewModel.Color1, Is.EqualTo(Resources.ColorBlue));
            Assert.That(_viewModel.Color2, Is.EqualTo(Resources.ColorBlue));
            Assert.That(_viewModel.Color3, Is.EqualTo(Resources.RedColor));  
            Assert.That(_viewModel.Color4, Is.EqualTo(Resources.ColorBlue));
            Assert.That(_viewModel.Color5, Is.EqualTo(Resources.RedColor));  
        }

        [Test]
        public void OnSelectionChanged_Should_SetRadiusValuesCorrectly()
        {
            // Arrange
            var entity = new PressureInVentil(4, "Ventil 4", "Cable sensor", "image.png")
            {
                lastFive = new List<double> { 3, 6, 9, 12, 15 } 
            };

            // Act
            _viewModel.OnSelectionChanged(entity);

            // Assert
            Assert.That(_viewModel.Radius1, Is.EqualTo(7));  // 3 * 2 + 1
            Assert.That(_viewModel.Radius2, Is.EqualTo(13)); // 6 * 2 + 1
            Assert.That(_viewModel.Radius3, Is.EqualTo(19)); // 9 * 2 + 1
            Assert.That(_viewModel.Radius4, Is.EqualTo(25)); // 12 * 2 + 1
            Assert.That(_viewModel.Radius5, Is.EqualTo(31)); // 15 * 2 + 1
        }

        [Test]
        public void OnSelectionChanged_Should_SetTextValuesCorrectly()
        {
            // Arrange
            var entity = new PressureInVentil(5, "Ventil 5", "Digital manometar", "image.png")
            {
                lastFiveTime = new List<string> { "10:00", "10:05", "10:10", "10:15", "10:20" }
            };

            // Act
            _viewModel.OnSelectionChanged(entity);

            // Assert
            Assert.That(_viewModel.Text1, Is.EqualTo("10:00"));
            Assert.That(_viewModel.Text2, Is.EqualTo("10:05"));
            Assert.That(_viewModel.Text3, Is.EqualTo("10:10"));
            Assert.That(_viewModel.Text4, Is.EqualTo("10:15"));
            Assert.That(_viewModel.Text5, Is.EqualTo("10:20"));
        }

        [Test]
        public void OnSelectionChanged_Should_SetSelectedIdNameAndValueCorrectly()
        {
            var entity = new PressureInVentil(6, "Ventil 6", "Cable Sensor", "image.png")
            {
                Value = 123.45
            };
            _viewModel.OnSelectionChanged(entity);
            Assert.That(_viewModel.SelectedId, Is.EqualTo(6));
            Assert.That(_viewModel.SelectedName, Is.EqualTo("Ventil 6"));
            Assert.That(_viewModel.SelectedValue, Is.EqualTo(123.45));
        }
    }
}
