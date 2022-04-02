using System;
using UnityEngine;

public enum Direction
{
    North,
    South,
    NorthWest,
    NorthEast,
    SouthWest,
    SouthEast,
}

public static class Utilities
{
    public const float GRID_SPACING_X = .75f;
    public const float GRID_SPACING_Y = 6f/7f;
    
    /// <summary>
    /// Convert a grid position to a world position.
    /// </summary>
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

    /// <summary>
    /// Return the grid position in a given direction.
    /// </summary>
    public static Vector2Int TranslatePosition(int gx, int gy, Direction direction)
    {
        switch (direction)
        {
            case Direction.North:
                return new Vector2Int(gx, gy+1);
            case Direction.South:
                return new Vector2Int(gx, gy-1);
            case Direction.NorthWest:
                if (gx % 2 == 0)
                {
                    return new Vector2Int(gx-1, gy+1);
                }
                else
                {
                    return new Vector2Int(gx-1, gy);
                }
            case Direction.NorthEast:
                if (gx % 2 == 0)
                {
                    return new Vector2Int(gx+1, gy+1);
                }
                else
                {
                    return new Vector2Int(gx+1, gy);
                }
            case Direction.SouthWest:
                if (gx % 2 == 0)
                {
                    return new Vector2Int(gx-1, gy);
                }
                else
                {
                    return new Vector2Int(gx-1, gy-1);
                }
            case Direction.SouthEast:
                if (gx % 2 == 0)
                {
                    return new Vector2Int(gx+1, gy);
                }
                else
                {
                    return new Vector2Int(gx+1, gy-1);
                }
        }

        throw new Exception("Couldn't get neighbor position!");
    }
}
