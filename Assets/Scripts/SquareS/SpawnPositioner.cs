using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public static class SpawnPositioner 
{
    public delegate void GameOverEvent();
    public static event GameOverEvent gameOver;

    //Returns a random free tile position on the playing field
    public static (Vector2, (int, int)) GetRandomFreeTile((bool, Vector2, (int, int), GameObject)[,] tileArray)
    {
        List<(Vector2, (int, int))> freeTiles = new List<(Vector2, (int, int))>();
        //Get all tiles were occupied is false
        foreach (var tile in tileArray)
        {
            if (!tile.Item1)
            {

                freeTiles.Add((tile.Item2, tile.Item3));
            }
        }
        if (freeTiles.Count > 0)
        {
            int randIndex = Random.Range(0, freeTiles.Count);
            return freeTiles[randIndex];
        } /*else
        {
            gameOver();
        }*/

        return (Vector2.zero, (0,0));
    }
}
