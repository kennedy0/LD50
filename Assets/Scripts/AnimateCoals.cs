using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCoals : MonoBehaviour
{
    public Color ColorA = new Color(1f, .2f, 0f);
    public Color ColorB = new Color(1f, .4f, 0f);
    private float _colorSpeed = 1f;
    private float _colorTimer = 0f;
    private int _mainColorId;

    private Material _material;
    
    void Start()
    {
        _material = GetComponent<Renderer>().material;
        _mainColorId = Shader.PropertyToID("_MainColor");
        _colorTimer = Random.Range(0f, 10f);

        transform.rotation = Random.rotation;
    }

    void Update()
    {
        AnimateColor();
    }

    void AnimateColor()
    {
        _colorTimer += Time.deltaTime;
        var t = Mathf.Sin(_colorTimer * _colorSpeed);
        t = (t + 1f) * .5f;
        Color newColor = Color.Lerp(ColorA, ColorB, t);
        _material.SetColor(_mainColorId, newColor);
    }
}
