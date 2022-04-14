using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Action = Actions.Action;

[RequireComponent(typeof(Token))]
public class Actor : MonoBehaviour
{
    public bool NoActions;
    
    private HexCell _cell;
    private Token _token;
    private int _actions;
    private bool _isIsVisible;
    
    private bool _actionReady;
    private bool _actionFinished;
    
    public HexCell Cell => _cell;
    
    public Token Token => _token;

    public int Actions => _actions;

    public bool ActionReady => _actionReady;

    public bool IsVisible => _isIsVisible;

    public override string ToString()
    {
        return gameObject.name;
    }

    private void Awake()
    {
        _cell = null;
        _token = GetComponent<Token>();
        _actions = 1;
        _isIsVisible = false;

        _actionReady = false;
        _actionFinished = false;

        // Only add the actor to the game manager if it can take actions.
        if (!NoActions)
        {
            GameManager.AddActor(this);
        }
    }

    /// <summary>
    /// Place this actor on a cell.
    /// </summary>
    public void Place(HexCell cell)
    {
        SetCell(cell);
        Token.SnapToTile(cell.Tile);
    }

    /// <summary>
    /// Set the cell that the actor is on.
    /// </summary>
    public void SetCell(HexCell cell)
    {
        var oldCell = _cell;
        var newCell = cell;
        
        _cell = newCell;
        OnCellChange(oldCell, newCell);
    }

    /// <summary>
    /// Send updates to cells when the position changes.
    /// This logic happens here to avoid global updates after every action.
    /// </summary>
    private void OnCellChange(HexCell oldCell, HexCell newCell)
    {
        oldCell?.UnsetActor();
        newCell?.SetActor(this);
        
        GetComponent<Player>()?.OnCellChange(oldCell, newCell);
        GetComponent<LightSource>()?.OnCellChange(oldCell, newCell);
    }

    /// <summary>
    /// Runs immediately before the actor's turn starts.
    /// </summary>
    public IEnumerator BeforeTurn()
    {
        Debug.Log($"{this} turn start.");
        yield return null;
    }

    /// <summary>
    /// This is where the actor makes their action(s) for this round.
    /// </summary>
    /// <returns></returns>
    public IEnumerator Turn()
    {
        for (var i = 0; i < Actions; i++)
        {
            _actionReady = true;
            _actionFinished = false;
            yield return WaitForAction();
        }
    }

    /// <summary>
    /// Wait for an action to be taken before proceeding.
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitForAction()
    {
        while (!_actionFinished)
        {
            yield return null;
        }
    }

    /// <summary>
    /// Runs immediately after the actor's turn ends.
    /// </summary>
    public IEnumerator AfterTurn()
    {
        Debug.Log($"{this} turn end.");
        yield return null;
    }

    /// <summary>
    /// Public entry point that tells this actor to perform an action on a cell.
    /// </summary>
    public void Action<T>(HexCell target) where T : Action, new()
    {
        if (!ActionReady)
        {
            Debug.LogError($"{this} cannot perform an action right now.");
            return;
        }

        _actionReady = false;
        var action = (T)Activator.CreateInstance(typeof(T));
        StartCoroutine(DoAction(action, target));
    }

    /// <summary>
    /// This is the coroutine that actually performs the action.
    /// </summary>
    private IEnumerator DoAction(Action action, HexCell target)
    {
        // Action
        Debug.Log(action.ActionText(this, target));
        yield return action.DoAction(this, target);
        _actionFinished = true;
    }
    
    /// <summary>
    /// Set the visibility of this actor.
    /// </summary>
    public void SetVisibility(bool visibility)
    {
        _isIsVisible = visibility;
        Token.UpdateVisibility();
    }
}
