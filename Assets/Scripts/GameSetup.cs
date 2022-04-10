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

        HexCell playerStart = Board.Grid.GetCell(Board.Instance.PlayerStartPosition);
        TokenManager.CreatePlayer(playerStart);
        
        HexCell campfireStart = Board.Grid.GetCell(Board.Instance.CampfireStartPosition);
        TokenManager.CreateCampfire(campfireStart);

        yield return SetupCamera();
    }

    /// <summary>
    /// Set up the board the first time it's created.
    /// </summary>
    private static IEnumerator SetupBoard()
    {
        // Grow selection in rings for a nice effect
        for (var i = 0; i < Board.Instance.StartingSize + 1; i++)
        {
            yield return Board.MakeTiles(0, 0, 0, i);
        }
    }

    /// <summary>
    /// Set up the camera after the tokens have been created.
    /// </summary>
    private static IEnumerator SetupCamera()
    {
        CameraFollow.SetTarget(Player.Transform);
        yield return null;
    }
}
