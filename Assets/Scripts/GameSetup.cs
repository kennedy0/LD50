using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    private static GameSetup _instance;

    private void Awake()
    {
        _instance = this;
    }

    /// <summary>
    /// Set up the game before the first round starts.
    /// </summary>
    public static IEnumerator SetupGame()
    {
        yield return SetupBoard();

        HexCell playerStart = new HexCell(Board.Instance.PlayerStartPosition);
        yield return TokenManager.CreatePlayer(playerStart);
        
        HexCell campfireStart = new HexCell(Board.Instance.CampfireStartPosition);
        yield return TokenManager.CreateCampfire(campfireStart);
    }

    /// <summary>
    /// Sets up the board the first time it's created.
    /// </summary>
    private static IEnumerator SetupBoard()
    {
        // Grow selection in rings for a nice effect
        for (var i = 0; i < Board.Instance.StartingSize + 1; i++)
        {
            yield return Board.Grid.MakeCells(0, 0, 0, i);
        }
    }
}
