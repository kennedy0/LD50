using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Token _token;
    private bool _canMove;

    private void Awake()
    {
        _token = GetComponent<Token>();
    }

    void Start()
    {
        _token.SnapToCell(_token.Board.PlayerStartCell);
        StartCoroutine(DelayInputHandling());
    }

    void Update()
    {
        HandleInput();
    }

    /// <summary>
    /// Wait a bit before allowing player control.
    /// ToDo: Remove this when there's an actual game loop.
    /// </summary>
    private IEnumerator DelayInputHandling()
    {
        yield return new WaitForSeconds(3f);
        _canMove = true;
    }

    /// <summary>
    /// Handle player input.
    /// ToDo: This is very demo-ish. All of this should be handled by a more robust game manager / action system.
    /// </summary>
    private void HandleInput()
    {
        if (!_canMove)
        {
            return;
        }

        bool moved = false;
        HexCell newCell = null;
        int distance = 1;
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            _token.SetFacingDirection(Direction.North);
            _token.Move(_token.CurrentCell.North);
            moved = true;
            newCell = _token.CurrentCell.North;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _token.SetFacingDirection(Direction.South);
            _token.Move(_token.CurrentCell.South);
            moved = true;
            newCell = _token.CurrentCell.South;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            _token.SetFacingDirection(Direction.NorthWest);
            _token.Move(_token.CurrentCell.NorthWest);
            moved = true;
            newCell = _token.CurrentCell.NorthWest;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            _token.SetFacingDirection(Direction.NorthEast);
            _token.Move(_token.CurrentCell.NorthEast);
            moved = true;
            newCell = _token.CurrentCell.NorthEast;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _token.SetFacingDirection(Direction.SouthWest);
            _token.Move(_token.CurrentCell.SouthWest);
            moved = true;
            newCell = _token.CurrentCell.SouthWest;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _token.SetFacingDirection(Direction.SouthEast);
            _token.Move(_token.CurrentCell.SouthEast);
            moved = true;
            newCell = _token.CurrentCell.SouthEast;
        }

        if (moved && newCell != null)
        {
            _token.Board.Grid.MakeCells(newCell.Q, newCell.R, newCell.S, distance);
        }
    }
}
