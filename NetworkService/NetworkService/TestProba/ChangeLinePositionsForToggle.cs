using NetworkService.ViewModel;
using NUnit.Framework;
using System.Threading;

namespace GraphTests
{
    [TestFixture]
    public class ChangeLinePositionsForToggle
    {
        private GraphViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            var staThread = new Thread(() =>
            {
                _viewModel = new GraphViewModel();
            });

            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }

        [Test]
        public void ChangeLinePositionsForToggle_KadaJeToggleTrue()
        { 
                _viewModel = new GraphViewModel();
                _viewModel.ChangeLinePositionsForToggle(true);
                Assert.That(_viewModel.L1, Is.EqualTo(150));
                Assert.That(_viewModel.L2, Is.EqualTo(232));
                Assert.That(_viewModel.L3, Is.EqualTo(314));
                Assert.That(_viewModel.L4, Is.EqualTo(396));
                Assert.That(_viewModel.L5, Is.EqualTo(478));
                Assert.That(_viewModel.LineBottom, Is.EqualTo(100));
                Assert.That(_viewModel.LineBottom2, Is.EqualTo(520));
        }

        [Test]
        public void ChangeLinePositionsForToggle_KadaJeToggleFalse()
        {
                _viewModel = new GraphViewModel();
                _viewModel.ChangeLinePositionsForToggle(false);

                Assert.AreEqual(60, _viewModel.L1);
                Assert.AreEqual(142, _viewModel.L2);
                Assert.AreEqual(224, _viewModel.L3);
                Assert.AreEqual(306, _viewModel.L4);
                Assert.AreEqual(388, _viewModel.L5);
                Assert.AreEqual(10, _viewModel.LineBottom);
                Assert.AreEqual(430, _viewModel.LineBottom2);
        }
    }
}