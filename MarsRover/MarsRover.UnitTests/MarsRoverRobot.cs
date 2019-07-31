using System;
using System.Collections.Generic;

namespace MarsRover.UnitTests
{
    internal class MarsRoverRobot
    {
        private readonly (int, int) _grid;
        private Dictionary<string, string> _turningLeftDict;
        private Dictionary<string, string> _turningRightDict;
        private Dictionary<string, (int, int)> _movingDict;

        public MarsRoverRobot((int, int) grid)
        {
            _grid = grid;
            _turningLeftDict = new Dictionary<string, string> { { "W", "S" }, { "N", "W" }, { "S", "E" }, { "E", "N" } };
            _turningRightDict = new Dictionary<string, string> { { "W", "N" }, { "N", "E" }, { "S", "W" }, { "E", "S" } };
            _movingDict = new Dictionary<string, (int, int)> { { "W", (-1, 0) }, { "S", (0, -1) }, { "E", (1, 0) }, { "N", (0, 1) } };
        }

        public string Move(RoverPosition position, string instructions)
        {
            // split direction into a list
            // if element is L/R
            // then change direction to  N/E/S/W
            // if element is M
            // then change position by 1 in grid

            if (string.IsNullOrEmpty(instructions))
            {
                return $"{position.XCoord} {position.YCoord} {position.Direction}";
            }

            if (Equals(instructions[0], 'L'))
            {
                position.Direction = _turningLeftDict[position.Direction];
                Move(position, instructions.Remove(0,1));
            }

            if (Equals(instructions[0], 'R'))
            {
                position.Direction = _turningRightDict[position.Direction];
                Move(position, instructions.Remove(0,1));
            }

            if (Equals(instructions[0], 'M'))
            {
                position.XCoord += _movingDict[position.Direction].Item1;
                position.YCoord += _movingDict[position.Direction].Item2;
                Move(position, instructions.Remove(0,1));

                if (IsMarsRoverRobotOutOfBounds(_grid, position))
                {
                    throw new IndexOutOfRangeException();
                }
            }

            return $"{position.XCoord} {position.YCoord} {position.Direction}";
        }

        public override string ToString()
        {
            return base.ToString();
        }

        private static bool IsMarsRoverRobotOutOfBounds((int, int) grid, RoverPosition position)
        {
            return position.XCoord > grid.Item1 || position.YCoord > grid.Item2 || position.XCoord < 0 || position.YCoord < 0;
        }
    }
}