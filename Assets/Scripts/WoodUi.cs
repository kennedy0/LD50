using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodUi : MonoBehaviour
{
    // ToDo - Temp! All of this is temp.

    private RectTransform _rectTransform;
    private float _timer;
    private float _visibleTime = 3f;

    private Vector3 _showPosition = Vector3.zero;
    private Vector3 _hidePosition = Vector3.left * 300f;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.localPosition = _hidePosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer > 0f)
        {
            _timer -= Time.deltaTime;
        }

        if (_timer < 0f)
        {
            _timer = 0f;
        }

        _rectTransform.localPosition = Vector3.Lerp(_hidePosition, _showPosition, _timer);
    }

    public void ShowUi()
    {
        _timer = _visibleTime;
    }
}
