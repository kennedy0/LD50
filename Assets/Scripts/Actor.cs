using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Action = Actions.Action;

[RequireComponent(typeof(Token))]
public class Actor : MonoBehaviour
{
    public delegate IEnumerator TurnAction();
    public event TurnAction OnTurnStart;
    public event TurnAction OnActionStart;
    public event TurnAction OnActionFinish;
    public event TurnAction OnTurnFinish;
    
    private HexCell _cell;
    private Token _token;
    private int _actions;
    
    private bool _isTurn;
    private bool _actionReady;
    private bool _actionFinished;
    
    public HexCell Cell => _cell;
    
    public Token Token => _token;

    public int Actions => _actions;

    public bool IsTurn => _isTurn;

    public bool ActionReady => _actionReady;

    public override string ToString()
    {
        return gameObject.name;
    }

    private void Awake()
    {
        _cell = null;
        _token = GetComponent<Token>();
        _actions = 1;

        _isTurn = false;
        _actionReady = false;
        _actionFinished = false;
        
        GameManager.AddActor(this);
    }

    /// <summary>
    /// Set the cell that the actor is on.
    /// </summary>
    public void SetCell(HexCell cell)
    {
        _cell = cell;
    }

    /// <summary>
    /// Place the actor on a cell.
    /// </summary>
    public IEnumerator Place(HexCell cell)
    {
        SetCell(cell);
        Token.SnapToCell(cell);
        yield return null;
    }

    /// <summary>
    /// Runs immediately before the actor's turn starts.
    /// </summary>
    public IEnumerator BeforeTurn()
    {
        Debug.Log($"{this} turn start.");
        _isTurn = true;
        
        // Turn start callbacks
        if (OnTurnStart != null)
        {
            yield return OnTurnStart();
        }
        
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
        _isTurn = false;
        
        // Turn finish callbacks
        if (OnTurnFinish != null)
        {
            yield return OnTurnFinish();
        }
        
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
        // Action start callbacks
        if (OnActionStart != null)
        {
            yield return OnActionStart();
        }
        
        // Action
        Debug.Log(action.ActionText(this, target));
        yield return action.DoAction(this, target);
        _actionFinished = true;
        
        // Action finish callbacks
        if (OnActionFinish != null)
        {
            yield return OnActionFinish();
        }
    }
}
