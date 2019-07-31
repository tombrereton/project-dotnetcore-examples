using System;
using System.Linq;
using NUnit.Framework;

namespace MarsRover.UnitTests
{
    class MarsRoverShould
    {
        [TestCase("", "1 2 N")]
        [TestCase("L", "1 2 W")]
        [TestCase("LM", "0 2 W")]
        [TestCase("LML", "0 2 S")]
        [TestCase("LMLM", "0 1 S")]
        [TestCase("LMLML", "0 1 E")]
        [TestCase("LMLMLM", "1 1 E")]
        [TestCase("LMLMLML", "1 1 N")]
        [TestCase("LMLMLMLM", "1 2 N")]
        [TestCase("LMLMLMLMM", "1 3 N")]
        [TestCase("R", "1 2 E")]
        [TestCase("RR", "1 2 S")]
        [TestCase("RRR", "1 2 W")]
        [TestCase("RRRR", "1 2 N")]
        public void BeInTheCorrectPositionGivenTheStartingPosition(string instructions, string expectedPosition)
        {
            var grid = (5, 5);
            var startingPosition = new RoverPosition(1, 2, "N");

            var marsRoverRobot = new MarsRoverRobot(grid);

            var actual = marsRoverRobot.Move(startingPosition, instructions);

            Assert.That(actual, Is.EqualTo(expectedPosition));
        }

        [Test]
        public void ThrowHitBoundaryExceptionWhenRoverPositionIsOutsideGrid()
        {
            var grid = (5, 5);
            var startingPosition = new RoverPosition(1, 2, "N");
            var marsRoverRobot = new MarsRoverRobot(grid);

            var instructions = "MMMM";

            Assert.Throws<IndexOutOfRangeException>(() => marsRoverRobot.Move(startingPosition, instructions));

        }

        [Test]
        public void ThrowHitBoundaryExceptionWhenRoverPositionIsOutsideGrid2()
        {
            var grid = (5, 5);
            var startingPosition = new RoverPosition(1, 2, "N");
            var marsRoverRobot = new MarsRoverRobot(grid);

            var instructions = "LMM";

            Assert.Throws<IndexOutOfRangeException>(() => marsRoverRobot.Move(startingPosition, instructions));

        }
    }
}
