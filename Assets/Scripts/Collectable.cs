using TMPro;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GameObject.Find("Wood Counter").GetComponent<TextMeshProUGUI>();
    }

    // Collect the actor.
    public void Collect()
    {
        Player.Inventory.Wood += 1;
        _text.SetText(Player.Inventory.Wood.ToString());
        Destroy(gameObject);
    }
}
