using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

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

    private static System.Random _random = new System.Random();
    
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

    /// <summary>
    /// Get a rotation angle from a direction
    /// </summary>
    public static Quaternion RotationFromDirection(Direction direction)
    {
        float rot = 0f;
        
        switch (direction)
        {
            case Direction.North:
                rot = 0f;
                break;
            case Direction.South:
                rot = 180f;
                break;
            case Direction.NorthWest:
                rot = -60f;
                break;
            case Direction.NorthEast:
                rot = 60f;
                break;
            case Direction.SouthWest:
                rot = -120f;
                break;
            case Direction.SouthEast:
                rot = 120f;
                break;
        }
        
        return Quaternion.Euler(0f, rot, 0f);
    }

    /// <summary>
    /// Get a list of neighbor coordinates.
    /// </summary>
    public static List<Vector2Int> NeighborCoordinates(int gx, int gy)
    {
        List<Vector2Int> coords = new List<Vector2Int>();
        coords.Add(TranslatePosition(gx, gy, Direction.North));
        coords.Add(TranslatePosition(gx, gy, Direction.South));
        coords.Add(TranslatePosition(gx, gy, Direction.NorthWest));
        coords.Add(TranslatePosition(gx, gy, Direction.NorthEast));
        coords.Add(TranslatePosition(gx, gy, Direction.SouthWest));
        coords.Add(TranslatePosition(gx, gy, Direction.SouthEast));
        return coords;
    }

    /// <summary>
    /// Returns a random Direction.
    /// </summary>
    public static Direction RandomDirection()
    {
        var directionList = Enum.GetValues(typeof(Direction))
            .Cast<Direction>()
            .ToList();
        var index = _random.Next(directionList.Count);
        return directionList[index];
    }
}
