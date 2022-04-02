using UnityEngine;

public static class Utilities
{
    public const float GRID_SPACING_X = .75f;
    public const float GRID_SPACING_Y = 6f/7f;
    
    public static Vector3 GridToWorldPosition(int gx, int gy)
    {
        float wx;
        float wy;

        wx = gx * GRID_SPACING_X;
        wy = gy * GRID_SPACING_Y;

        if (gx % 2 == 0)
        {
            wy += GRID_SPACING_Y / 2f;
        }

        return new Vector3(wx, 0f, wy);
    }
}
