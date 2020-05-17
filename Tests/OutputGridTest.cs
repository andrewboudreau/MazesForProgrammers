using System;
using System.Threading;
using MazesForProgrammers.DataStructures;
using MazesForProgrammers.Mazes;
using MazesForProgrammers.Render;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazesForProgrammers.Tests
{
    [TestClass]
    public class OutputGridTest
    {
        public Grid Grid { get; protected set; }

        public RecursiveBacktracker Algorithm { get; protected set; }

        [TestInitialize]
        public void Setup()
        {
            Grid = new Grid();
            Algorithm = new RecursiveBacktracker();
        }

        [TestCleanup]
        public void Teardown()
        {
            var distances = Grid.RandomCell.Distances;

            Thread.Sleep(500);
            Grid
                .RenderToConsole(distances)
                .RenderImageAndOpen(distances);

            Thread.Sleep(500);
                Grid.RenderImageAndOpen($"maze_{DateTime.Now.Ticks}.png", 10);
        }
    }
}
