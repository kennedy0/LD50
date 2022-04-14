using System.Collections;
using System.Collections.Generic;
using Datatypes;
using UnityEngine;

public class HexCell
{
    private int _q;
    private int _r;
    private int _s;
    private Tile _tile;
    
    private int _height;

    private bool _isVisible;
    private Actor _actor;
    
    public HexCell(int q, int r, int s)
    {
        _q = q;
        _r = r;
        _s = s;

        _isVisible = false;
        _actor = null;
        
        InitRandomFeatures();
        CreateTile();
        BoardGen.GenerateToken(this);
    }

    public override string ToString()
    {
        return $"HexCell({Q}, {R}, {S})";
    }

    /// <summary>
    /// Create the tile for this cell.
    /// </summary>
    private void CreateTile()
    {
        var tileObject = Board.InstantiateTile();
        var tile = tileObject.GetComponent<Tile>();
        _tile = tile;
        tile.InitTile(this);
    }

    /// <summary>
    /// Generate random features based on the cell's location.
    /// </summary>
    private void InitRandomFeatures()
    {
        _height = BoardGen.GenerateHeight(this);
    }

    public HexGrid Grid => Board.Grid;

    public Tile Tile => _tile;

    public int Height => _height;
    
    public int Q => _q;
    
    public int R => _r;
    
    public int S => _s;

    public Hex Position => new Hex(Q, R, S);

    // World position is always 0 on the Y-axis. Height is a separate parameter. 
    public Vector3 WorldPosition => Position.WorldPosition();

    public HexCell North => Grid.GetCell(Q, R-1, S+1);
    
    public HexCell South => Grid.GetCell(Q, R+1, S-1);
    
    public HexCell NorthWest => Grid.GetCell(Q-1, R, S+1);
    
    public HexCell NorthEast => Grid.GetCell(Q+1, R-1, S);
    
    public HexCell SouthWest => Grid.GetCell(Q-1, R+1, S);
    
    public HexCell SouthEast => Grid.GetCell(Q+1, R, S-1);

    public List<HexCell> Neighbors => new List<HexCell>{North, South, NorthEast, NorthWest, SouthEast, SouthWest};

    public bool IsVisible => _isVisible;

    public Actor Actor => _actor;

    /// <summary>
    /// Get the distance to another cell.
    /// </summary>
    public int DistanceTo(HexCell cell)
    {
        return Hex.Distance(Position, cell.Position);
    }

    /// <summary>
    /// Set the visibility of this cell.
    /// </summary>
    public void SetVisibility(bool visibility)
    {
        _isVisible = visibility;
        Tile.UpdateVisibility();

        if (Actor != null)
        {
            Actor.SetVisibility(visibility);
        }
    }

    /// <summary>
    /// Set the actor that is on this cell.
    /// </summary>
    public void SetActor(Actor actor)
    {
        // Make sure that the actor has been set to this cell.
        if (actor.Cell != this)
        {
            Debug.Log($"Trying to set actor {actor} cell to {this}, but it is currently on cell {actor.Cell}");
        }

        _actor = actor;
    }

    /// <summary>
    /// Unset the actor that is on this cell.
    /// </summary>
    public void UnsetActor()
    {
        _actor = null;
    }
}
