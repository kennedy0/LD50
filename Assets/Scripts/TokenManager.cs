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
        var actor = tokenObject.GetComponent<Actor>();
        yield return actor.Place(cell);
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
