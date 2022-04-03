using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class OldHexTile : MonoBehaviour
{
    [Header("Grid")]
    public int X;
    public int Y;

    private bool _visible = true;

    private OldHexGrid _grid;

    private Transform _hexCtrl;
    private AnimateHex _animateHex;

    private GameObject _hex;
    private GameObject _hexBlank;

    public bool Visible => _visible;

    void Awake()
    {
        _hexCtrl = transform.Find("hex_ctrl");
        _animateHex = _hexCtrl.GetComponent<AnimateHex>();

        _hex = _hexCtrl.Find("hex").gameObject;
        _hexBlank = _hexCtrl.Find("hex_blank").gameObject;
        
        // Start hidden - it will be revealed during RevealTile
        _hexCtrl.gameObject.SetActive(false);
    }

    /// <summary>
    /// Store a reference to the owning grid.
    /// </summary>
    public void SetGrid(OldHexGrid grid)
    {
        _grid = grid;
    }

    /// <summary>
    /// Initialize the tile.
    /// </summary>
    public void InitTile(float creationDelay)
    {
        // Set random rotation
        _hexCtrl.localRotation = OldUtilities.RotationFromDirection(OldUtilities.RandomDirection());
        
        // Start the reveal animation
        StartCoroutine(RevealTile(creationDelay));
    }

    // This handles the animation and visual setup the first time the tile is revealed.
    private IEnumerator RevealTile(float creationDelay)
    {
        yield return new WaitForSeconds(creationDelay);
        _hexCtrl.gameObject.SetActive(true);
        UpdateVisibility();
        _animateHex.PlayFlipAnimation();
    }

    /// <summary>
    /// Hide a tile when it's too far away.
    /// </summary>
    private void HideTile()
    {
        if (!_visible)
        {
            return;
        }

        _hex.SetActive(false);
        _hexBlank.SetActive(true);
        _visible = false;
    }

    /// <summary>
    /// Show a tile when the player is close enough.
    /// </summary>
    private void ShowTile()
    {
        if (_visible)
        {
            return;
        }

        _hex.SetActive(true);
        _hexBlank.SetActive(false);
        _visible = true;
    }

    /// <summary>
    /// Update the visibility of this tile
    /// </summary>
    private void UpdateVisibility()
    {
        if (AdjacentToPlayer() || AdjacentToCampfire())
        {
            ShowTile();
        }
        else
        {
            HideTile();
        }
    }

    /// <summary>
    /// Runs when the player position is changed.
    /// </summary>
    public void HandlePlayerPositionUpdate(int old_gx, int old_gy, int new_gx, int new_gy)
    {
        UpdateVisibility();
    }

    /// <summary>
    /// Returns true if this tile is adjacent to the player (or if the player is on this tile).
    /// </summary>
    private bool AdjacentToPlayer()
    {
        var distance = 1;
        var myNeighbors = _grid.GetTiles(X, Y, distance);
        var playerTile = _grid.GetTile(_grid.PlayerX, _grid.PlayerY);
        if (myNeighbors.Contains(playerTile))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Returns true if this tile is adjacent to the campfire.
    /// </summary>
    /// <returns></returns>
    private bool AdjacentToCampfire()
    {
        var distance = 2;
        var myNeighbors = _grid.GetTiles(X, Y, distance);
        var campfireTile = _grid.GetTile(0, 0);
        if (myNeighbors.Contains(campfireTile))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
