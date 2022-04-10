using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int TileCreationRange = 2;
    
    private static Player _instance;
    private Actor _actor;

    public static Actor Actor => _instance._actor;

    private void Awake()
    {
        _instance = this;
        _actor = GetComponent<Actor>();
    }

    public static Transform Transform => _instance.transform;

    /// <summary>
    /// Create tiles surrounding the player.
    /// </summary>
    public void MakeSurroundingTiles()
    {
        var range = TileCreationRange;
        StartCoroutine(Board.MakeTiles(Actor.Cell.Q, Actor.Cell.R, Actor.Cell.S, range));
    }
}
