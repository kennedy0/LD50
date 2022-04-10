using System.Collections;
using System.Collections.Generic;
using Datatypes;
using UnityEngine;

[RequireComponent(typeof(Actor))]
[RequireComponent(typeof(TokenAnimator))]
public class Token : MonoBehaviour
{
    private Actor _actor;
    private Transform _tokenCtrl;
    private TokenAnimator _animator;

    // The amount of time it takes the token to move.
    private const float MOVE_TIME = .3f;

    public Actor Actor => _actor;

    private void Awake()
    {
        _actor = GetComponent<Actor>();
        _tokenCtrl = transform.Find("token_ctrl");
        _animator = GetComponent<TokenAnimator>();
    }

    /// <summary>
    /// Move the token to a cell.
    /// </summary>
    public IEnumerator Move(HexCell oldCell, HexCell newCell)
    {
        FaceCell(oldCell, newCell);
        yield return _animator.PlayMoveAnimation(MOVE_TIME, oldCell, newCell);
    }

    /// <summary>
    /// Snap the token to a position, without any animation.
    /// </summary>
    public void SnapToCell(HexCell cell)
    {
        transform.position = cell.WorldPosition;
    }

    /// <summary>
    /// Face this token towards a cell.
    /// </summary>
    public void FaceCell(HexCell fromCell, HexCell toCell)
    {
        var target = (toCell.WorldPosition - fromCell.WorldPosition).normalized;
        _tokenCtrl.rotation = Quaternion.LookRotation(target);
    }
}
