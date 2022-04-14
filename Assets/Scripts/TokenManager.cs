using System.Collections;
using System.Collections.Generic;
using Actions;
using UnityEngine;

public class TokenManager : MonoBehaviour
{
    [Header("Special")]
    public GameObject Player;
    public GameObject Campfire;
    
    [Header("Resources")]
    public GameObject Wood;
    
    private static TokenManager _instance;

    private void Awake()
    {
        _instance = this;
    }

    /// <summary>
    /// Create a token on a cell.
    /// </summary>
    private static void CreateToken(GameObject tokenPrefab, HexCell cell)
    {
        // Don't place a token on a cell that's already occupied.
        if (cell.Actor != null)
        {
            Debug.LogError($"Cannot place a token on cell {cell}; it already has an actor: {cell.Actor}");
        }

        // Instantiate object
        var tokenObject = Instantiate(tokenPrefab);
        SetName(tokenObject);
        
        // Initialize actor 
        var actor = tokenObject.GetComponent<Actor>();
        actor.Place(cell);
    }

    /// <summary>
    /// Set the name of a newly created token.
    /// </summary>
    private static void SetName(GameObject go)
    {
        // Take '(Clone)' out of the name.
        go.name = go.name.Replace("(Clone)", "");
    }

    public static void CreatePlayer(HexCell cell)
    {
        CreateToken(_instance.Player, cell);
    }
    
    public static void CreateCampfire(HexCell cell)
    {
        CreateToken(_instance.Campfire, cell);
    }

    public static void CreateWood(HexCell cell)
    {
        CreateToken(_instance.Wood, cell);
    }
}
