using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenManager : MonoBehaviour
{
    public static TokenManager Instance;
    
    public GameObject Player;

    public GameObject Campfire;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Create a token on a cell.
    /// </summary>
    private static IEnumerator CreateToken(GameObject tokenPrefab, HexCell cell)
    {
        var tokenObject = Instantiate(tokenPrefab);
        SetName(tokenObject);
        var actor = tokenObject.GetComponent<Actor>();
        yield return actor.Place(cell);
    }

    /// <summary>
    /// Set the name of a newly created token.
    /// </summary>
    private static void SetName(GameObject go)
    {
        // Take '(Clone)' out of the name.
        go.name = go.name.Replace("(Clone)", "");
    }

    public static IEnumerator CreatePlayer(HexCell cell)
    {
        yield return CreateToken(Instance.Player, cell);
    }
    
    public static IEnumerator CreateCampfire(HexCell cell)
    {
        yield return CreateToken(Instance.Campfire, cell);
    }
}
