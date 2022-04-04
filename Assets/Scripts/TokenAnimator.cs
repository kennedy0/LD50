using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenAnimator : MonoBehaviour
{
    private float _timer;
    private bool _isPlaying;
    
    // Determines at what point through the move animation the "Put Down" sound should play.
    // Playing this slightly before the token lands feels better.
    private const float PUT_DOWN_SOUND_TIME = .8f;
    
    /// <summary>
    /// Trigger the move animation.
    /// </summary>
    public void PlayMoveAnimation(float moveTime, HexCell startCell, HexCell endCell)
    {
        if (_isPlaying)
        {
            return;
        }

        StartCoroutine(Move(moveTime, startCell, endCell));
    }
    
    /// <summary>
    /// Plays when the token is picked up.
    /// </summary>
    private void PlayPickUpSound()
    {
        SoundEffects.SoundEffectsMaster.PlayKnock();
    }

    /// <summary>
    /// Plays when the token is put down.
    /// </summary>
    private void PlayPutDownSound()
    {
        SoundEffects.SoundEffectsMaster.PlayWood();
    }
    
    /// <summary>
    /// Coroutine that moves and animates the player.
    /// </summary>
    private IEnumerator Move(float moveTime, HexCell startCell, HexCell endCell)
    {
        // Start animation
        _isPlaying = true;
        bool playedPutDownSound = false;
        
        PlayPickUpSound();

        // Animate the token moving from one tile to another.
        var startPos = startCell.WorldPosition;
        var endPos = endCell.WorldPosition;
        var timer = 0f;
        while (timer < moveTime)
        {
            // Update timer
            timer += Time.deltaTime;
            var t = timer / moveTime;
            
            // Animate position
            transform.position = Vector3.Lerp(startPos, endPos, t);

            // Play the Put Down sound.
            if (t >= PUT_DOWN_SOUND_TIME && !playedPutDownSound)
            {
                SoundEffects.SoundEffectsMaster.PlayWood();
                playedPutDownSound = true;
            }

            yield return null;
        }

        // Snap position
        transform.position = endPos;
        
        // End animation
        _isPlaying = false;
    }
}
