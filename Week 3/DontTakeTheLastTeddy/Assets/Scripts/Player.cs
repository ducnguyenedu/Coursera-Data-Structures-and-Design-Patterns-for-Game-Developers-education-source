using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A player
/// </summary>
public class Player : MonoBehaviour, ITurnOverInvoker
{
    PlayerName myName;
    Timer thinkingTimer;

    // minimax search support
    Difficulty difficulty;
    int searchDepth = 0;
    MinimaxTree<Configuration> tree;

    // events invoked by class
    TurnOver turnOverEvent = new TurnOver();

    // saved for efficiency
    LinkedList<MinimaxTreeNode<Configuration>> nodeList =
        new LinkedList<MinimaxTreeNode<Configuration>>();
    List<int> binContents = new List<int>();
    List<Configuration> newConfigurations =
        new List<Configuration>();

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
    /// <param name="name">player name</param>
    /// <param name="gameObject">the game object the script is attached to</param>
    //public Player(PlayerName name, GameObject gameObject) :
    //    base(gameObject)
    //{
    //    // set name
    //    myName = name;

    //    // initialization code from Awake

    //    // add timer component
    //    thinkingTimer = gameObject.AddComponent<ThinkingTimer>();
    //    thinkingTimer.Duration = GameConstants.AiThinkSeconds;
    //    thinkingTimer.AddTimerFinishedListener(HandleThinkingTimerFinished);

    //    // register as invoker and listener
    //    EventManager.AddTurnOverInvoker(this);
    //    EventManager.AddTakeTurnListener(HandleTakeTurnEvent);
    //}

    #endregion

    /// <summary>
    /// Awake is called before Start
    /// </summary>
    void Awake()
	{
        // set name
		if (CompareTag("Player1"))
        {
            myName = PlayerName.Player1;
        }
        else
        {
            myName = PlayerName.Player2;
        }

        // add timer component
        thinkingTimer = gameObject.AddComponent<Timer>();
        thinkingTimer.Duration = GameConstants.AiThinkSeconds;
        thinkingTimer.AddTimerFinishedListener(HandleThinkingTimerFinished);

        // register as invoker and listener
        EventManager.AddTurnOverInvoker(this);
        EventManager.AddTakeTurnListener(HandleTakeTurnEvent);
	}

    /// <summary>
    /// Gets and sets the difficulty for the player
    /// </summary>
    public Difficulty Difficulty
    {
        get { return difficulty; }
        set
        {
            difficulty = value;
            switch (difficulty)
            {
                case Difficulty.Easy:
                    searchDepth = GameConstants.EasyMinimaxDepth;
                    break;
                case Difficulty.Medium:
                    searchDepth = GameConstants.MediumMinimaxDepth;
                    break;
                case Difficulty.Hard:
                    searchDepth = GameConstants.HardMinimaxDepth;
                    break;
                default:
                    searchDepth = GameConstants.EasyMinimaxDepth;
                    break;
            }
        }
    }

    /// <summary>
    /// Adds the given listener for the TurnOver event
    /// </summary>
    /// <param name="listener">listener</param>
    public void AddTurnOverListener(
        UnityAction<PlayerName, Configuration> listener)
    {
        turnOverEvent.AddListener(listener);
    }

    /// <summary>
    /// Handles the TakeTurn event
    /// </summary>
    /// <param name="player">whose turn it is</param>
    /// <param name="boardConfiguration">current board configuration</param>
    void HandleTakeTurnEvent(PlayerName player,
        Configuration boardConfiguration)
    {
        // only take turn if it's our turn
        if (player == myName)
        {
            tree = BuildTree(boardConfiguration);
            thinkingTimer.Run();
        }
    }

    /// <summary>
    /// Builds the tree
    /// </summary>
    /// <param name="boardConfiguration">current board configuration</param>
    /// <returns>tree</returns>
    MinimaxTree<Configuration> BuildTree(
        Configuration boardConfiguration)
    {
        // build tree to appropriate depth
        MinimaxTree<Configuration> tree =
            new MinimaxTree<Configuration>(boardConfiguration);
        nodeList.Clear();
        nodeList.AddLast(tree.Root);
        while (nodeList.Count > 0)
        {
            MinimaxTreeNode<Configuration> currentNode =
                nodeList.First.Value;
            nodeList.RemoveFirst();
            List<Configuration> children =
                GetNextConfigurations(currentNode.Value);
            foreach (Configuration child in children)
            {
                // STUDENTS: only add to tree if within search depth
                if (GetNodeDepth(currentNode) < searchDepth)
                {
                    MinimaxTreeNode<Configuration> childNode =
                        new MinimaxTreeNode<Configuration>(
                            child, currentNode);
                    tree.AddNode(childNode);
                    nodeList.AddLast(childNode);
                }
            }
        }
        return tree;
    }

    /// <summary>
    /// Gets the depth of the given node
    /// </summary>
    /// <param name="node">node to check</param>
    /// <returns>node depth</returns>
    int GetNodeDepth(MinimaxTreeNode<Configuration> node)
    {
        int depth = 0;
        MinimaxTreeNode<Configuration> currentNode = node;
        while (currentNode.Parent != null)
        {
            depth++;
            currentNode = currentNode.Parent;
        }
        return depth;
    }

