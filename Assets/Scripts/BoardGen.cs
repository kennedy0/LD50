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
        if (cell.Actor != null)
        {
            return;
        }

        // Random chance to create wood.
        if (Random.Range(0, 50) == 0)
        {
            TokenManager.CreateWood(cell);
        }
    }
}
