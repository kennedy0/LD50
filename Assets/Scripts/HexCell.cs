using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell
{
    private HexGrid _grid;
    private int _q;
    private int _r;
    private int _s;
    private Tile _tile;
    
    public HexCell(HexGrid grid, int q, int r, int s)
    {
        _grid = grid;
        _q = q;
        _r = r;
        _s = s;
        CreateTile();
    }
    
    /// <summary>
    /// Create a tile linked to this cell.
    /// </summary>
    private void CreateTile()
    {
        var tileGameObject = Grid.Board.InstantiateTile();
        _tile = tileGameObject.AddComponent<Tile>();
        _tile.InitTile(this);
    }

    public override string ToString()
    {
        return $"HexCell({Q}, {R}, {S})";
    }

    public HexGrid Grid => _grid;
    
    public int Q => _q;
    
    public int R => _r;
    
    public int S => _s;

    public HexCell North => Grid.GetCell(Q, R-1, S+1);
    
    public HexCell South => Grid.GetCell(Q, R+1, S-1);
    
    public HexCell NorthWest => Grid.GetCell(Q-1, R, S+1);
    
    public HexCell NorthEast => Grid.GetCell(Q+1, R-1, S);
    
    public HexCell SouthWest => Grid.GetCell(Q-1, R+1, S);
    
    public HexCell SouthEast => Grid.GetCell(Q+1, R, S-1);

    public List<HexCell> Neighbors => new List<HexCell>{North, South, NorthEast, NorthWest, SouthEast, SouthWest};

    /// <summary>
    /// Get the distance to another cell.
    /// </summary>
    public int DistanceTo(HexCell cell)
    {
        return Utilities.CalculateDistance(Q, R, S, cell.Q, cell.R, cell.S);
    }
}
