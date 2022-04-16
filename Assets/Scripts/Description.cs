using System.Collections;
using TMPro;
using UnityEngine;

public class Description : MonoBehaviour
{
    public string DescriptionText;
    
    private TextBox _textBox;

    private void Awake()
    {
        _textBox = TextBox.Find();
    }

    // Show Text Box.
    public IEnumerator Describe()
    {
        // ToDo: All of this...
        SoundEffects.SoundEffectsMaster.PlayNote();
        yield return _textBox.DrawText(DescriptionText);
    }

}