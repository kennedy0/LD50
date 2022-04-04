using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{
    private Board _board;
    private Transform _tokenCtrl;
    private TokenAnimator _animator;
    private bool _isMoving;
    
    private HexCell _currentCell;

    // The amount of time it takes the token to move.
    private const float MOVE_TIME = .3f;

    public Board Board => _board;

    public HexCell CurrentCell => _currentCell;

    private void Awake()
    {
        _board = GameObject.Find("BOARD").GetComponent<Board>();
        _tokenCtrl = transform.Find("token_ctrl");
        _animator = GetComponent<TokenAnimator>();
    }

    /// <summary>
    /// Move the token to a cell.
    /// </summary>
    public void Move(HexCell cell)
    {
        if (_isMoving)
        {
            return;
        }

        StartCoroutine(DoMove(cell));
    }

    private IEnumerator DoMove(HexCell cell)
    {
        _isMoving = true;
        yield return _animator.PlayMoveAnimation(MOVE_TIME, _currentCell, cell);
        _currentCell = cell;
        _isMoving = false;
    }

    /// <summary>
    /// Snap this token's position to a cell position.
    /// </summary>
    public void SnapToCell(HexCell cell)
    {
        transform.position = cell.WorldPosition;
        _currentCell = cell;
    }

    /// <summary>
    /// Set the direction that this token is facing.
    /// </summary>
    public void SetFacingDirection(Direction direction)
    {
        _tokenCtrl.rotation = Utilities.RotationFromDirection(direction);
    }
}
