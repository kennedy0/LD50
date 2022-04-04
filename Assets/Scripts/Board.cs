using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [Header("Board Setup")]
    public int StartingSize = 2;
    public Vector3Int PlayerStartPosition = new Vector3Int(0, -1, 1);
    public Vector3Int CampfireStartPosition = new Vector3Int(0, 0, 0);
    
    [Header("Prefabs")]
    public GameObject TilePrefab;
    
    private HexGrid _grid;

    public HexCell PlayerStartCell => _grid.GetCell(PlayerStartPosition);
    
    public HexCell CampfireStartCell => _grid.GetCell(CampfireStartPosition);

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
        // Grow selection in rings for a nice effect
        for (var i = 0; i < StartingSize + 1; i++)
        {
            _grid.MakeCells(0, 0, 0, i);
        }
    }

    /// <summary>
    ///  Create a new Tile GameObject.
    /// </summary>
    public GameObject InstantiateTile()
    {
        return Instantiate(TilePrefab, transform);
    }
}
