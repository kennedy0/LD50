using System.Collections;
using System.Collections.Generic;
using Datatypes;
using UnityEngine;

[RequireComponent(typeof(Actor))]
[RequireComponent(typeof(TokenAnimator))]
public class Token : MonoBehaviour
{
    private Actor _actor;
    private TokenVisibility _tokenVisibility;
    private Transform _tokenCtrl;
    private TokenAnimator _animator;

    // The amount of time it takes the token to move.
    private const float MOVE_TIME = .3f;

    public Actor Actor => _actor;

    private void Awake()
    {
        _actor = GetComponent<Actor>();
        _tokenVisibility = GetComponent<TokenVisibility>();
        _tokenCtrl = transform.Find("token_ctrl");
        _animator = GetComponent<TokenAnimator>();
    }

    /// <summary>
    /// Move the token to a tile.
    /// </summary>
    public IEnumerator Move(Tile oldTile, Tile newTile)
    {
        FaceTile(oldTile, newTile);
        yield return _animator.PlayMoveAnimation(MOVE_TIME, oldTile, newTile);
    }

    /// <summary>
    /// Snap the token to a tile, without any animation.
    /// </summary>
    public void SnapToTile(Tile tile)
    {
        transform.position = tile.WorldPosition;
    }

    /// <summary>
    /// Face this token towards a tile.
    /// </summary>
    public void FaceTile(Tile currentTile, Tile facingTile)
    {
        // Calculate based on the cell position (which is y-zero) to avoid wonky rotations.
        var target = (facingTile.Cell.WorldPosition - currentTile.Cell.WorldPosition).normalized;
        _tokenCtrl.rotation = Quaternion.LookRotation(target);
    }
    
    /// <summary>
    /// Change the color on the tile if it's visible.
    /// </summary>
    public void UpdateVisibility()
    {
        StartCoroutine(_tokenVisibility.UpdateVisibility());
    }
}
