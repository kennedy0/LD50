using System;
using System.Collections;
using System.Collections.Generic;
using Datatypes;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board Instance;

    [Header("Board Setup")]
    public int StartingSize = 2;
    public Hex PlayerStartPosition = new Hex(0, -1, 1);
    public Hex CampfireStartPosition = new Hex(0, 0, 0);
    
    [Header("Prefabs")]
    public GameObject TilePrefab;
    
    private HexGrid _grid;

    public static HexGrid Grid => Instance._grid;

    private void Awake()
    {
        Instance = this;
        _grid = new HexGrid(this);
    }


    /// <summary>
    ///  Create a new Tile GameObject.
    /// </summary>
    public GameObject InstantiateTile()
    {
        return Instantiate(TilePrefab, transform);
    }
}
