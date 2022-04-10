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

    private bool _isVisibile;
    
    public HexCell(int q, int r, int s)
    {
        _q = q;
        _r = r;
        _s = s;

        _isVisibile = false;
    }
    
    public HexCell(Hex h)
    {
        _q = h.Q;
        _r = h.R;
        _s = h.S;
        
        _isVisibile = false;
    }

    public override string ToString()
    {
        return $"HexCell({Q}, {R}, {S})";
    }

    public void LinkToTile(Tile tile)
    {
        if (_tile != null)
        {
            Debug.LogError($"{this} is already linked to a tile.");
            return;
        }

        _tile = tile;
    }

    public HexGrid Grid => Board.Grid;

    public Tile Tile => _tile;
    
    public int Q => _q;
    
    public int R => _r;
    
    public int S => _s;

    public Hex Position => new Hex(Q, R, S);

    public Vector3 WorldPosition => Position.WorldPosition();

    public HexCell North => Grid.GetCell(Q, R-1, S+1);
    
    public HexCell South => Grid.GetCell(Q, R+1, S-1);
    
    public HexCell NorthWest => Grid.GetCell(Q-1, R, S+1);
    
    public HexCell NorthEast => Grid.GetCell(Q+1, R-1, S);
    
    public HexCell SouthWest => Grid.GetCell(Q-1, R+1, S);
    
    public HexCell SouthEast => Grid.GetCell(Q+1, R, S-1);

    public List<HexCell> Neighbors => new List<HexCell>{North, South, NorthEast, NorthWest, SouthEast, SouthWest};

    public bool IsVisible => _isVisibile;

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
        _isVisibile = visibility;
        Tile.UpdateVisibility();
    }
}
