using ProgrammingAssignment3ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace ProgrammingAssignment3
{
    // Don't change ANY of the code in this file; if you
    // do, you'll break the automated grader!

    /// <summary>
    /// Programming Assignment 3
    /// </summary>
    class Program
    {
        const int PlayerOneGameObjectId = 1;
        const int PlayerTwoGameObjectId = 2;

        // test case to run
        static int testCaseNumber;

        // supporting test objects
        static Board board = new Board(new GameObject(0,
            new Transform(Vector3.zero)));
        static Player player1;
        static Player player2;

        // mapping between game object ids and thinking timers
        static Dictionary<int, ThinkingTimer> gameObjectIdThinkingTimers =
            new Dictionary<int, ThinkingTimer>();

        /// <summary>
        /// Tests Player and DontTakeTheLastTeddy classes
        /// </summary>
        /// <param name="args">command-line args</param>
        static void Main(string[] args)
        {
            // set up UnityEngine delegates
            GameObject.AddAddComponentDelegate(typeof(ThinkingTimer),
                AddThinkingTimerComponent);
            GameObject.AddGetComponentDelegate(typeof(Board), GetBoardComponent);
            GameObject.AddGetComponentDelegate(typeof(Player), GetPlayerComponent);
            GameObject.AddFindObjectByTagDelegate(FindGameObjectWithTag);

            // initialize dictionary and create players
            BuildGameObjectIdThinkingTimerDictionary();
            player1 = new Player(PlayerName.Player1, new GameObject(
                PlayerOneGameObjectId,
                new Transform(Vector3.zero)));
            player2 = new Player(PlayerName.Player2, new GameObject(
                PlayerTwoGameObjectId,
                new Transform(Vector3.zero)));

            // set up test objects
            DontTakeTheLastTeddy dontTakeTheLastTeddy =
                new DontTakeTheLastTeddy(new GameObject(3,
                new Transform(Vector3.zero)));
            dontTakeTheLastTeddy.Awake();
            Difficulty player1Difficulty = Difficulty.Easy;
            Difficulty player2Difficulty = Difficulty.Easy;

            // multiple test run support
            const int NumRunsPerTestCase = 100;
            PlayerName firstMovePlayer = PlayerName.Player1;

            // autograder testing
            const float SameDifficultyEpsilon = 0.3f;
            const float EasyMediumEpsilon = 0.4f;
            const float EasyHardEpsilon = 0.4f;
            const float MediumHardEpsilon = 0.2f;
            GameMonitor gameMonitor = new GameMonitor();

            // loop while there's more input
            string input = Console.ReadLine();
            while (input[0] != 'q')
            {
                // extract test case number from string
                GetInputValueFromString(input);

                // execute selected test case
                switch (testCaseNumber)
                {
                    case 1:
                        // play easy against easy games, alternating 
                        // starting player
                        player1Difficulty = Difficulty.Easy;
                        player2Difficulty = Difficulty.Easy;
                        for (int i = 0; i < NumRunsPerTestCase; i++)
                        {
                            firstMovePlayer = GetNewStartingPlayer(firstMovePlayer);
                            dontTakeTheLastTeddy.StartGame(firstMovePlayer,
                                player1Difficulty, player2Difficulty);
                        }
                        if (WithinTestEpsilon(SameDifficultyEpsilon,
                            Statistics.GetWins(PlayerName.Player1, player1Difficulty,
                            player2Difficulty),
                            Statistics.GetWins(PlayerName.Player2, player1Difficulty,
                            player2Difficulty),
                            ComparisonOrderConstraint.None))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 2:
                        // play medium against medium games, alternating 
                        // starting player
                        player1Difficulty = Difficulty.Medium;
                        player2Difficulty = Difficulty.Medium;
                        for (int i = 0; i < NumRunsPerTestCase; i++)
                        {
                            firstMovePlayer = GetNewStartingPlayer(firstMovePlayer);
                            dontTakeTheLastTeddy.StartGame(firstMovePlayer,
                                player1Difficulty, player2Difficulty);
                        }
                        if (WithinTestEpsilon(SameDifficultyEpsilon,
                            Statistics.GetWins(PlayerName.Player1, player1Difficulty,
                            player2Difficulty),
                            Statistics.GetWins(PlayerName.Player2, player1Difficulty,
                            player2Difficulty),
                            ComparisonOrderConstraint.None))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 3:
                        // play hard against hard games, alternating 
                        // starting player
                        player1Difficulty = Difficulty.Hard;
                        player2Difficulty = Difficulty.Hard;
                        for (int i = 0; i < NumRunsPerTestCase; i++)
                        {
                            firstMovePlayer = GetNewStartingPlayer(firstMovePlayer);
                            dontTakeTheLastTeddy.StartGame(firstMovePlayer,
                                player1Difficulty, player2Difficulty);
                        }
                        if (WithinTestEpsilon(SameDifficultyEpsilon,
                            Statistics.GetWins(PlayerName.Player1, player1Difficulty,
                            player2Difficulty),
                            Statistics.GetWins(PlayerName.Player2, player1Difficulty,
                            player2Difficulty),
                            ComparisonOrderConstraint.None))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 4:
                        // play easy against medium games, alternating 
                        // starting player
                        player1Difficulty = Difficulty.Easy;
                        player2Difficulty = Difficulty.Medium;
                        for (int i = 0; i < NumRunsPerTestCase; i++)
                        {
                            firstMovePlayer = GetNewStartingPlayer(firstMovePlayer);
                            dontTakeTheLastTeddy.StartGame(firstMovePlayer,
                                player1Difficulty, player2Difficulty);
                        }
                        if (WithinTestEpsilon(EasyMediumEpsilon,
                            Statistics.GetWins(PlayerName.Player1, player1Difficulty,
                            player2Difficulty),
                            Statistics.GetWins(PlayerName.Player2, player1Difficulty,
                            player2Difficulty),
                            ComparisonOrderConstraint.Player2Higher))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 5:
                        // play easy against hard games, alternating 
                        // starting player
                        player1Difficulty = Difficulty.Easy;
                        player2Difficulty = Difficulty.Hard;
                        for (int i = 0; i < NumRunsPerTestCase; i++)
                        {
                            firstMovePlayer = GetNewStartingPlayer(firstMovePlayer);
                            dontTakeTheLastTeddy.StartGame(firstMovePlayer,
                                player1Difficulty, player2Difficulty);
                        }
                        if (WithinTestEpsilon(EasyHardEpsilon,
                            Statistics.GetWins(PlayerName.Player1, player1Difficulty,
                            player2Difficulty),
                            Statistics.GetWins(PlayerName.Player2, player1Difficulty,
                            player2Difficulty),
                            ComparisonOrderConstraint.Player2Higher))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 6:
                        // play medium against hard games, alternating 
                        // starting player
                        player1Difficulty = Difficulty.Medium;
                        player2Difficulty = Difficulty.Hard;
                        for (int i = 0; i < NumRunsPerTestCase; i++)
                        {
                            firstMovePlayer = GetNewStartingPlayer(firstMovePlayer);
                            dontTakeTheLastTeddy.StartGame(firstMovePlayer,
                                player1Difficulty, player2Difficulty);
                        }
                        // note that we let medium win more than hard as
                        // long as the win percentages are close
                        if (WithinTestEpsilon(MediumHardEpsilon,
                            Statistics.GetWins(PlayerName.Player1, player1Difficulty,
                            player2Difficulty),
                            Statistics.GetWins(PlayerName.Player2, player1Difficulty,
                            player2Difficulty),
                            ComparisonOrderConstraint.NoneOrPlayer2Higher))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;

                    #region Duplicate test cases to fill out to 10

                    case 7:
                        // play easy against easy games, alternating 
                        // starting player
                        player1Difficulty = Difficulty.Easy;
                        player2Difficulty = Difficulty.Easy;
                        for (int i = 0; i < NumRunsPerTestCase; i++)
                        {
                            firstMovePlayer = GetNewStartingPlayer(firstMovePlayer);
                            dontTakeTheLastTeddy.StartGame(firstMovePlayer,
                                player1Difficulty, player2Difficulty);
                        }
                        if (WithinTestEpsilon(SameDifficultyEpsilon,
                            Statistics.GetWins(PlayerName.Player1, player1Difficulty,
                            player2Difficulty),
                            Statistics.GetWins(PlayerName.Player2, player1Difficulty,
                            player2Difficulty),
                            ComparisonOrderConstraint.None))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 8:
                        // play medium against medium games, alternating 
                        // starting player
                        player1Difficulty = Difficulty.Medium;
                        player2Difficulty = Difficulty.Medium;
                        for (int i = 0; i < NumRunsPerTestCase; i++)
                        {
                            firstMovePlayer = GetNewStartingPlayer(firstMovePlayer);
                            dontTakeTheLastTeddy.StartGame(firstMovePlayer,
                                player1Difficulty, player2Difficulty);
                        }
                        if (WithinTestEpsilon(SameDifficultyEpsilon,
                            Statistics.GetWins(PlayerName.Player1, player1Difficulty,
                            player2Difficulty),
                            Statistics.GetWins(PlayerName.Player2, player1Difficulty,
                            player2Difficulty),
                            ComparisonOrderConstraint.None))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 9:
                        // play easy against medium games, alternating 
                        // starting player
                        player1Difficulty = Difficulty.Easy;
                        player2Difficulty = Difficulty.Medium;
                        for (int i = 0; i < NumRunsPerTestCase; i++)
                        {
                            firstMovePlayer = GetNewStartingPlayer(firstMovePlayer);
                            dontTakeTheLastTeddy.StartGame(firstMovePlayer,
                                player1Difficulty, player2Difficulty);
                        }
                        if (WithinTestEpsilon(EasyMediumEpsilon,
                            Statistics.GetWins(PlayerName.Player1, player1Difficulty,
                            player2Difficulty),
                            Statistics.GetWins(PlayerName.Player2, player1Difficulty,
                            player2Difficulty),
                            ComparisonOrderConstraint.Player2Higher))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 10:
                        // play easy against hard games, alternating 
                        // starting player
                        player1Difficulty = Difficulty.Easy;
                        player2Difficulty = Difficulty.Hard;
                        for (int i = 0; i < NumRunsPerTestCase; i++)
                        {
                            firstMovePlayer = GetNewStartingPlayer(firstMovePlayer);
                            dontTakeTheLastTeddy.StartGame(firstMovePlayer,
                                player1Difficulty, player2Difficulty);
                        }
                        if (WithinTestEpsilon(EasyHardEpsilon,
                            Statistics.GetWins(PlayerName.Player1, player1Difficulty,
                            player2Difficulty),
                            Statistics.GetWins(PlayerName.Player2, player1Difficulty,
                            player2Difficulty),
                            ComparisonOrderConstraint.Player2Higher))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;

                        #endregion
                }

                input = Console.ReadLine();
            }
        }

        /// <summary>
        /// Extracts the test case number from the given input string
        /// </summary>
        /// <param name="input">input string</param>
        static void GetInputValueFromString(string input)
        {
            testCaseNumber = int.Parse(input);
        }

        /// <summary>
        /// Returns true is the relative percentage of wins for the
        /// players is within the given epsilon, false otherwise.
        /// 
        /// If the constraint parameter is set to Player2Higher, this is a 
        /// one-directional comparison where the epsilon is compared 
        /// to the player 2 win percentagecompared to the player 1 win
        /// percentage, where player 2 has to have a > epsilon win percentage 
        /// more than player 1
        /// 
        /// If the constraint parameter is set to None, it doesn't
        /// matter which player has a higher win percentage as long as 
        /// the difference between their win percentages is within the 
        /// given epsilon
        /// 
        /// If the constraint parameter is set to NoneOrPlayer2Higher, 
        /// either the player 1 win percentage has to be no more than
        /// epsilon higher than the player 2 win percentage or the 
        /// player 2 win percentage can be greater than epsilon higher
        /// than the player 1 wins percentage
        /// </summary>
        /// <param name="epsilon">epsilon</param>
        /// <param name="player1Wins">player 1 wins</param>
        /// <param name="player2Wins">player 2 wins</param>
        /// <param name="constraint">constraint on comparison order</param>
        /// <returns>true or false as described above</returns>
        static bool WithinTestEpsilon(float epsilon, int player1Wins, 
            int player2Wins, ComparisonOrderConstraint constraint)
        {
            int numGames = player1Wins + player2Wins;
            float player1WinPercentage = (float)player1Wins / numGames;
            float player2WinPercentage = (float)player2Wins / numGames;

            // autograder testing
            //Console.WriteLine("Player 1 Win %: " + player1WinPercentage);
            //Console.WriteLine("Player 2 Win %: " + player2WinPercentage);
            //Console.WriteLine("Epsilon: " + epsilon);
            //Console.WriteLine("Actual Difference: " +
            //    (player2WinPercentage - player1WinPercentage));

            if (constraint == ComparisonOrderConstraint.None)
            {
                return Mathf.Abs(player2WinPercentage - player1WinPercentage) <
                    epsilon;
            }
            else if (constraint == ComparisonOrderConstraint.Player2Higher)
            {
                return (player2WinPercentage - player1WinPercentage) > epsilon;
            }
            else
            {
                return (Mathf.Abs(player2WinPercentage - player1WinPercentage) <
                    epsilon) ||
                    ((player2WinPercentage - player1WinPercentage) > epsilon);
            }
        }

        /// <summary>
        /// Gets the name of the new starting player based on the
        /// current starting player
        /// </summary>
        /// <param name="currentStartingPlayer">current starting player</param>
        /// <returns>new starting player</returns>
        static PlayerName GetNewStartingPlayer(PlayerName currentStartingPlayer)
        {
            if (currentStartingPlayer == PlayerName.Player1)
            {
                return PlayerName.Player2;
            }
            else
            {
                return PlayerName.Player1;
            }
        }

        #region Delegates for Unity engine

        /// <summary>
        /// Builds the mapping between game objects and 
        /// thinking timers
        /// </summary>
        static void BuildGameObjectIdThinkingTimerDictionary()
        {
            gameObjectIdThinkingTimers.Clear();
            gameObjectIdThinkingTimers.Add(PlayerOneGameObjectId,
                new ThinkingTimer());
            gameObjectIdThinkingTimers.Add(PlayerTwoGameObjectId,
                new ThinkingTimer());
        }

        /// <summary>
        /// Delegate to add the ThinkingTimer component for a game object
        /// </summary>
        /// <param name="gameObject">game object</param>
        /// <returns>ThinkingTimer component</returns>
        static ThinkingTimer AddThinkingTimerComponent(GameObject gameObject)
        {
            if (gameObjectIdThinkingTimers.ContainsKey(gameObject.id))
            {
                return gameObjectIdThinkingTimers[gameObject.id];
            }
            else
            {
                // critical failure
                return null;
            }
        }

        /// <summary>
        /// Delegate to get the Board component for a game object
        /// </summary>
        /// <param name="gameObject">game object</param>
        /// <returns>Board component</returns>
        static Board GetBoardComponent(GameObject gameObject)
        {
            if (gameObject.id == board.gameObject.id)
            {
                return board;
            }
            else
            {
                // critical failure
                return null;
            }
        }

        /// <summary>
        /// Delegate to get the Player component for a game object
        /// </summary>
        /// <param name="gameObject">game object</param>
        /// <returns>Player component</returns>
        static Player GetPlayerComponent(GameObject gameObject)
        {
            if (gameObject.id == player1.gameObject.id)
            {
                return player1;
            }
            else if (gameObject.id == player2.gameObject.id)
            {
                return player2;
            }
            else
            {
                // critical failure
                return null;
            }
        }

        /// <summary>
        /// Delegate to find a game object by its tag
        /// </summary>
        /// <param name="tag">tag</param>
        /// <returns>corresponding game object</returns>
        static GameObject FindGameObjectWithTag(string tag)
        {
            if (tag == "Board")
            {
                return board.gameObject;
            }
            else if (tag == "Player1")
            {
                return player1.gameObject;
            }
            else if (tag == "Player2")
            {
                return player2.gameObject;
            }

            // should never get here
            return null;
        }

        #endregion

    }
}
