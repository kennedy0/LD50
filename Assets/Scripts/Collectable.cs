using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Collect the actor.
    public void Collect()
    {
        Player.Inventory.Wood += 1;
        Destroy(gameObject);
    }
}
