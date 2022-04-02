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

    private Dictionary<Vector2Int, HexTile> _tiles;

    private void Awake()
    {
        _tiles = new Dictionary<Vector2Int, HexTile>();
    }

    private void Start()
    {
        InitHexGrid();
    }

    /// <summary>
    /// Create the hex grid.
    /// </summary>
    private void InitHexGrid()
    {
        // Starting point
        Generate(0, 0);
        
        // Surrounding ring
        foreach (var coordinate in Utilities.NeighborCoordinates(0, 0))
        {
            Generate(coordinate.x, coordinate.y);
        }
    }

    /// <summary>
    /// Generate tiles from a given point. This is the main entry point for generating tiles.
    /// </summary>
    public void Generate(int gx, int gy)
    {
        if (!TileExists(gx, gy))
        {
            AddTile(gx, gy);
        }

        foreach (var coordinate in Utilities.NeighborCoordinates(gx, gy))
        {
            var cx = coordinate.x;
            var cy = coordinate.y;
            if (!TileExists(cx, cy))
            {
                AddTile(cx, cy);
            }
        }
    }

    /// <summary>
    /// Add a tile to the grid.
    /// </summary>
    private void AddTile(int gx, int gy)
    {
        // Tile gameobject
        var tile = Instantiate(Tile, transform);
        tile.name = $"Tile ({gx}, {gy})";
        tile.transform.position = Utilities.GridToWorldPosition(gx, gy);
        
        // Add the tile component
        var hexTile = tile.AddComponent<HexTile>();
        hexTile.X = gx;
        hexTile.Y = gy;
        
        // Add 
        _tiles.Add(new Vector2Int(gx, gy), hexTile);
    }

    /// <summary>
    /// Get a tile from a position.
    /// </summary>
    public HexTile GetTile(int gx, int gy)
    {
        return _tiles[new Vector2Int(gx, gy)];
    }

    /// <summary>
    /// Check if a tile exists at a coordinate.
    /// </summary>
    public bool TileExists(int gx, int gy)
    {
        return _tiles.ContainsKey(new Vector2Int(gx, gy));
    }
}
