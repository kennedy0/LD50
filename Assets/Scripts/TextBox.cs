using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{
    public float TextBoxFadeTime = .5f;
    public Image BgImage;
    public TextMeshProUGUI Text;
    public float LetterTime = .05f;
    public float FastLetterTime = .02f;

    private const string HTML_ALPHA_TAG = "<color=#00000000>";

    public static TextBox Find()
    {
        return FindObjectOfType<TextBox>(includeInactive: true);
    }

    public IEnumerator ShowTextBox(string text)
    {
        Text.CrossFadeAlpha(0f, 0f, true);
        BgImage.CrossFadeAlpha(0f, 0f, true);
        yield return Show();
        yield return DrawText(text);
        yield return Hide();
    }

    private IEnumerator Show()
    {
        Text.CrossFadeAlpha(1f, TextBoxFadeTime, false);
        BgImage.CrossFadeAlpha(1f, TextBoxFadeTime, false);
        yield return new WaitForSeconds(TextBoxFadeTime);
    }

    private IEnumerator Hide()
    {
        Text.CrossFadeAlpha(0, TextBoxFadeTime, false);
        BgImage.CrossFadeAlpha(0, TextBoxFadeTime, false);
        yield return new WaitForSeconds(TextBoxFadeTime);
        ClearText();
    }

    private IEnumerator DrawText(string text, bool autoNext = false, float autoNextTime = .5f)
    {
        // Initialize the text
        ClearText();

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
            SoundEffects.SoundEffectsMaster.PlayWoodQuiet();
            
            // Pause before showing next letter
            float waitTime = ButtonIsPressed() ? FastLetterTime : LetterTime;
            yield return new WaitForSeconds(waitTime);
        }
        
        yield return WaitForConfirm();
    }

    private void ClearText()
    {
        Text.text = "";
    }

    private IEnumerator WaitForConfirm()
    {
        while (!ButtonIsPressedDown())
        {
            yield return null;
        }
    }

    private bool ButtonIsPressed()
    {
        return Input.anyKey || Input.GetMouseButton(0) || Input.GetMouseButton(1);
    }

    private bool ButtonIsPressedDown()
    {
        return Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1);
    }
}