    /// <summary>
    /// Handles the thinking timer finishing
    /// 
    /// Leave public to support automated grading
    /// </summary>
    public void HandleThinkingTimerFinished()
    {
        // do the search and pick the move
        Minimax(tree.Root, true);

        // find child node with maximum score
        IList<MinimaxTreeNode<Configuration>> children =
            tree.Root.Children;
        MinimaxTreeNode<Configuration> maxChildNode = children[0];
        for (int i = 1; i < children.Count; i++)
        {
            if (children[i].MinimaxScore > maxChildNode.MinimaxScore)
            {
                maxChildNode = children[i];
            }
        }

        // provide new configuration as second argument
        turnOverEvent.Invoke(myName, maxChildNode.Value);
    }

    /// <summary>
    /// Gets a list of the possible next configurations
    /// given the current configuration
    /// </summary>
    /// <param name="currentConfiguration">current configuration</param>
    /// <returns>list of next configurations</returns>
    List<Configuration> GetNextConfigurations(
        Configuration currentConfiguration)
    {
        newConfigurations.Clear();
        IList<int> currentBins = currentConfiguration.Bins;
        for (int i = 0; i < currentBins.Count; i++)
        {
            int currentBinCount = currentBins[i];
            while (currentBinCount > 0)
            {
                // take one teddy from current bin
                currentBinCount--;

                // add new next configuration to list
                binContents.Clear();
                binContents.AddRange(currentBins);
                binContents[i] = currentBinCount;
                newConfigurations.Add(
                    new Configuration(binContents));
            }
        }
        return newConfigurations;
    }

    /// <summary>
    /// Assigns minimax scores to the tree nodes
    /// </summary>
    /// <param name="tree">tree to mark with scores</param>
    /// <param name="maximizing">whether or not we're maximizing</param>
    void Minimax(MinimaxTreeNode<Configuration> tree,
        bool maximizing)
    {
        // recurse on children
        IList<MinimaxTreeNode<Configuration>> children = tree.Children;
        if (children.Count > 0)
        {
            foreach (MinimaxTreeNode<Configuration> child in children)
            {
                // toggle maximizing as we move down
                Minimax(child, !maximizing);
            }

            // set default node minimax score
            if (maximizing)
            {
                tree.MinimaxScore = int.MinValue;
            }
            else
            {
                tree.MinimaxScore = int.MaxValue;
            }

            // find maximum or minimum value in children
            foreach (MinimaxTreeNode<Configuration> child in children)
            {
                if (maximizing)
                {
                    // check for higher minimax score
                    if (child.MinimaxScore > tree.MinimaxScore)
                    {
                        tree.MinimaxScore = child.MinimaxScore;
                    }
                }
                else
                {
                    // minimizing, check for lower minimax score
                    if (child.MinimaxScore < tree.MinimaxScore)
                    {
                        tree.MinimaxScore = child.MinimaxScore;
                    }
                }
            }
        }
        else
        {
            // leaf nodes are the base case
            AssignHeuristicMinimaxScore(tree, maximizing);
        }
    }

    /// <summary>
    /// Assigns the end of game minimax score
    /// </summary>
    /// <param name="node">node to mark with score</param> 
    /// <param name="maximizing">whether or not we're maximizing</param>
    void AssignEndOfGameMinimaxScore(MinimaxTreeNode<Configuration> node,
        bool maximizing)
    {
        if (maximizing)
        {
            // other player took the last teddy
            node.MinimaxScore = 1;
        }
        else
        {
            // we took the last teddy
            node.MinimaxScore = 0;
        }
    }
        
    /// <summary>
    /// Assigns a heuristic minimax score to the given node
    /// </summary>
    /// <param name="node">node to mark with score</param>
    /// <param name="maximizing">whether or not we're maximizing</param>
    void AssignHeuristicMinimaxScore(
        MinimaxTreeNode<Configuration> node,
        bool maximizing)
    {
        // might have reached an end-of-game configuration
        if (node.Value.Empty)
        {
            AssignEndOfGameMinimaxScore(node, maximizing);
        }
        else
        {
            // use a heuristic evaluation function to score the node

            // if there are two bins with teddy bears, and one of the bins 
            // has one teddy bear, we can win by leaving the bin with one 
            // teddy bear if we're currently maximizing but we'll lose
            // if we're currently minimizing
            List<int> nonEmptyBins = node.Value.NonEmptyBins;
            if (nonEmptyBins.Count == 2 &&
                (nonEmptyBins[0] == 1 || nonEmptyBins[1] == 1))
            {
                if (maximizing)
                {
                    node.MinimaxScore = 1;
                }
                else
                {
                    node.MinimaxScore = 0;
                }
            }
            else if (nonEmptyBins.Count % 2 == 1)
            {
                // an odd number of bins could be good if
                // each bin is emptied in a single turn if we're
                // currently maximizing but could be bad if 
                // we're currently minimizing
                if (maximizing)
                {
                    node.MinimaxScore = 0.75f;
                }
                else
                {
                    node.MinimaxScore = 0.25f;
                }
            }
            else if (node.Value.NumBears % 2 == 0)
            {
                // an even number of bears could be good if each player
                // takes one bear per turn and we're currently maximizing
                // but could be bad if we're currently minimizing
                if (maximizing)
                {
                    node.MinimaxScore = 0.75f;
                }
                else
                {
                    node.MinimaxScore = 0.25f;
                }
            }
            else
            {
                // no special cases found
                node.MinimaxScore = 0.5f;
            }
        }
    }
}
