namespace MarsRover.UnitTests
{
    internal class RoverPosition
    {
        public int XCoord { get; set; }
        public int YCoord { get; set; }
        public string Direction { get; set; }

        public RoverPosition(int xCoord, int yCoord, string direction)
        {
            XCoord = xCoord;
            YCoord = yCoord;
            Direction = direction;
        }
    }
}