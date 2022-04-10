using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[RequireComponent(typeof(TileVisibility))]
public class Tile : MonoBehaviour
{
    private HexCell _cell;
    private TileVisibility _tileVisibility;
    
    public HexCell Cell => _cell;

    private void Awake()
    {
        _tileVisibility = GetComponent<TileVisibility>();
    }

    /// <summary>
    /// This is called by the parent cell after this tile has been created.
    /// Runs after Awake, before Start.
    /// </summary>
    public void LinkToCell(HexCell cell)
    {
        if (Cell != null)
        {
            Debug.LogError($"{this} is already linked to cell {cell}.");
            return;
        }

        _cell = cell;
    }

    /// <summary>
    /// Show or hide the tile if it's near a light source.
    /// </summary>
    public void UpdateVisibility()
    {
        _tileVisibility.UpdateVisibility();
    }
}
