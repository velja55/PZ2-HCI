using NetworkService.Model;
using NetworkService.ViewModel;
using NUnit.Framework;
using System.Threading;

namespace GridTests
{
    public class Tests
    {
        private GridViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            var staThread = new Thread(() =>
            {
                _viewModel = new GridViewModel();
            });

            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }


        [Test]
        public void OnStartDraw_KadaJeCanvasPrazan()
        {
            // Arrange
            int index = 1; // Indeks gde je objekat null
            _viewModel.objectsOnCanvas[index] = null;

            // Act
            _viewModel.OnStartDraw(index.ToString());

            // Assert
            Assert.That(_viewModel.DrawSource, Is.EqualTo(-1));
            Assert.That(_viewModel.DrawTarget, Is.EqualTo(-1));
        }

        [Test]
        public void OnStartDraw_KadaCanvasNijePrazan()
        {
            // Arrange
            int index = 1; // Indeks gde objekat nije null
            var pressureInVentil = new PressureInVentil(1, "Test", "Digital manometar", "ImagePath");
            _viewModel.objectsOnCanvas[index] = pressureInVentil;

            // Act
            _viewModel.OnStartDraw(index.ToString());

            // Assert
            Assert.That(_viewModel.DrawSource, Is.EqualTo(index));
            Assert.That(_viewModel.DrawTarget, Is.EqualTo(-1));
        }

    }
}
