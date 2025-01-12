using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public static class SpawnPositioner 
{
    //Returns a random free tile position on the playing field
    public static (Vector2, (int, int)) GetRandomFreeTile((bool, Vector2, (int, int), GameObject)[,] tileArray)
    {
        List<(Vector2, (int, int))> freeTiles = new List<(Vector2, (int, int))>();
        //Get all tiles were occupied is false
        foreach (var tile in tileArray)
        {
            //Debug.Log($"Tile at Index: {tile.Item3} has position {tile.Item2}");
            if (!tile.Item1)
            {

                freeTiles.Add((tile.Item2, tile.Item3));
            }
        }

        int randIndex = Random.Range(0, freeTiles.Count);
        return freeTiles[randIndex];
    }
}
