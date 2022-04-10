using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private int _round;
    private ActorList _actors;
    private bool _exitGameLoop = false;

    private void Awake()
    {
        _instance = this;

        _round = 0;
        _actors = new ActorList();
    }

    void Start()
    {
        StartCoroutine(GameLoop());
    }

    /// <summary>
    /// This is the main game loop.
    /// </summary>
    private IEnumerator GameLoop()
    {
        yield return GameSetup.SetupGame();

        while (true)
        {
            yield return BeforeRound();
            yield return Round();
            yield return AfterRound();

            if (_exitGameLoop)
            {
                break;
            }
        }
    }

    /// <summary>
    /// Runs immediately before a round starts.
    /// </summary>
    private IEnumerator BeforeRound()
    {
        Debug.Log($"Round {_round} start.");
        yield return null;
    }
    
    /// <summary>
    /// During a round, each actor takes a turn.
    /// </summary>
    private IEnumerator Round()
    {
        foreach (var actor in _actors)
        {
            yield return BeforeEachTurn();
            yield return actor.BeforeTurn();
            yield return actor.Turn();
            yield return actor.AfterTurn();
            yield return AfterEachTurn();
        }
    }

    /// <summary>
    /// Runs before each turn, for each actor.
    /// </summary>
    private IEnumerator BeforeEachTurn()
    {
        yield return null;
    }

    /// <summary>
    /// Runs after each turn, for each actor.
    /// </summary>
    private IEnumerator AfterEachTurn()
    {
        Player.MakeSurroundingTiles();
        yield return null;
    }

    /// <summary>
    /// Runs immediately after a round ends.
    /// </summary>
    private IEnumerator AfterRound()
    {
        Debug.Log($"Round {_round} end.");
        _actors.UpdateList();
        _round += 1;
        yield return null;
    }

    /// <summary>
    /// Add an actor to the list of actors.
    /// </summary>
    public static void AddActor(Actor actor)
    {
        _instance._actors.Add(actor);
    }
}
