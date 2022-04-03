using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HexTile : MonoBehaviour
{
    [Header("Grid")]
    public int X;
    public int Y;

    private Transform _hexCtrl;
    private AnimateHex _animateHex;

    void Awake()
    {
        _hexCtrl = transform.Find("hex_ctrl");
        _animateHex = _hexCtrl.GetComponent<AnimateHex>();
        
        _hexCtrl.gameObject.SetActive(false);
    }

    /// <summary>
    /// Initialize the tile.
    /// </summary>
    public void InitTile(float creationDelay)
    {
        // Set random rotation
        _hexCtrl.localRotation = Utilities.RotationFromDirection(Utilities.RandomDirection());
        StartCoroutine(RevealTile(creationDelay));
    }

    // This handles the animation and visual setup the first time the tile is revealed.
    private IEnumerator RevealTile(float creationDelay)
    {
        yield return new WaitForSeconds(creationDelay);
        _hexCtrl.gameObject.SetActive(true);
        _animateHex.PlayFlipAnimation();
    }
}
