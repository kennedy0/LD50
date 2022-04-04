using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Token _token;

    private void Awake()
    {
        _token = GetComponent<Token>();
    }

    void Start()
    {
        _token.SnapToCell(_token.Board.PlayerStartCell);
    }

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _token.SetFacingDirection(Direction.North);
            _token.Move(_token.CurrentCell.North);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _token.SetFacingDirection(Direction.South);
            _token.Move(_token.CurrentCell.South);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            _token.SetFacingDirection(Direction.NorthWest);
            _token.Move(_token.CurrentCell.NorthWest);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            _token.SetFacingDirection(Direction.NorthEast);
            _token.Move(_token.CurrentCell.NorthEast);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _token.SetFacingDirection(Direction.SouthWest);
            _token.Move(_token.CurrentCell.SouthWest);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _token.SetFacingDirection(Direction.SouthEast);
            _token.Move(_token.CurrentCell.SouthEast);
        }
    }
}
