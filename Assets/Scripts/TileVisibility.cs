using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileVisibility : MonoBehaviour
{
    private Tile _tile;
    private Transform _tileCtrl;
    private GameObject _realTile;
    private GameObject _blankTile;
    
    private void Awake()
    {
        _tile = GetComponent<Tile>();
        _tileCtrl = transform.Find("tile_ctrl");
        _realTile = _tileCtrl.Find("real_tile").gameObject;
        _blankTile = _tileCtrl.Find("blank_tile").gameObject;
        
        _realTile.SetActive(false);
        _blankTile.SetActive(false);
    }

    private void Start()
    {
        ShowBlank();
    }

    /// <summary>
    /// Update the visibility on the tile based on the cell's visibility value.
    /// </summary>
    public IEnumerator UpdateVisibility()
    {
        if (_tile.Cell.IsVisible)
        {
            ShowReal();
        }
        else
        {
            ShowBlank();
        }

        yield return null;
    }

    /// <summary>
    /// Show the blank tile.
    /// </summary>
    private void ShowBlank()
    {
        _blankTile.SetActive(true);
        _realTile.SetActive(false);
    }

    /// <summary>
    /// Show the real tile.
    /// </summary>
    private void ShowReal()
    {
        _blankTile.SetActive(false);
        _realTile.SetActive(true);
    }
}
