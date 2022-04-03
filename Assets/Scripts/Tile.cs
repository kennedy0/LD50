using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private HexCell _cell;

    private Transform _tileCtrl;
    private GameObject _realTile;
    private GameObject _blankTile;

    private void Awake()
    {
        _tileCtrl = transform.Find("tile_ctrl");
        _realTile = _tileCtrl.Find("real_tile").gameObject;
        _blankTile = _tileCtrl.Find("blank_tile").gameObject;
    }

    /// <summary>
    /// This is called by the parent cell after this Tile has been attached to the GameObject.
    /// Runs after Awake, before Start.
    /// </summary>
    public void InitTile(HexCell cell)
    {
        _cell = cell;
        gameObject.name = $"Tile ({cell.Q}, {cell.R}, {cell.S})";
        transform.position = Utilities.GridToWorldPosition(cell.Q, cell.R, cell.S);
        _tileCtrl.localRotation = Utilities.RandomRotation();
    }
}
