using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid
{
    private Dictionary<Vector3Int, HexCell> _cells;

    public HexGrid()
    {
        _cells = new Dictionary<Vector3Int, HexCell>();
    }

    /// <summary>
    /// Get a cell from the grid.
    /// </summary>
    public HexCell Cell(int q, int r, int s)
    {
        if (!HasCell(q, r, s))
        {
            AddCell(q, r, s);
        }

        return _cells[new Vector3Int(q, r, s)];
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
}
