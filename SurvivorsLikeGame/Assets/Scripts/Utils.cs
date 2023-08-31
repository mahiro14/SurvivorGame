using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static string GetTextTimer(float timer)
    {
        int seconds = (int)timer % 60;
        int minutes = (int)timer / 60;
        return minutes.ToString() + ":" + seconds.ToString("00");
    }

    public static bool IsCollisionTile(Tilemap tilemapCollision, Vector2 position)
    {
        Vector3Int cellPosition = tilemapCollision.WorldToCell(position);

        if (tilemapCollision.GetTile(cellPosition))
        {
            return true;
        }

        return false;
    }
}
