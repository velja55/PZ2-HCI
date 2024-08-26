using NetworkService.Model;
using NetworkService.ViewModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GridTests
{
    [TestFixture]
    public class OnEndDrawingTests
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
        public void OnEndDraw_ProveraLinija_KadaJeSveURedu()
        {
            // Arrange
            _viewModel.DrawSource = 0;
            string value = "1"; // Updated to match the target point index
            _viewModel.Points = new List<System.Windows.Point>
    {
        new System.Windows.Point(0, 0), // drawSource point
        new System.Windows.Point(100, 100) // target point
    };
            _viewModel.objectsOnCanvas = new List<PressureInVentil>
    {
        new PressureInVentil(0,"Ventil 1",NetworkService.CableSensorString,"image.png"), // index 0
        new PressureInVentil(1,"Ventil 2",NetworkService.DigitalManometarString,"image.png")  // index 1
    };
            _viewModel.LinesOnDisplay = new ObservableCollection<DisplayLine>();

            // Act
            _viewModel.OnEndDraw(value);

            // Assert
            Assert.That(_viewModel.LinesOnDisplay.Count, Is.EqualTo(1));
            Assert.That(_viewModel.LinesOnDisplay[0].X1, Is.EqualTo(0));
            Assert.That(_viewModel.LinesOnDisplay[0].Y1, Is.EqualTo(0));
            Assert.That(_viewModel.LinesOnDisplay[0].X2, Is.EqualTo(100));
            Assert.That(_viewModel.LinesOnDisplay[0].Y2, Is.EqualTo(100));
        }

        [Test]
        public void OnEndDraw_LinijaVecPostoji()
        {
            _viewModel.DrawSource = 0;
            string value = "1";
            _viewModel.LinesOnDisplay.Add(new DisplayLine(0, 100, 0, 100));
            _viewModel.OnEndDraw(value);
            Assert.That(_viewModel.LinesOnDisplay.Count, Is.EqualTo(1));
        }

        [Test]
        public void OnEndDraw_KadaJeSourceInvalid()
        {
            // Arrange
            _viewModel.DrawSource = -1; // Invalid source
            string value = "1";

            // Act
            _viewModel.OnEndDraw(value);

            // Assert
            Assert.That(_viewModel.LinesOnDisplay.Count, Is.EqualTo(0));
        }
        [Test]
        public void OnEndDraw_TargetInvalid()
        {
            // Arrange
            _viewModel.DrawSource = 0;
            string value = "-1"; // Invalid target
            _viewModel.LinesOnDisplay=new ObservableCollection<DisplayLine>();
            // Act
            _viewModel.OnEndDraw(value);
            // Assert
            Assert.That(_viewModel.LinesOnDisplay.Count, Is.EqualTo(0));
        }
        [Test]
        public void OnEndDraw_TargetNull()
        {
            _viewModel.DrawSource = 0;
            _viewModel.objectsOnCanvas[1] = null; // No object at target
            _viewModel.LinesOnDisplay = new ObservableCollection<DisplayLine>();
            string value = "1";
            _viewModel.OnEndDraw(value);
            Assert.That(_viewModel.LinesOnDisplay.Count, Is.EqualTo(0));
        }
    }
}
