using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class TextBox : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public float LetterTime = .05f;
    public float FastLetterTime = .02f;

    private const string HTML_ALPHA_TAG = "<color=#00000000>";

    public static TextBox Find()
    {
        return FindObjectOfType<TextBox>(includeInactive: true);
    }

    public void ClearText()
    {
        Text.text = "";
    }

    public IEnumerator DrawText(string text, bool autoNext = false, float autoNextTime = .5f)
    {
        // Initialize the text
        Text.text = "";

        // Slide the html alpha tag through each character to give it the appearance of typing.
        for (int i = 0; i <= text.Length; i++)
        {
            // If we are past the visible text box, strip the current text and start over
            if (i == Text.firstOverflowCharacterIndex)
            {
                // Wait for input
                if (autoNext)
                {
                    yield return new WaitForSecondsRealtime(autoNextTime);
                }
                else
                {
                    yield return WaitForConfirm();
                }
                
                text = text.Substring(Text.firstOverflowCharacterIndex);
                i = 0;
                continue;
            }

            // Reassign the text with the new alpha tag
            Text.text = text.Insert(i, HTML_ALPHA_TAG);
            
            // Pause before showing next letter
            float waitTime = ButtonIsPressed() ? FastLetterTime : LetterTime;
            yield return new WaitForSeconds(waitTime);
        }
        
        yield return WaitForConfirm();
    }

    private bool ButtonIsPressed()
    {
        return Input.anyKey || Input.GetMouseButton(0) || Input.GetMouseButton(1);
    }
    
    private bool ButtonIsPressedDown()
    {
        return Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1);
    }

    private IEnumerator WaitForConfirm()
    {
        while (!ButtonIsPressedDown())
        {
            yield return null;
        }
    }
}
