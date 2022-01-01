using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Game manager
/// </summary>
public class DontTakeTheLastTeddy : MonoBehaviour, ITakeTurnInvoker, IGameOverInvoker,
    IGameStartingInvoker
{
    Board board;
    Player player1;
    Player player2;

    // multiple games support
    Timer newGameDelayTimer;
    PlayerName firstMovePlayer = PlayerName.Player1;
    const int TotalGames = 600;
    int gamesPlayed = 0;

    // events invoked by class
    TakeTurn takeTurnEvent = new TakeTurn();
    GameOver gameOverEvent = new GameOver();
    GameStarting gameStartingEvent = new GameStarting();

    #region Constructor

    // Uncomment the code below after copying this class into the console
    // app for the automated grader. DON'T uncomment it now; it won't
    // compile in a Unity project

    /// <summary>
    /// Constructor
    /// 
    /// Note: The class in the Unity solution doesn't use a constructor;
    /// this constructor is to support automated grading
    /// </summary>
    /// <param name="gameObject">the game object the script is attached to</param>
    //public DontTakeTheLastTeddy(GameObject gameObject) :
    //    base(gameObject)
    //{
    //}

    #endregion

    /// <summary>
    /// Awake is called before Start
    /// 
    /// Leave this method public to support automated grading
    /// </summary>
    public void Awake()
    {
        // retrieve board and player references
        board = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
        player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>();
        player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>();

        // register as invoker and listener
        EventManager.AddTakeTurnInvoker(this);
        EventManager.AddGameOverInvoker(this);
        EventManager.AddGameStartingInvoker(this);
        EventManager.AddTurnOverListener(HandleTurnOverEvent);

        // set up timer for delay between games
        newGameDelayTimer = gameObject.AddComponent<Timer>();
        newGameDelayTimer.Duration = 0.01f;
        newGameDelayTimer.AddTimerFinishedListener(HandleTimerFinishedEvent);

        // initialize statistics class
        Statistics.Initialize();
    }

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
	{
        gamesPlayed++;
        StartGame(firstMovePlayer, Difficulty.Easy, Difficulty.Easy);
    }

    /// <summary>
    /// Adds the given listener for the TakeTurn event
    /// </summary>
    /// <param name="listener">listener</param>
    public void AddTakeTurnListener(UnityAction<PlayerName, Configuration> listener)
    {
        takeTurnEvent.AddListener(listener);
    }

    /// <summary>
    /// Adds the given listener for the GameOver event
    /// </summary>
    /// <param name="listener">listener</param>
    public void AddGameOverListener(UnityAction<PlayerName, Difficulty, Difficulty> listener)
    {
        gameOverEvent.AddListener(listener);
    }

    /// <summary>
    /// Adds the given listener for the GameStarting event
    /// </summary>
    /// <param name="listener">listener</param>
    public void AddGameStartingListener(UnityAction listener)
    {
        gameStartingEvent.AddListener(listener);
    }

    /// <summary>
    /// Starts a game with the given player taking the
    /// first turn
    /// 
    /// Leave this method public to support automated grading
    /// </summary>
    /// <param name="firstPlayer">player taking first turn</param>
    /// <param name="player1Difficulty">difficulty for player 1</param>
    /// <param name="player2Difficulty">difficulty for player 2</param>
    public void StartGame(PlayerName firstPlayer, Difficulty player1Difficulty,
        Difficulty player2Difficulty)
    {
        gameStartingEvent.Invoke();

        // set player difficulties
        player1.Difficulty = player1Difficulty;
        player2.Difficulty = player2Difficulty;

        // create new board
        board.CreateNewBoard();
        takeTurnEvent.Invoke(firstPlayer,
            board.Configuration);
    }

    /// <summary>
    /// Handles the TurnOver event by having the 
    /// other player take their turn
    /// </summary>
    /// <param name="player">who finished their turn</param>
    /// <param name="newConfiguration">the new board configuration</param>
    void HandleTurnOverEvent(PlayerName player, 
        Configuration newConfiguration)
    {
        board.Configuration = newConfiguration;

        // check for game over
        if (newConfiguration.Empty)
        {
            // fire event with winner
            if (player == PlayerName.Player1)
            {
                gameOverEvent.Invoke(PlayerName.Player2, player1.Difficulty, player2.Difficulty);
            }
            else
            {
                gameOverEvent.Invoke(PlayerName.Player1, player1.Difficulty, player2.Difficulty);
            }
            newGameDelayTimer.Run();
        }
        else
        {
            // game not over, so give other player a turn
            if (player == PlayerName.Player1)
            {
                takeTurnEvent.Invoke(PlayerName.Player2,
                    newConfiguration);
            }
            else
            {
                takeTurnEvent.Invoke(PlayerName.Player1,
                    newConfiguration);
            }
        }
    }

    /// <summary>
    /// Starts a new game when the new game delay timer finishes
    /// </summary>
    void HandleTimerFinishedEvent()
    {
        // constant provided for autograder support
        if (!GameConstants.PlaySingleGame)
        {
            if (gamesPlayed < TotalGames)
            {
                if (gamesPlayed % 100 == 0)
                {
                    SetPlayerDifficulties(gamesPlayed);
                }

                gamesPlayed++;

                // alternate player making first move in game
                if (firstMovePlayer == PlayerName.Player1)
                {
                    firstMovePlayer = PlayerName.Player2;
                }
                else
                {
                    firstMovePlayer = PlayerName.Player1;
                }

                StartGame(firstMovePlayer, player1.Difficulty, player2.Difficulty);
            }
            else
            {
                // move to statistics scene when all games have been played
                SceneManager.LoadScene("statistics");
            }
        }
    }

    /// <summary>
    /// Sets the player difficulties for the various difficulty pairings
    /// </summary>
    /// <param name="gamesPlayed">games played so far</param>
    void SetPlayerDifficulties(int gamesPlayed)
    {
        switch (gamesPlayed)
        {
            case 100:
                player1.Difficulty = Difficulty.Medium;
                player2.Difficulty = Difficulty.Medium;
                break;
            case 200:
                player1.Difficulty = Difficulty.Hard;
                player2.Difficulty = Difficulty.Hard;
                break;
            case 300:
                player1.Difficulty = Difficulty.Easy;
                player2.Difficulty = Difficulty.Medium;
                break;
            case 400:
                player1.Difficulty = Difficulty.Easy;
                player2.Difficulty = Difficulty.Hard;
                break;
            case 500:
                player1.Difficulty = Difficulty.Medium;
                player2.Difficulty = Difficulty.Hard;
                break;
        }
    }
}
