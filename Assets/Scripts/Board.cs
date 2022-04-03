using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [Header("Board Setup")]
    public int StartingSize = 3;
    public GameObject TilePrefab;
    
    private HexGrid _grid;

    private void Awake()
    {
        _grid = new HexGrid(this);
    }

    private void Start()
    {
        SetupBoard();
    }

    /// <summary>
    /// Sets up the board the first time it's created.
    /// </summary>
    private void SetupBoard()
    {
        _grid.MakeCells(0, 0, 0, StartingSize);
    }

    /// <summary>
    ///  Create a new Tile GameObject linked to a cell.
    /// </summary>
    public Tile NewTile(HexCell cell)
    {
        var tileGameObject = Instantiate(TilePrefab);
        var tile = tileGameObject.AddComponent<Tile>();
        return tile;
    }
}
