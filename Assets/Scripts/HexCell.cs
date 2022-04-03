using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell
{
    private HexGrid _grid;
    private int _q;
    private int _r;
    private int _s;

    public HexCell(HexGrid grid, int q, int r, int s)
    {
        _grid = grid;
        _q = q;
        _r = r;
        _s = s;
    }

    public HexGrid Grid => _grid;
    
    public int Q => _q;
    
    public int R => _r;
    
    public int S => _s;

    public HexCell North => Grid.Cell(Q, R-1, S+1);
    
    public HexCell South => Grid.Cell(Q, R+1, S-1);
    
    public HexCell NorthWest => Grid.Cell(Q-1, R, S+1);
    
    public HexCell NorthEast => Grid.Cell(Q+1, R-1, S);
    
    public HexCell SouthWest => Grid.Cell(Q-1, R+1, S);
    
    public HexCell SouthEast => Grid.Cell(Q+1, R, S-1);

    public List<HexCell> Neighbors => new List<HexCell>{North, South, NorthEast, NorthWest, SouthEast, SouthWest};
}
