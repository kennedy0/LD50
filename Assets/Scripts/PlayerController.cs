using Actions;
using ExtensionMethods;
using UnityEngine;

[RequireComponent(typeof(Actor))]
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
        HexCell targetCell;
        
        // Get input to target cell
        if (Input.GetKey(KeyCode.Q))
        {
            targetCell = _actor.Cell.NorthWest;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            targetCell = _actor.Cell.North;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            targetCell = _actor.Cell.NorthEast;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            targetCell = _actor.Cell.SouthWest;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            targetCell = _actor.Cell.South;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            targetCell = _actor.Cell.SouthEast;
        }
        else
        {
            return;
        }

        // Do action on target cell
        if (!targetCell.Occupied)
        {
            _actor.Action<Move>(targetCell);
        }
        else if (targetCell.Actor.IsCollectable)
        {
            _actor.Action<Collect>(targetCell);
        }
        else if (targetCell.Actor.gameObject.HasComponent<Campfire>())
        {
            _actor.Action<Kindle>(targetCell);
        }
    }
}
