using System.Collections;
using System.Collections.Generic;
using Datatypes;
using UnityEngine;
using UnityEngine.UI;

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
        yield return FadeIn();
        yield return CreateCampfire();
        yield return CreatePlayer();
        yield return SetupBoard();
        yield return SetupCamera();
    }

    /// <summary>
    /// ToDo - Remove this after LD
    /// </summary>
    /// <returns></returns>
    private static IEnumerator FadeIn()
    {
        // Show message
        var tb = TextBox.Find();
        yield return tb.ShowTextBox("Keep the fire lit.");
        yield return tb.ShowTextBox("Use the keyboard to move.\nQWE\nASD");
    }

    /// <summary>
    /// ToDo - Remove this after LD
    /// </summary>
    private static IEnumerator CreateCampfire()
    {
        yield return new WaitForSeconds(.5f);
        HexCell campfireStart = Board.Grid.GetCell(Board.Instance.CampfireStartPosition);
        yield return Board.RevealTiles(campfireStart.Position, 0);
        TokenManager.CreateCampfire(campfireStart);
        yield return new WaitForSeconds(1f);
    }

    /// <summary>
    /// ToDo - Remove this after LD
    /// </summary>
    private static IEnumerator CreatePlayer()
    {
        yield return new WaitForSeconds(.5f);
        HexCell playerStart = Board.Grid.GetCell(Board.Instance.PlayerStartPosition);
        yield return Board.RevealTiles(playerStart.Position, 0);
        TokenManager.CreatePlayer(playerStart);
        yield return new WaitForSeconds(1f);
    }

    /// <summary>
    /// Set up the board the first time it's created.
    /// </summary>
    private static IEnumerator SetupBoard()
    {
        yield return new WaitForSeconds(1f);
        // ToDo: Generate the starting region
        yield return Board.GenerateRegion();
        
        // ToDo: Generate surrounding regions

        // Reveal tiles in rings for a nice effect
        for (var i = 0; i < Board.Instance.StartingSize + 1; i++)
        {
            yield return Board.RevealTiles(Hex.Zero, i);
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
