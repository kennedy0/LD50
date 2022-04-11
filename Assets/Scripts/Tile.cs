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
    private bool _isRevealed;
    
    public HexCell Cell => _cell;

    public Vector3 WorldPosition => GetWorldPosition();

    public bool IsRevealed => _isRevealed;

    private void Awake()
    {
        _tileVisibility = GetComponent<TileVisibility>();
    }

    /// <summary>
    /// Initialize this tile by linking it to its cell.
    /// </summary>
    public void InitTile(HexCell cell)
    {
        // Link tile to cell.
        if (Cell != null)
        {
            Debug.LogError($"{this} is already linked to cell {cell}.");
            return;
        }

        _cell = cell;
        
        // Set tile name and position
        gameObject.name = $"Tile ({cell.Q}, {cell.R}, {cell.S})";
        transform.position = WorldPosition;
        
        // Set height of base piece
        var tileBase = transform.Find("tile_ctrl").Find("tile_base");
        tileBase.localScale = new Vector3(1f, cell.Height, 1f);
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
    /// Reveal this tile.
    /// </summary>
    public void Reveal()
    {
        if (IsRevealed)
        {
            Debug.LogError($"{this} has already been revealed.");
            return;
        }

        // Reveal the tile
        _isRevealed = true;
        var revealer = GetComponent<TileRevealer>();
        StartCoroutine(revealer.Reveal());
    }

    /// <summary>
    /// Change the color on the tile if it's visible.
    /// </summary>
    public void UpdateVisibility()
    {
        StartCoroutine(_tileVisibility.UpdateVisibility());
    }
}
