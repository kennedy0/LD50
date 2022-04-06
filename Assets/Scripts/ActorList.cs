using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorList: IEnumerable<Actor>, IEnumerable
{
    // List of actors
    private List<Actor> _actors;
    
    // Lists for add / remove queue
    private List<Actor> _toAdd;
    private List<Actor> _toRemove;

    // Hash sets to quickly check list membership
    private HashSet<Actor> _current;
    private HashSet<Actor> _adding;
    private HashSet<Actor> _removing;

    public int Count => _actors.Count;

    internal ActorList()
    {
        _actors = new List<Actor>();
        _toAdd = new List<Actor>();
        _toRemove = new List<Actor>();
        _current = new HashSet<Actor>();
        _adding = new HashSet<Actor>();
        _removing = new HashSet<Actor>();
    }

    /// <summary>
    /// Implementation for the GetEnumerator method. Allows 'foreach' loops.
    /// </summary>
    public IEnumerator<Actor> GetEnumerator()
    {
        return _actors.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Add an Actor to the list.
    /// </summary>
    public void Add(Actor actor)
    {
        if (!_current.Contains(actor) && !_adding.Contains(actor))
        {
            _toAdd.Add(actor);
            _adding.Add(actor);
        }
    }

    /// <summary>
    /// Remove an Actor from the list.
    /// </summary>
    public void Remove(Actor actor)
    {
        if (_current.Contains(actor) && !_removing.Contains(actor))
        {
            _toRemove.Add(actor);
            _removing.Add(actor);
        }
    }

    /// <summary>
    /// This handles all of the logic for adding and removing actors from the list.
    /// This process is deferred until the end of the current turn.
    /// </summary>
    public void UpdateList()
    {
        // Add queued entities.
        if (_toAdd.Count > 0)
        {
            foreach (var actor in _toAdd)
            {
                _actors.Add(actor);
                _current.Add(actor);
            }
            _toAdd.Clear();
            _adding.Clear();
        }
        
        // Remove queued entities.
        if (_toRemove.Count > 0)
        {
            foreach (var actor in _toRemove)
            {
                _actors.Remove(actor);
                _current.Remove(actor);
            }
            _toRemove.Clear();
            _removing.Clear();
        }
    }
}
