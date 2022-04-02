using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;


public class HexGrid : MonoBehaviour
{
    public GameObject Tile;
    public int Width;
    public int Height;

    private Dictionary<Vector2Int, GameObject> _tiles;

    private void Awake()
    {
        _tiles = new Dictionary<Vector2Int, GameObject>();
    }

    private void Start()
    {
        InitHexGrid();
    }

    /// <summary>
    /// Create the hex grid.
    /// </summary>
    void InitHexGrid()
    {
        int x;
        int y;
        
        for (x = 0; x < Width; x++)
        {
            for (y = 0; y < Height; y++)
            {
                AddTile(x, y);
            }
        }
    }

    /// <summary>
    /// Add a tile to the grid.
    /// </summary>
    public void AddTile(int gx, int gy)
    {
        // Tile gameobject
        var tile = Instantiate(Tile, transform);
        tile.name = $"Tile ({gx}, {gy})";
        tile.transform.position = Utilities.GridToWorldPosition(gx, gy);
        
        // Add the cell component
        var hexTile = tile.AddComponent<HexTile>();
        hexTile.X = gx;
        hexTile.Y = gy;
        
        // Add 
        _tiles.Add(new Vector2Int(gx, gy), tile);
    }

    /// <summary>
    /// Get a tile from a position.
    /// </summary>
    public GameObject GetTile(int gx, int gy)
    {
        return _tiles[new Vector2Int(gx, gy)];
    }
}
