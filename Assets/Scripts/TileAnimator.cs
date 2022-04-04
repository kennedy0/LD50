using System.Collections;
using UnityEngine;

public class TileAnimator : MonoBehaviour
{
    private float _timer;
    private bool _isPlaying;
    
    /// <summary>
    /// Trigger the flip animation.
    /// </summary>
    public void PlayFlipAnimation(float flipTime)
    {
        if (_isPlaying)
        {
            return;
        }

        StartCoroutine(Flip(flipTime));
    }

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
    private IEnumerator Flip(float flipTime)
    {
        // Start animation
        _isPlaying = true;
        PlayFlipSound();
        
        // Animate rotation from face-down to face-up
        Quaternion startRot = transform.rotation * Quaternion.Euler(0f, 0f, -180f);
        Quaternion endRot = transform.rotation * Quaternion.Euler(0f, 0f, 0f);
        
        _timer = 0f;
        while (_timer < flipTime)
        {
            _timer += Time.deltaTime;
            var t = _timer / flipTime;
            transform.localRotation = Quaternion.Lerp(startRot, endRot, t);
            yield return null;
        }

        // Snap to final rotation
        transform.localRotation = endRot;

        // End animation
        _isPlaying = false;
    }
}
