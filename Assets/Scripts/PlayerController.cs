using Actions;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Actor _actor;

    private void Awake()
    {
        _actor = GetComponent<Actor>();
    }

    private void Update()
    {
        if (_actor.ActionReady)
        {
            HandleInput();
        }
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            _actor.Action<Move>(_actor.Cell.NorthWest);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            _actor.Action<Move>(_actor.Cell.North);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            _actor.Action<Move>(_actor.Cell.NorthEast);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _actor.Action<Move>(_actor.Cell.SouthWest);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _actor.Action<Move>(_actor.Cell.South);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _actor.Action<Move>(_actor.Cell.SouthEast);
        }
    }
}
