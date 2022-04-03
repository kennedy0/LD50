using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;


public class HexGrid : MonoBehaviour
{
    public GameObject Tile;
    public float TileCreationDelay = .1f;

    private Dictionary<Vector2Int, HexTile> _tiles;
    private float _tileCreationTimer = 0f;

    private void Awake()
    {
        _tiles = new Dictionary<Vector2Int, HexTile>();
    }

    private void Start()
    {
        InitHexGrid();
    }

    private void Update()
    {
        UpdateTileCreationTimer();
    }

    /// <summary>
    /// Global timer to delay tile creation. 
    /// </summary>
    private void UpdateTileCreationTimer()
    {
        _tileCreationTimer -= Time.deltaTime;
        if (_tileCreationTimer < 0f)
        {
            _tileCreationTimer = 0f;
        }
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
            Generate(coordinate.x, coordinate.y, 2);
        }
    }

    /// <summary>
    /// Generate tiles from a given point. This is the main entry point for generating tiles.
    /// Depth determines how many neighbors are generated.
    ///     0 = no neighbors
    ///     1 = neighbors 1 space away
    ///     2 = neighbors 2 spaces away
    ///     etc.
    /// </summary>
    public void Generate(int gx, int gy, int distance = 0)
    {
        // Depth must be at least 0.
        if (distance < 0)
        {
            distance = 0;
        }

        // Keep track of tiles we've added
        HashSet<Vector2Int> addedTiles = new HashSet<Vector2Int>();
        
        // Always add the requested tiles
        AddTile(gx, gy);
        addedTiles.Add(new Vector2Int(gx, gy));

        while (distance > 0)
        {
            distance -= 1;
            HashSet<Vector2Int> newlyAddedTiles = new HashSet<Vector2Int>();
            foreach (var tileCoord in addedTiles)
            {
                int tx = tileCoord.x;
                int ty = tileCoord.y;
                foreach (var neighborCoord in Utilities.NeighborCoordinates(tx, ty))
                {
                    int nx = neighborCoord.x;
                    int ny = neighborCoord.y;
                    AddTile(nx, ny);
                    newlyAddedTiles.Add(new Vector2Int(nx, ny));
                }
            }
            addedTiles.UnionWith(newlyAddedTiles);
        }
    }

    /// <summary>
    /// Add a tile to the grid.
    /// </summary>
    private void AddTile(int gx, int gy)
    {
        // Don't generate this tile if it already exists.
        if (TileExists(gx, gy))
        {
            return;
        }

        _tileCreationTimer += TileCreationDelay;

        // Tile gameobject
        var tile = Instantiate(Tile, transform);
        tile.name = $"Tile ({gx}, {gy})";
        tile.transform.position = Utilities.GridToWorldPosition(gx, gy);
        
        // Add the tile component
        var hexTile = tile.GetComponent<HexTile>();
        hexTile.X = gx;
        hexTile.Y = gy;

        // Add to tile collection
        _tiles.Add(new Vector2Int(gx, gy), hexTile);
        
        // Trigger the creation of the tile
        hexTile.InitTile(_tileCreationTimer);
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
