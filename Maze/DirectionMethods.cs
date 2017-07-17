using System;

namespace Maze
{
    public static class DirectionMethods
    {
        public static Direction TurnLeft(Direction currentDirection)
        {
            switch (currentDirection)
            {
                case Direction.North:
                    return Direction.West;
                case Direction.West:
                    return Direction.South;
                case Direction.South:
                    return Direction.East;
                case Direction.East:
                    return Direction.North;
                default:
                    throw new ArgumentOutOfRangeException(nameof(currentDirection), currentDirection, null);
            }
        }

        public static Direction TurnRight(Direction currentDirection)
        {
            switch (currentDirection)
            {
                case Direction.North:
                    return Direction.East;
                case Direction.East:
                    return Direction.South;
                case Direction.South:
                    return Direction.West;
                case Direction.West:
                    return Direction.North;
                default:
                    throw new ArgumentOutOfRangeException(nameof(currentDirection), currentDirection, null);
            }
        }
    }
}