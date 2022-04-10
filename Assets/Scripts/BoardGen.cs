using UnityEngine;

public static class BoardGen
{
    /// <summary>
    /// Generate a terrain value for a cell.
    /// </summary>
    public static Terrain GenerateTerrain(HexCell cell)
    {
        return Terrain.Forest;
    }

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
        if (Random.Range(0, 10) == 0)
        {
            // TokenManager.CreateWood(cell);
        }
    }
}
