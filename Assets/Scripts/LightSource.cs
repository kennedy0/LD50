using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class LightSource : MonoBehaviour
{
    public int Radius = 1;

    private Actor _actor;

    private void Awake()
    {
        _actor = GetComponent<Actor>();
    }

    /// <summary>
    /// Check if a tile is in range of this light source.
    /// </summary>
    public bool TileInRange(Tile tile)
    {
        if (tile.Cell.DistanceTo(_actor.Cell) <= Radius)
        {
            return true;
        }
        
        return false;
    }
}
