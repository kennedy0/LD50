using System;
using System.Collections;
using Datatypes;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int TileRevealRange = 3;
    
    private static Player _instance;
    private Actor _actor;

    public static Actor Actor => _instance._actor;

    private void Awake()
    {
        _instance = this;
        _actor = GetComponent<Actor>();
    }

    public static Transform Transform => _instance.transform;

    /// <summary>
    /// Called when the player actor's cell changes
    /// </summary>
    public void OnCellChange(HexCell oldCell, HexCell newCell)
    {
        GenerateNeighborRegions(oldCell, newCell);
        RevealSurroundingTiles(oldCell, newCell);
    }

    /// <summary>
    /// Generate large regions of the world adjacent to the player.
    /// </summary>
    private void GenerateNeighborRegions(HexCell oldCell, HexCell newCell)
    {
        // Exit if old or new cell are null.
        if (oldCell == null || newCell == null)
        {
            return;
        }
        Debug.Log($"Region: {newCell.Position.GetRegion()}");

        // ToDo: Exit if the region did not change.

        // ToDo: Generate region for each neighbor.
    }

    /// <summary>
    /// Reveal tiles near the player.
    /// </summary>
    private void RevealSurroundingTiles(HexCell oldCell, HexCell newCell)
    {
        var range = TileRevealRange;
        StartCoroutine(Board.RevealTiles(newCell.Position, range));
    }
}
