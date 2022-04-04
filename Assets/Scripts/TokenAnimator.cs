using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenAnimator : MonoBehaviour
{
    private Transform _tokenCtrl;

    // Determines at what point through the move animation the "Put Down" sound should play.
    // Playing this slightly before the token lands feels better.
    private const float PUT_DOWN_SOUND_TIME = .8f;
    
    // Determines the height that the token should be picked up while it's moving
    private const float PICK_UP_HEIGHT = .2f;
    
    // Determines the amount that the token should rotate while it's moving
    private const float PICK_UP_ROTATION = 12f;

    private void Awake()
    {
        _tokenCtrl = transform.Find("token_ctrl");
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
    /// Coroutine that moves and animates the token.
    /// </summary>
    public IEnumerator PlayMoveAnimation(float moveTime, HexCell startCell, HexCell endCell)
    {
        bool playedPutDownSound = false;
        PlayPickUpSound();

        // Store constant values
        var startPos = startCell.WorldPosition;
        var endPos = endCell.WorldPosition;
        var tokenCtrlStartRot = _tokenCtrl.localRotation;
        
        // Animate the token moving from one tile to another.
        var timer = 0f;
        while (timer < moveTime)
        {
            // Update timer
            timer += Time.deltaTime;
            var t = timer / moveTime;
            var tMid = Mathf.Sin(Mathf.Lerp(0f, Mathf.PI, t));  // 0 -> 1 -> 0
            
            // Calculate position based on time
            var tokenPos = Vector3.Lerp(startPos, endPos, t);
            var tokenCtrlPos = Vector3.up * tMid * PICK_UP_HEIGHT;
            var tokenCtrlRot = Quaternion.Euler(PICK_UP_ROTATION * tMid, 0f, 0f);
            
            // Set position
            transform.position = tokenPos;
            _tokenCtrl.localPosition = tokenCtrlPos;
            _tokenCtrl.localRotation = tokenCtrlStartRot * tokenCtrlRot;

            // Play the Put Down sound.
            if (t >= PUT_DOWN_SOUND_TIME && !playedPutDownSound)
            {
                PlayPutDownSound();
                playedPutDownSound = true;
            }

            yield return null;
        }

        // Snap position
        transform.position = endPos;
    }
}
