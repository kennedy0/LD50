using System.Collections;
using TMPro;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private WoodUi _ui;

    private void Awake()
    {
        _text = GameObject.Find("Wood Counter").GetComponent<TextMeshProUGUI>();
        _ui = GameObject.Find("Wood UI").transform.Find("ctrl").GetComponent<WoodUi>();
    }

    // Collect the actor.
    public void Collect()
    {
        // ToDo: All of this...
        SoundEffects.SoundEffectsMaster.PlayWood();
        Player.Inventory.Wood += 1;
        _text.SetText(Player.Inventory.Wood.ToString());
        _ui.ShowUi();
        StartCoroutine(PlayCollectAnimation());
    }

    /// <summary>
    /// Play the animation when this is collected.
    /// ToDo: Temp
    /// </summary>
    private IEnumerator PlayCollectAnimation()
    {
        var timer = 0f;
        var animationTime = .25f;
        var distance = 2f;
        
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + (Vector3.up * distance);
        Vector3 startRot = transform.localRotation.eulerAngles;
        Vector3 endRot = startRot;
        endRot.y += 360f;
        Vector3 startScale = transform.localScale;
        Vector3 endScale = startScale * .5f;
        
        while (timer <= animationTime)
        {
            var t = timer / animationTime;
            transform.position = Vector3.Lerp(startPos, endPos, t);
            transform.localEulerAngles = Vector3.Lerp(startRot, endRot, t);
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
