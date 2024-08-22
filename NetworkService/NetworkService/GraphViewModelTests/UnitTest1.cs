using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkService.ViewModel;
using NUnit.Framework;
using System;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace GraphViewModelTests
{
    public class GraphViewModelTests
    {
        private GraphViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            // Setup ViewModel instance before each test
            _viewModel = new GraphViewModel();
        }

        [Test]
        public void ChangeLinePositionsForToggle_WhenToggled_IsTrue_UpdatesLinePositions()
        {
            // Act
            _viewModel.ChangeLinePositionsForToggle(true);

            // Assert
            Assert.AreEqual(150, _viewModel.L1);
            Assert.AreEqual(232, _viewModel.L2);
            Assert.AreEqual(314, _viewModel.L3);
            Assert.AreEqual(396, _viewModel.L4);
            Assert.AreEqual(478, _viewModel.L5);
            Assert.AreEqual(100, _viewModel.LineBottom);
            Assert.AreEqual(520, _viewModel.LineBottom2);
        }

        [Test]
        public void ChangeLinePositionsForToggle_WhenToggled_IsFalse_UpdatesLinePositions()
        {
            // Act
            _viewModel.ChangeLinePositionsForToggle(false);

            // Assert
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
