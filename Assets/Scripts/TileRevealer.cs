using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRevealer : MonoBehaviour
{
    private TileAnimator _animator;
    private Transform _tileCtrl;

    private void Awake()
    {
        _tileCtrl = transform.Find("tile_ctrl");
        _animator = _tileCtrl.GetComponent<TileAnimator>();
        HideTileModels();
    }

    /// <summary>
    /// Hide the tile models.
    /// </summary>
    private void HideTileModels()
    {
        _tileCtrl.gameObject.SetActive(false);
    }
    
    /// <summary>
    /// Show the tile models
    /// </summary>
    private void ShowTileModels()
    {
        _tileCtrl.gameObject.SetActive(true);
    }

    /// <summary>
    /// Sets a random rotation for this tile.
    /// </summary>
    private void SetRandomRotation()
    {
        // Set random rotation
        var rotations = new List<float> { 0f, 60f, 120f, 180f, 240f, 300f};
        var randomRotation = rotations[Random.Range(0, 6)];
        _tileCtrl.localRotation = Quaternion.Euler(0f, randomRotation, 0f);
    }

    /// <summary>
    /// Reveal the tile.
    /// Once it has been revealed, this component will be deleted.
    /// </summary>
    public IEnumerator Reveal()
    {
        ShowTileModels();
        SetRandomRotation();
        yield return _animator.Flip();
        Destroy(this);
    }
}
