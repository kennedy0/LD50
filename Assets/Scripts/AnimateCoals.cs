using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCoals : MonoBehaviour
{
    public Color ColorA;
    public Color ColorB;
    public float ColorSpeed = 1f;
    public float ColorTimerOffset = 0f;
    
    private float _colorTimer = 0f;
    private int _mainColorId;

    private Material _material;
    
    void Start()
    {
        _material = GetComponent<Renderer>().material;
        _mainColorId = Shader.PropertyToID("_MainColor");
        _colorTimer = ColorTimerOffset;
    }

    void Update()
    {
        AnimateColor();
    }

    void AnimateColor()
    {
        _colorTimer += Time.deltaTime;
        var t = Mathf.Sin(_colorTimer * ColorSpeed);
        t = (t + 1f) * .5f;
        Color newColor = Color.Lerp(ColorA, ColorB, t);
        _material.SetColor(_mainColorId, newColor);
    }
}
