using System.Collections.Generic;
using Datatypes;
using UnityEngine;

public static class BoardGen
{
    /// <summary>
    /// Generate a height value for a cell.
    /// </summary>
    public static int GenerateHeight(HexCell cell)
    {
        return 0;
    }

    /// <summary>
    /// Generate a token to appear on a cell.
    /// </summary>
    public static void GenerateToken(HexCell cell)
    {
        // Don't place token on an occupied cell.
        if (cell.Occupied)
        {
            return;
        }

        // // Don't place token close to the starting location
        if (Hex.Distance(cell.Position, Hex.Zero) < 5)
        {
            return;
        }

        // Random chance to create wood.
        var chance = 20;
        if (Random.Range(0, chance) == 0)
        {
            var actor = TokenManager.CreateWood(cell);
            
            // Set random rotation
            var tokenCtrl = actor.transform.Find("token_ctrl");
            var rotations = new List<float> { 0f, 60f, 120f, 180f, 240f, 300f};
            var randomRotation = rotations[Random.Range(0, 6)];
            tokenCtrl.localRotation = Quaternion.Euler(0f, randomRotation, 0f);
        }
    }
}
