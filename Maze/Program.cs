using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Linq;

namespace Maze
{
    public class Program
    {
        private const string Address = "2a01:4f8:160:5263::e3d3:467f";
        private const int Port = 2766;
        private const string Key = "";
        private const int Delay = 110;

        public static void Main(string[] args)
        {
            TcpClient client = new TcpClient(Address, Port);
            NetworkStream stream = client.GetStream();

            string responseData = GetResponse(stream);
            Console.WriteLine(responseData);
            responseData = SendCommand(stream, Key);

            Console.WriteLine(responseData);

            if (responseData == "you can't start over again that quick. please wait")
            {
                Console.ReadKey();
                return;
            }

            HashSet<MazeState> states = new HashSet<MazeState>();
            MazeState currentState = new MazeState();
            states.Add(currentState);

            while (true)
            {
                Surroundings surroundings = Look(stream);
                string nextMove = NextMove(surroundings, states, currentState);

                switch (nextMove)
                {
                    case "left":
                        Console.WriteLine("turn left");
                        currentState.Facing = DirectionMethods.TurnLeft(currentState.Facing);
                        SendCommand(stream, "turn left");
                        break;
                    case "right":
                        Console.WriteLine("turn right");
                        currentState.Facing = DirectionMethods.TurnRight(currentState.Facing);
                        SendCommand(stream, "turn right");
                        break;
                    default:
                        Console.WriteLine("No change direction");
                        break;
                }

                SendData(stream, "look");
                string lookAgainResponse = GetResponse(stream);
                if (lookAgainResponse == "darkness" || lookAgainResponse == "doors")
                {
                    Console.WriteLine(lookAgainResponse);
                }
                else
                {
                    continue;
                }

                SendData(stream, "step");
                string response = GetResponse(stream);
                currentState = MazeState.Step(currentState);

                int visitCount = states.Count(state => state.X == currentState.X && state.Y == currentState.Y);
                if (visitCount > 1)
                {
                    Console.WriteLine($"Been there {visitCount} times");
                }

                states.Add(new MazeState(currentState));

                if (response == "ok")
                {
                    Console.WriteLine("step: "  + response);
                }
                else
                {
                    Console.WriteLine(response);
                }

                Thread.Sleep(Delay);
            }
        }

        private static string SendCommand(Stream stream, string command)
        {
            SendData(stream, command);
            string response = GetResponse(stream);
            Thread.Sleep(Delay);
            return response;
        }

        private static Surroundings Look(Stream stream)
        {
            string lookStraight = SendCommand(stream, "look");
            SendCommand(stream, "turn left");
            string lookLeft = SendCommand(stream, "look");
            SendCommand(stream, "turn right");
            SendCommand(stream, "turn right");
            string lookRight = SendCommand(stream, "look");
            SendCommand(stream, "turn left");

            return new Surroundings
            {
                Straight = lookStraight,
                Left = lookLeft,
                Right = lookRight
            };
        }

        private static string NextMove(Surroundings s, HashSet<MazeState> states, MazeState currentState)
        {
            if (s.Left == "doors")
            {
                return "left";
            }
            if (s.Right == "doors")
            {
                return "right";
            }

            if (s.Straight == "doors")
            {
                return null;
            }

            // dead end
            if (s.Straight == "wall" && s.Left == "wall" && s.Right == "wall")
            {
                return "left";
            }

            // only left
            if (s.Straight == "wall" && s.Left == "darkness" && s.Right == "wall")
            {
                return "left";
            }

            // only right
            if (s.Straight == "wall" && s.Left == "wall" && s.Right == "darkness")
            {
                return "right";
            }

            // only Straight
            if (s.Straight == "darkness" && s.Left == "wall" && s.Right == "wall")
            {
                return null;
            }

            // choose lowest visited
            int stepLeftVisited = int.MaxValue;
            int stepRightVisited = int.MaxValue;
            int stepStraightVisited = int.MaxValue;

            if (s.Left != "wall")
            {
                MazeState stepLeft = new MazeState(currentState);
                stepLeft.Facing = DirectionMethods.TurnLeft(stepLeft.Facing);
                stepLeft = MazeState.Step(stepLeft);

                stepLeftVisited = states.Count(state => state.X == stepLeft.X && state.Y == stepLeft.Y);
            }

            if (s.Right != "wall")
            {
                MazeState stepRight = new MazeState(currentState);
                stepRight.Facing = DirectionMethods.TurnRight(stepRight.Facing);
                stepRight = MazeState.Step(stepRight);

                stepRightVisited = states.Count(state => state.X == stepRight.X && state.Y == stepRight.Y);
            }

            if (s.Straight != "wall")
            {
                MazeState stepStraight = MazeState.Step(currentState);
                stepStraightVisited = states.Count(state => state.X == stepStraight.X && state.Y == stepStraight.Y);
            }

            int minimalVisits = new [] { stepLeftVisited, stepRightVisited, stepStraightVisited }.Min();

            if (stepLeftVisited == minimalVisits)
            {
                return "left";
            }
            if (stepRightVisited == minimalVisits)
            {
                return "right";
            }

            return null;

        }


        private static string GetResponse(Stream stream)
        {
            byte[] data = new byte[2048];
            int bytesCount = stream.Read(data, 0, data.Length);
            return Encoding.ASCII.GetString(data, 0, bytesCount);
        }

        private static void SendData(Stream stream, string message)
        {
            byte[] data = Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }
    }
}
