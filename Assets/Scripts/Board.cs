using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private HexGrid _grid;

    void Awake()
    {
        _grid = new HexGrid();
    }
}
