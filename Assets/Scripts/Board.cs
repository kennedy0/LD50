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
    /// Get all tiles on the board.
    /// </summary>
    public static List<Tile> AllTiles()
    {
        var tiles = new List<Tile>();
        foreach (var cell in Grid.AllCells())
        {
            if (cell.Tile != null)
            {
                tiles.Add(cell.Tile);
            }
        }
        
        return tiles;
    }

    public static IEnumerator MakeTiles(int q, int r, int s, int distance = 0)
    {
        foreach (var cell in Grid.GetCells(q, r, s, distance))
        {
            // Skip if tile has already been created
            if (cell.Tile != null)
            {
                continue;
            }

            // Create tile
            var tileObject = Instantiate(Instance.TilePrefab, Instance.transform);
            var tile = tileObject.GetComponent<Tile>();
            
            // Link cell and tile
            cell.LinkToTile(tile);
            tile.LinkToCell(cell);

            
            // Tile reveal delay
            yield return new WaitForSeconds(Instance.TileRevealDelay);
        }
    }

    /// <summary>
    /// Update visibility on all tiles.
    /// </summary>
    public static void UpdateVisibility()
    {
        foreach (var tile in AllTiles())
        {
            tile.UpdateVisibility();
        }
    }
}
