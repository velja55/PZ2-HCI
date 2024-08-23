using NetworkService.ViewModel;
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
        public void OnStartDraw_WhenObjectOnCanvasIsNull_ShouldSetDrawSourceAndDrawTargetToMinusOne()
        {
            // Arrange
            int index = 1; // Indeks gde je objekat null
            _viewModel.objectsOnCanvas[index] = null;

            // Act
            _viewModel.OnStartDraw(index.ToString());

            // Assert
            Assert.That(_viewModel.drawSource, Is.EqualTo(-1));
            Assert.That(_viewModel.drawTarget, Is.EqualTo(-1));
        }

        [Test]
        public void OnStartDraw_WhenObjectOnCanvasIsNotNull_ShouldSetDrawSourceToIndexAndDrawTargetToMinusOne()
        {
            // Arrange
            int index = 1; // Indeks gde objekat nije null
            var pressureInVentil = new PressureInVentil(1, "Test", "Digital manometar", "ImagePath");
            _viewModel.objectsOnCanvas[index] = pressureInVentil;

            // Act
            _viewModel.OnStartDraw(index.ToString());

            // Assert
            Assert.That(_viewModel.drawSource, Is.EqualTo(index));
            Assert.That(_viewModel.drawTarget, Is.EqualTo(-1));
        }

        [Test]
        public void OnStartDraw_InvalidIndex_ShouldNotThrowException()
        {
            // Arrange
            int index = 100; // Indeks koji je van granica liste

            // Act & Assert
            Assert.DoesNotThrow(() => _viewModel.OnStartDraw(index.ToString()));
        }

        [Test]
        public void OnStartDraw_WhenCalledWithNonNumericValue_ShouldNotThrowException()
        {
            // Arrange
            string nonNumericValue = "abc";

            // Act & Assert
            Assert.DoesNotThrow(() => _viewModel.OnStartDraw(nonNumericValue));
        }
    }
}
}