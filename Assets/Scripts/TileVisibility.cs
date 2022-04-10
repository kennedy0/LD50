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
    /// Check to see if this tile is within range of any light source.
    /// ToDo: This check is extremely expensive.
    /// </summary>
    public void UpdateVisibility()
    {
        ShowBlank();
        
        var lightSources = FindObjectsOfType<LightSource>();
        foreach (var lightSource in lightSources)
        {
            if (lightSource.TileInRange(_tile))
            {
                ShowReal();
            }
        }
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
