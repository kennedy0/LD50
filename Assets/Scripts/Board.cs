using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [Header("Board Setup")]
    public int StartingSize = 2;
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
    ///  Create a new Tile GameObject.
    /// </summary>
    public GameObject InstantiateTile()
    {
        return Instantiate(TilePrefab, transform);
    }
}
