using System.Collections;
using UnityEngine;

public class WoodFx : MonoBehaviour
{
    // ToDo: This entire script is garbage.
    
    private void Start()
    {
        StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation()
    {
        SoundEffects.SoundEffectsMaster.PlayWood();
        
        var timer = 0f;
        var animationTime = .25f;
        var distance = 5f;
        
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + (Vector3.down * distance);
        Quaternion startRot = Random.rotation;
        Quaternion endRot = Random.rotation;
        Vector3 startScale = transform.localScale;
        Vector3 endScale = transform.localScale;
        
        while (timer <= animationTime)
        {
            var t = timer / animationTime;
            transform.position = Vector3.Lerp(startPos, endPos, t);
            transform.localRotation = Quaternion.Lerp(startRot, endRot, t);
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
            timer += Time.deltaTime;
            yield return null;
        }

        FindObjectOfType<Embers>().PlayBurst();
        Destroy(gameObject);
    }
}
