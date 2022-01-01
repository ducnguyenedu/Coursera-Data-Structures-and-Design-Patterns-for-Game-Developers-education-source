using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Statistics container
/// </summary>
public static class Statistics
{
    static int[,] wins;

    /// <summary>
    /// Initializes the statistics container
    /// </summary>
    public static void Initialize()
    {
        wins = new int[6, 2];
        EventManager.AddGameOverListener(HandleGameOverEvent);
    }

    /// <summary>
    /// Gets the number of wins for the given player 
    /// and difficulties
    /// </summary>
    /// <returns>number of wins</returns>
    /// <param name="player">player</param>
    /// <param name="player1Difficulty">Player 1 difficulty</param>
    /// <param name="player2Difficulty">Player 2 difficulty</param>
    public static int GetWins(PlayerName player, 
        Difficulty player1Difficulty, Difficulty player2Difficulty)
    {
        int rowIndex = CalculateRowIndex(player1Difficulty,
            player2Difficulty);
        return wins[rowIndex, (int)player];
    }

    /// <summary>
    /// Updates the statistics for the given winner and difficulties
    /// </summary>
    /// <param name="winner">winner</param>
    /// <param name="player1Difficulty">Player 1 difficulty</param>
    /// <param name="player2Difficulty">Player 2 difficulty</param>
    static void HandleGameOverEvent(PlayerName winner,
        Difficulty player1Difficulty, Difficulty player2Difficulty)
    {
        int rowIndex = CalculateRowIndex(player1Difficulty,
            player2Difficulty);
        wins[rowIndex, (int)winner]++;
    }

    /// <summary>
    /// Calculates the row index for the given difficulties
    /// </summary>
    /// <returns>row index</returns>
    /// <param name="player1Difficulty">Player 1 difficulty</param>
    /// <param name="player2Difficulty">Player 2 difficulty</param>
    static int CalculateRowIndex(Difficulty player1Difficulty, 
        Difficulty player2Difficulty)
    {
        if (player1Difficulty == Difficulty.Easy &&
            player2Difficulty == Difficulty.Hard)
        {
            return 5;
        }
        else
        {
            return (int)player1Difficulty + (int)player2Difficulty;
        }
    }
}
