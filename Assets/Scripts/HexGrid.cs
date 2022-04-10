using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Datatypes;
using UnityEngine;

public class HexGrid
{
    private Board _board;
    private Dictionary<Hex, HexCell> _cells;

    public HexGrid(Board board)
    {
        _board = board;
        _cells = new Dictionary<Hex, HexCell>();
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

        return _cells[new Hex(q, r, s)];
    }

    public HexCell GetCell(Hex h)
    {
        return GetCell(h.Q, h.R, h.S);
    }

    /// <summary>
    /// Get all cells on the grid.
    /// </summary>
    public List<HexCell> AllCells()
    {
        return _cells.Values.ToList();
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
        for (var i = q-range; i < q+range+1; i++)
        {
            for (var j = r-range; j < r+range+1; j++)
            {
                for (var k = s-range; k < s+range+1; k++)
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

    public List<HexCell> GetCells(Hex h, int range)
    {
        return GetCells(h.Q, h.R, h.S, range);
    }

    /// <summary>
    /// Checks if a cell exists in the grid.
    /// </summary>
    private bool HasCell(int q, int r, int s)
    {
        return _cells.ContainsKey(new Hex(q, r, s));
    }

    private bool HasCell(Hex h)
    {
        return HasCell(h.Q, h.R, h.S);
    }

    /// <summary>
    /// Adds a cell to the grid.
    /// </summary>
    private void AddCell(int q, int r, int s)
    {
        var cell = new HexCell(q, r, s);
        _cells.Add(new Hex(q, r, s), cell);
    }

    private void AddCell(Hex h)
    {
        AddCell(h.Q, h.R, h.S);
    }
}
