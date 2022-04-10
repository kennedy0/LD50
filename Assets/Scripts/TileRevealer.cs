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
    }

    private void Start()
    {
        SetRandomRotation();
        StartCoroutine(DoReveal());
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
    private IEnumerator DoReveal()
    {
        yield return _animator.Flip();
        Destroy(this);
    }
}
