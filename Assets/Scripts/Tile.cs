using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Tile : MonoBehaviour
{
    private HexCell _cell;
    private Transform _tileCtrl;
    private GameObject _realTile;
    private GameObject _blankTile;
    private TileAnimator _animator;

    private const float REVEAL_DELAY_TIME = .05f;
    private const float FLIP_TIME = .3f;
    private static float _revealTimer;

    private void Awake()
    {
        _tileCtrl = transform.Find("tile_ctrl");
        _realTile = _tileCtrl.Find("real_tile").gameObject;
        _blankTile = _tileCtrl.Find("blank_tile").gameObject;
        _animator = _tileCtrl.GetComponent<TileAnimator>();
        
        _realTile.SetActive(false);
        _blankTile.SetActive(false);
    }

    /// <summary>
    /// This is called by the parent cell after this Tile has been attached to the GameObject.
    /// Runs after Awake, before Start.
    /// </summary>
    public void InitTile(HexCell cell)
    {
        _cell = cell;
        gameObject.name = $"Tile ({cell.Q}, {cell.R}, {cell.S})";
        transform.position = cell.WorldPosition;
        
        // Set random rotation
        var rotations = new List<float> { 0f, 60f, 120f, 180f, 240f, 300f};
        var randomRotation = rotations[UnityEngine.Random.Range(0, 6)];
        _tileCtrl.localRotation = Quaternion.Euler(0f, randomRotation, 0f);
        
        StartCoroutine(RevealTile());  // ToDo: Don't reveal as soon as it's created, wait until the player is in some proximity
    }

    /// <summary>
    /// Visual setup and animation the first time the tile is revealed.
    /// ToDo: Put this in a separate TileRevealer component that can delete itself when it's done.
    /// </summary>
    private IEnumerator RevealTile()
    {
        _revealTimer += REVEAL_DELAY_TIME;
        yield return new WaitForSeconds(_revealTimer);
        _realTile.SetActive(true);  // ToDo: Always use a method to see if real/blank tile should be used
        _animator.PlayFlipAnimation(FLIP_TIME);
    }

    private void Update()
    {
        UpdateRevealTimer();
    }

    /// <summary>
    /// Updates the global timer that delays the tile reveal.
    /// </summary>
    private void UpdateRevealTimer()
    {
        _revealTimer -= Time.deltaTime;
        if (_revealTimer < 0f)
        {
            _revealTimer = 0f;
        }
    }
}
