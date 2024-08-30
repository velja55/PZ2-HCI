using NetworkService.Model;
using NetworkService.ViewModel;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GridTests
{
    [TestFixture]
    public class UpdateValueOnCanvas_Tests
    {
        private GridViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            _viewModel = new GridViewModel
            {
                SelectedId = new ObservableCollection<string> { "1", "2" },
                SelectedValue = new ObservableCollection<string> { "OldValue1", "OldValue2" },
                BorderBrushes = new ObservableCollection<string> { "Black", "Black" },
                GridBackgrounds = new ObservableCollection<string> { "LightSteelBlue", "LightSteelBlue" },
                objectsOnCanvas = new List<PressureInVentil>
                {
                    new PressureInVentil(1, "Value1", "Cable sensor", "image1"),
                    new PressureInVentil(2, "Value2", "Digital manometar", "image2")
                }
            };
        }

        [Test]
        public void UpdateValueOnCanvas_PressureValueVanOpsega()
        {
            // Arrange
            var pressure = new PressureInVentil(2, "UpdatedValue", "Digital manometar", "image2");
            pressure.Value = 3; // Set value to be out of range

            // Act
            _viewModel.UpdateValueOnCanvas(pressure);

            // Assert
            Assert.AreEqual("2", _viewModel.SelectedId[1], "SelectedId should be updated.");
            Assert.AreEqual("3", _viewModel.SelectedValue[1], "SelectedValue should be updated.");
            Assert.AreEqual("Red", _viewModel.BorderBrushes[1], "BorderBrushes should be Red for out of range values.");
            Assert.AreEqual("HotPink", _viewModel.GridBackgrounds[1], "GridBackgrounds should be HotPink for out of range values.");
        }

        [Test]
        public void UpdateValueOnCanvas_KadaValueNijeSelektovan()
        {
            // Arrange
            var pressure = new PressureInVentil(3, "UpdatedValue", "Cable sensor", "image3");

            // Act
            _viewModel.UpdateValueOnCanvas(pressure);

            // Assert
            Assert.AreEqual("1", _viewModel.SelectedId[0], "SelectedId should remain unchanged.");
            Assert.AreEqual("OldValue1", _viewModel.SelectedValue[0], "SelectedValue should remain unchanged.");
            Assert.AreEqual("Black", _viewModel.BorderBrushes[0], "BorderBrushes should remain unchanged.");
            Assert.AreEqual("LightSteelBlue", _viewModel.GridBackgrounds[0], "GridBackgrounds should remain unchanged.");
        }

    }
}
