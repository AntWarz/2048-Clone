using System.Collections.Generic;
using UnityEngine;

public static class FieldScanner
{
    public static List<(bool, Vector2, (int, int), GameObject)> GetAllOccupied((bool, Vector2, (int, int), GameObject)[,] tileArray, Vector2 direction)
    {
        List<(bool, Vector2, (int, int), GameObject)> occupiedTiles = new List<(bool, Vector2, (int, int), GameObject)>();
        //Debug.Log($"Direction parameter: {direction}");
        //Get all tiles were occupied is true

        //Tile array is created from left to right and bottom to top. 
        //This means that for downwards movement, I can later loop through the collection without modification
        //For upward movement I need to loop through the reversed collection


        //for down movement
        if (direction == Vector2.down)
        {
            for (int y = 0; y <  tileArray.GetLength(1); y++)
            {
                for (int x = 0; x < tileArray.GetLength(0); x++)
                {
                    if (tileArray[x, y].Item1)
                    {
                        //Debug.Log($"Index: {(x, y)}, array Pos: {tileArray[x, y].Item3}");
                        occupiedTiles.Add(tileArray[x, y]);
                    }
                }
            }
        }

        //for up movement
        if (direction == Vector2.up)
        {
            //Debug.Log("Reached the loop for up movement");
            for (int y = tileArray.GetLength(1) - 1; y >= 0 ; y--)
            {
                for (int x = tileArray.GetLength(0) - 1; x >= 0 ; x--)
                {
                    if (tileArray[x, y].Item1)
                    {
                        //Debug.Log($"Index: {(x, y)}, array Pos: {tileArray[x, y].Item3}");
                        occupiedTiles.Add(tileArray[x, y]);
                    }
                }
            }
        }

        //for left movement 
        if (direction == Vector2.left)
        {
            for (int x = 0; x < tileArray.GetLength(0); x++)
            {
                for (int y = 0; y < tileArray.GetLength(1); y++)
                {
                    if (tileArray[x, y].Item1)
                    {
                        //Debug.Log($"Index: {(x, y)}, array Pos: {tileArray[x, y].Item3}");
                        occupiedTiles.Add(tileArray[x, y]);
                    }
                }
            }
        }

        //for right movement
        if (direction == Vector2.right)
        {
            for (int x = tileArray.GetLength(0) - 1; x >= 0; x--)
            {
                for (int y = tileArray.GetLength(1) - 1; y >= 0 ; y--)
                {
                    if (tileArray[x, y].Item1)
                    {
                        //Debug.Log($"Index: {(x, y)}, array Pos: {tileArray[x, y].Item3}");
                        occupiedTiles.Add(tileArray[x, y]);
                    }
                }
            }
        }

        return occupiedTiles;
    }
}
