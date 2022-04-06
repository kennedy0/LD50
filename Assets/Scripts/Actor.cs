using System.Collections;
using UnityEngine;

public class Actor : MonoBehaviour
{
    private HexCell _cell;
    private Token _token;
    private bool _ready;
    private bool _turnFinished;

    public HexCell Cell => _cell;
    
    public Token Token => _token;

    public bool Ready => _ready;

    private void Awake()
    {
        _cell = null;
        _token = GetComponent<Token>();
        _ready = false;
        _turnFinished = false;
        
        GameManager.AddActor(this);
    }

    /// <summary>
    /// Place the actor on a cell.
    /// </summary>
    public IEnumerator Place(HexCell cell)
    {
        Token.SnapToCell(cell);
        _cell = cell;
        yield return null;
    }

    /// <summary>
    /// Runs immediately before the actor's turn starts.
    /// </summary>
    public IEnumerator BeforeTurn()
    {
        _ready = true;
        _turnFinished = false;
        yield return null;
    }

    /// <summary>
    /// This is where the actor makes their action(s) for this round.
    /// </summary>
    /// <returns></returns>
    public IEnumerator Turn()
    {
        while (!_turnFinished)
        {
            yield return null;
        }
    }

    /// <summary>
    /// Runs immediately after the actor's turn ends.
    /// </summary>
    public IEnumerator AfterTurn()
    {
        yield return null;
    }

    /// <summary>
    /// Move the actor to a cell.
    /// </summary>
    public void Move(HexCell cell)
    {
        _ready = false;
        StartCoroutine(DoMove(cell));
    }
    
    private IEnumerator DoMove(HexCell cell)
    {
        var oldCell = _cell;
        var newCell = cell;
        yield return Token.Move(oldCell, newCell);
        _cell = newCell;
        _turnFinished = true;
    }

    /// <summary>
    /// Skip the current move.
    /// </summary>
    public void Pass()
    {
        _ready = false;
        _turnFinished = true;
    }
}
