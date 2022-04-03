using System.Collections;
using UnityEngine;

public class AnimateHex : MonoBehaviour
{
    public float FlipTime = .5f;

    private float _timer = 0f;
    private bool _isPlaying = false;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayFlipAnimation();
        }
    }

    private void PlayFlipAnimation()
    {
        if (_isPlaying)
        {
            return;
        }

        StartCoroutine(Flip());
    }

    private IEnumerator Flip()
    {
        // Start animation
        _isPlaying = true;
        
        // Animate
        Quaternion startRot = Quaternion.Euler(0f, 0f, -180f);
        Quaternion endRot = Quaternion.Euler(0f, 0f, 0f);
        
        _timer = 0f;
        while (_timer < FlipTime)
        {
            _timer += Time.deltaTime;
            var t = _timer / FlipTime;
            Quaternion.Lerp(startRot, endRot, t);
            yield return null;
        }

        transform.rotation = endRot;

        // End animation
        _isPlaying = false;
    }
}
