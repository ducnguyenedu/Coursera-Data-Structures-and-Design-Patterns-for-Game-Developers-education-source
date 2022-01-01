using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignment3
{
    /// <summary>
    /// Monitors the current game and prints out status messages
    /// 
    /// This class wasn't included in the Unity game, I wrote it to use
    /// in the autograder
    /// </summary>
    public class GameMonitor
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public GameMonitor()
        {
            // add listeners for all events
            //EventManager.AddTakeTurnListener(TakeTurnListener);
            //EventManager.AddTurnOverListener(TurnOverListener);
            //EventManager.AddGameOverListener(GameOverListener);
            //EventManager.AddGameStartingListener(GameStartingListener);
        }

        #endregion

        #region Listeners

        /// <summary>
        /// Listens for the TakeTurn event
        /// </summary>
        /// <param name="player">player who should take their turn</param>
        /// <param name="unused">unused</param>
        void TakeTurnListener(PlayerName player, Configuration unused)
        {
            Console.WriteLine(player.ToString() + " starting to take turn");
        }

        /// <summary>
        /// Listens for the TurnOver event
        /// </summary>
        /// <param name="player">player who just finished taking their turn</param>
        /// <param name="unused">unused</param>
        void TurnOverListener(PlayerName player, Configuration unused)
        {
            Console.WriteLine(player.ToString() + " finished taking turn");
        }

        /// <summary>
        /// Listens for the GameOver event
        /// </summary>
        /// <param name="player">player who won the game</param>
        /// <param name="player1Difficulty">player 1 difficulty</param>
        /// <param name="player2Difficulty">player 2 difficulty</param>
        void GameOverListener(PlayerName player, Difficulty player1Difficulty,
            Difficulty player2Difficulty)
        {
            Console.WriteLine(player.ToString() + " won the game of " +
                player1Difficulty.ToString() + " against " +
                player2Difficulty.ToString());
        }

        /// <summary>
        /// Listens for the GameStarting event
        /// </summary>
        void GameStartingListener()
        {
            Console.WriteLine("Game starting");
        }

        #endregion
    }
}
