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

    public Vector3 WorldPosition => GetWorldPosition();

    private void Awake()
    {
        _tileVisibility = GetComponent<TileVisibility>();
    }

    /// <summary>
    /// Link this tile to a cell.
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

    private void Start()
    {
        // Set tile name and position
        gameObject.name = $"Tile ({Cell.Q}, {Cell.R}, {Cell.S})";
        transform.position = WorldPosition;
        
        // Set height of base piece
        var tileBase = transform.Find("tile_ctrl").Find("tile_base");
        tileBase.localScale = new Vector3(1f, Cell.Height, 1f);
    }

    /// <summary>
    /// Get the world position of this tile.
    /// </summary>
    private Vector3 GetWorldPosition()
    {
        // If the tile hasn't been linked to a cell yet, it has no position.
        if (Cell == null)
        {
            return Vector3.zero;
        }

        return new Vector3(Cell.WorldPosition.x, Cell.Height * Utilities.TILE_HEIGHT, Cell.WorldPosition.z);
    }

    /// <summary>
    /// Show or hide the tile if it's near a light source.
    /// </summary>
    public void UpdateVisibility()
    {
        StartCoroutine(_tileVisibility.UpdateVisibility());
    }
}
