using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Maze
{
    [DebuggerDisplay("X={X}, Y={Y}, {Facing}, {TraveledDistance}")]
    public class MazeState
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Facing { get; set; }
        public int TraveledDistance { get; set; }

        public MazeState()
        {
            X = 0;
            Y = 0;
            Facing = Direction.North;
            TraveledDistance = 0;
        }

        public MazeState(MazeState oldMazeState)
        {
            X = oldMazeState.X;
            Y = oldMazeState.Y;
            Facing = oldMazeState.Facing;
            TraveledDistance = oldMazeState.TraveledDistance;
        }


        public static MazeState Step(MazeState currentState)
        {
            MazeState newState = new MazeState(currentState);

            switch (newState.Facing)
            {
                case Direction.North:
                    newState.Y = newState.Y - 1;
                    break;
                case Direction.East:
                    newState.X = newState.X + 1;
                    break;
                case Direction.South:
                    newState.Y = newState.Y + 1;
                    break;
                case Direction.West:
                    newState.X = newState.X - 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            newState.TraveledDistance = newState.TraveledDistance + 1;

            return newState;
        }

        protected bool Equals(MazeState other)
        {
            return X == other.X && Y == other.Y && Facing == other.Facing && TraveledDistance == other.TraveledDistance;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MazeState) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = X;
                hashCode = (hashCode * 397) ^ Y;
                hashCode = (hashCode * 397) ^ (int) Facing;
                hashCode = (hashCode * 397) ^ TraveledDistance;
                return hashCode;
            }
        }
    }
}