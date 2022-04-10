using System.Collections;
using UnityEngine;

public class TileAnimator : MonoBehaviour
{
    public float FlipTime = .3f;

    /// <summary>
    /// Plays when the tile flip happens.
    /// </summary>
    private void PlayFlipSound()
    {
        SoundEffects.SoundEffectsMaster.PlayRockWobble();
    }

    /// <summary>
    /// Flip animation.
    /// </summary>
    public IEnumerator Flip()
    {
        PlayFlipSound();
        
        // Animate rotation from face-down to face-up
        Quaternion startRot = transform.rotation * Quaternion.Euler(0f, 0f, -180f);
        Quaternion endRot = transform.rotation * Quaternion.Euler(0f, 0f, 0f);
        
        var timer = 0f;
        while (timer < FlipTime)
        {
            timer += Time.deltaTime;
            var t = timer / FlipTime;
            transform.localRotation = Quaternion.Lerp(startRot, endRot, t);
            yield return null;
        }

        // Snap to final rotation
        transform.localRotation = endRot;
    }
}
