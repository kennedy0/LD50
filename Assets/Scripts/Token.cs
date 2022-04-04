using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{
    private HexCell _cell;
    private TokenAnimator _animator;

    // The amount of time it takes the token to move.
    private const float MOVE_TIME = .3f;

    private void Awake()
    {
        _animator = GetComponent<TokenAnimator>();
    }
    /// <summary>
    /// Move the token to a cell.
    /// </summary>
    public void Move(HexCell cell)
    {
        _animator.PlayMoveAnimation(MOVE_TIME, _cell, cell);
        _cell = cell;
    }

    /// <summary>
    /// Snap this token's position to a cell position.
    /// </summary>
    public void SnapToCellPosition(HexCell cell)
    {
        transform.position = cell.WorldPosition;
    }
}
