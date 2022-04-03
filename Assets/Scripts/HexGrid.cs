using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HexGrid
{
    private Board _board;
    private Dictionary<Vector3Int, HexCell> _cells;

    public HexGrid(Board board)
    {
        _board = board;
        _cells = new Dictionary<Vector3Int, HexCell>();
    }

    public Board Board => _board;

    /// <summary>
    /// Get a cell from the grid.
    /// </summary>
    public HexCell GetCell(int q, int r, int s)
    {
        if (!HasCell(q, r, s))
        {
            AddCell(q, r, s);
        }

        return _cells[new Vector3Int(q, r, s)];
    }

    /// <summary>
    /// Get all cells within range of a coordinate.
    /// </summary>
    public List<HexCell> GetCells(int q, int r, int s, int range)
    {
        List<HexCell> cells = new List<HexCell>();
        
        // Make sure the range is not negative.
        if (range < 0)
        {
            throw new Exception($"Range ({range}) cannot be negative.");
        }
        
        // Iterate over range to find cells where the max distance is less than the range.
        // https://www.redblobgames.com/grids/hexagons/#range
        for (var i = -range; i < range+1; i++)
        {
            for (var j = -range; j < range+1; j++)
            {
                for (var k = -range; k < range+1; k++)
                {
                    if (i + j + k == 0)
                    {
                        cells.Add(GetCell(i, j, k));
                    }
                }
            }
        }

        return cells;
    }

    /// <summary>
    /// Checks if a cell exists in the grid.
    /// </summary>
    private bool HasCell(int q, int r, int s)
    {
        return _cells.ContainsKey(new Vector3Int(q, r, s));
    }

    /// <summary>
    /// Adds a cell to the grid.
    /// </summary>
    private void AddCell(int q, int r, int s)
    {
        var cell = new HexCell(this, q, r, s);
        _cells.Add(new Vector3Int(q, r, s), cell);
    }

    /// <summary>
    /// Create cells centered around a coordinate.
    /// 
    /// </summary>
    public void MakeCells(int q, int r, int s, int distance = 0)
    {
        GetCells(q, r, s, distance);
    }
}
