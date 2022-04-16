using System.Collections;
using Actions;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class Campfire : MonoBehaviour
{
    public int Fuel = 50;

    // ToDo : Temp!
    public GameObject WoodFx;
    public Description Description;
    // ToDo : Temp!
    
    private Actor _actor;
    private TextMeshProUGUI _woodText;

    public HexCell Cell => _actor.Cell;

    private void Awake()
    {
        _woodText = GameObject.Find("Wood Counter").GetComponent<TextMeshProUGUI>();
        _actor = GetComponent<Actor>();
    }

    private void Start()
    {
        // Update Description - ToDo: Temp!
        Description.DescriptionText = $"Rounds left: {Fuel}";
    }

    private void Update()
    {
        if (_actor.ActionReady)
        {
            Burn();
        }
    }

    /// <summary>
    /// Burn fuel.
    /// </summary>
    private void Burn()
    {
        Fuel -= 1;
        _actor.Action<Pass>(Cell);
        
        // Update Description - ToDo: Temp!
        Description.DescriptionText = $"Rounds left: {Fuel}";
        if (Fuel <= 0)
        {
            GameManager.EndGame();
        }
    }

    /// <summary>
    /// Spend wood to add fuel to the fire.
    /// </summary>
    public void Kindle(int wood)
    {
        // Update fuel count
        Fuel += wood * 10;
        
        // Play FX  - ToDo: Temp!
        StartCoroutine(PlayWoodFx(wood));
        
        // Update UI
        _woodText.SetText("0");
    }

    /// <summary>
    /// ToDo: Temp!
    /// </summary>
    private IEnumerator PlayWoodFx(int count)
    {
        var pause = .1f;
        for (var i = 0; i < count; i++)
        {
            var wood = Instantiate(WoodFx);
            wood.transform.position = transform.position + (Vector3.up * 3.5f);
            yield return new WaitForSeconds(pause);
        }
    }
}
