using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Datatypes;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board Instance;

    [Header("Board Setup")]
    public int StartingSize = 2;
    public Hex PlayerStartPosition = new Hex(0, -1, 1);
    public Hex CampfireStartPosition = new Hex(0, 0, 0);
    
    [Header("Tiles")]
    public GameObject TilePrefab;
    public float TileRevealDelay = .05f;
    
    private HexGrid _grid;

    public static HexGrid Grid => Instance._grid;

    private void Awake()
    {
        Instance = this;
        _grid = new HexGrid(this);
    }

    /// <summary>
    /// Instantiate a tile object.
    /// </summary>
    public static GameObject InstantiateTile()
    {
        return Instantiate(Instance.TilePrefab, Instance.transform);
    }

    /// <summary>
    /// Generate all cells in a region.
    /// </summary>
    public static IEnumerator GenerateRegion()
    {
        Debug.LogError("GenerateRegion is not implemented!");
        yield return null;
    }

    /// <summary>
    /// Reveal tiles around a location.
    /// </summary>
    public static IEnumerator RevealTiles(Hex position, int distance = 0)
    {
        foreach (var cell in Grid.GetCells(position.Q, position.R, position.S, distance))
        {
            // Skip if tile has already been revealed
            if (cell.Tile.IsRevealed)
            {
                continue;
            }
            
            cell.Tile.Reveal();
            yield return new WaitForSeconds(Instance.TileRevealDelay);
        }
    }
}
