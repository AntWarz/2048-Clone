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

    public static bool IsFieldFull()
    {
        int squareCounter = 0;
        int surroundingCounter = 0;

        foreach (var tile in TileArrayHolder.TileArray)
        {
            if (tile.Item1)
            {
                squareCounter++;
                if (tile.Item4.GetComponent<Square>() != null && tile.Item4.GetComponent<Square>().HasSurroundingEqual)
                {
                    surroundingCounter++;
                }
            }
        }

        if (squareCounter == TileArrayHolder.TileArray.Length && surroundingCounter > 0) return false;
        else return true;
    }

    public static bool EqualsSurroundingSquare((int, int) arrayIndex, int value)
    {
        int arraySize = TileArrayHolder.ArraySize;
        bool hasSurroundingEqual = false;
        int x = arrayIndex.Item1;
        int y = arrayIndex.Item2;

        Square thisSquare = TileArrayHolder.TileArray[x, y].Item4.GetComponent<Square>();
        int thisValue = thisSquare.Value;

        int xPlus = x + 1;
        int yPlus = y + 1;
        int xMinus = x - 1;
        int yMinus = y - 1;

        bool isInBoundsUp = HelperFunctions.IndicesInBounds(x, yPlus, arraySize, 0, arraySize, 0);
        bool isInBoundsDown = HelperFunctions.IndicesInBounds(x, yMinus, arraySize, 0, arraySize, 0);
        bool isInBoundsRight = HelperFunctions.IndicesInBounds(xPlus, y, arraySize, 0, arraySize, 0);
        bool isInBoundsLeft = HelperFunctions.IndicesInBounds(xMinus, y, arraySize, 0, arraySize, 0);

        if (isInBoundsUp)
        {
            if (TileArrayHolder.TileArray[x, yPlus].Item4 != null)
            {
                Square squareUp = TileArrayHolder.TileArray[x, yPlus].Item4.GetComponent<Square>();
                int squareUpValue = squareUp.Value;
                Debug.Log($"Square above tile at {(x, y)} has value: {squareUpValue}");
                if (thisValue == squareUpValue)
                {
                    hasSurroundingEqual = true;
                }
            }
        }
        else if (isInBoundsDown)
        {
            if (TileArrayHolder.TileArray[x, yMinus].Item4 != null)
            {
                Square squareDown = TileArrayHolder.TileArray[x, yMinus].Item4.GetComponent<Square>();
                int squareDownValue = squareDown.Value;
                Debug.Log($"Square under tile at {(x, y)} has value: {squareDownValue}");
                if (thisValue == squareDownValue)
                {
                    hasSurroundingEqual = true;
                }
            }
        }
        else if (isInBoundsRight)
        {
            if (TileArrayHolder.TileArray[xPlus, y].Item4 != null)
            {
                Square squareRight = TileArrayHolder.TileArray[xPlus, y].Item4.GetComponent<Square>();
                int squareRightValue = squareRight.Value;
                Debug.Log($"Square right of tile at {(x, y)} has value: {squareRightValue}");
                if (thisValue == squareRightValue)
                {
                    hasSurroundingEqual = true;
                }
            }
        }
        else if (isInBoundsLeft)
        {
            if (TileArrayHolder.TileArray[xMinus, y].Item4 != null)
            {
                Square squareLeft = TileArrayHolder.TileArray[xMinus, y].Item4.GetComponent<Square>();
                int squareLeftValue = squareLeft.Value;
                Debug.Log($"Square left to tile at {(x, y)} has value: {squareLeftValue}");
                if (thisValue == squareLeftValue)
                {
                    hasSurroundingEqual = true;
                }
            }
        }

        return hasSurroundingEqual;
    }
}
