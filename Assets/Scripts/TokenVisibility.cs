using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenVisibility : MonoBehaviour
{
    private Token _token;
    private Transform _tokenCtrl;

    private void Awake()
    {
        _token = GetComponent<Token>();
        _tokenCtrl = transform.Find("token_ctrl");
    }

    private void Start()
    {
        Hide();
    }

    /// <summary>
    /// Update the visibility on the tile based on the cell's visibility value.
    /// </summary>
    public IEnumerator UpdateVisibility()
    {
        if (_token.Actor.IsVisible)
        {
            Show();
        }
        else
        {
            Hide();
        }

        yield return null;
    }

    private void Hide()
    {
        _tokenCtrl.gameObject.SetActive(false);
    }

    private void Show()
    {
        _tokenCtrl.gameObject.SetActive(true);
    }
}
