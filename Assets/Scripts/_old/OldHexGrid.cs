using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using UnityEngine;


public class OldHexGrid : MonoBehaviour
{
    public GameObject Tile;
    public float TileCreationDelay = .1f;

    private Dictionary<Vector2Int, OldHexTile> _tiles;
    private float _tileCreationTimer = 0f;

    private int _playerX = 0;
    private int _playerY = 1;

    public int PlayerX => _playerX;
    public int PlayerY => _playerY;

    private void Awake()
    {
        _tiles = new Dictionary<Vector2Int, OldHexTile>();
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
        var distance = 3;
        Generate(0, 0, distance);
        
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
        var hexTile = tile.GetComponent<OldHexTile>();
        hexTile.SetGrid(this);
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
    public OldHexTile GetTile(int gx, int gy)
    {
        if (TileExists(gx, gy))
        {
            return _tiles[new Vector2Int(gx, gy)];
        }

        return null;
    }

    /// <summary>
    /// Check if a tile exists at a coordinate.
    /// </summary>
    public bool TileExists(int gx, int gy)
    {
        return _tiles.ContainsKey(new Vector2Int(gx, gy));
    }

    /// <summary>
    /// Get a tile and its surrounding neighbors.
    /// Neighbors must exist to be returned.
    /// ToDo: This is super inefficient, it should be reworked.
    /// ToDo: This is very similar to Generate - that logic should be combined.
    /// </summary>
    public HashSet<OldHexTile> GetTiles(int gx, int gy, int distance)
    {
        // Depth must be at least 0.
        if (distance < 0)
        {
            distance = 0;
        }

        // HashSet to store tiles
        HashSet<OldHexTile> tiles = new HashSet<OldHexTile>();
        tiles.Add(GetTile(gx, gy));

        // Keep checking neighbors of tiles until distance is exhausted.
        var d = distance;
        while (d > 0)
        {
            d -= 1;
            HashSet<OldHexTile> newTiles = new HashSet<OldHexTile>();
            foreach (var tile in tiles)
            {
                var neighborCoords = Utilities.NeighborCoordinates(tile.X, tile.Y);
                foreach (var nc in neighborCoords)
                {
                    var t = GetTile(nc.x, nc.y);
                    if (t != null)
                    {
                        newTiles.Add(t);
                    }
                }
            }
            tiles.UnionWith(newTiles);
        }
        
        return tiles;
    }

    /// <summary>
    /// Runs when the player position is changed.
    /// </summary>
    public void HandlePlayerPositionUpdate(int old_gx, int old_gy, int new_gx, int new_gy)
    {
        // Store new player position
        _playerX = new_gx;
        _playerY = new_gy;

        // Generate new tiles
        var distance = 3;
        Generate(new_gx, new_gy, distance);

        // Get list of neighbors from old and new position
        var oldNeighbors = GetTiles(old_gx, old_gy, distance);
        var newNeighbors = GetTiles(new_gx, new_gy, distance);
        var allNeighbors = oldNeighbors.Union(newNeighbors);
        
        // Call update player position on all neighbor tiles
        foreach (var tile in allNeighbors)
        {
            tile.HandlePlayerPositionUpdate(old_gx, old_gy, new_gx, new_gy);
        }
    }
}
