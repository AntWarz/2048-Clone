using UnityEngine;

public static class FieldCalculator
{
    public static (bool, Vector2, (int, int), GameObject)[,] InitializeFieldArray(int tileSize, int squaresPerSide)
    {
        (bool, Vector2, (int, int), GameObject)[,] tileArray;
        tileArray = new (bool, Vector2, (int, int), GameObject)[squaresPerSide, squaresPerSide];

        int lowerLeft = -tileSize;
        int i = 0;
        int j = 0;
        for (int y = 0; y < squaresPerSide; y++)
        {
            for (int x = 0; x < squaresPerSide; x++)
            {
                Vector2 instPos = new Vector2(i + tileSize + lowerLeft, j + tileSize + lowerLeft);
                tileArray[x, y] = (false, instPos, (x, y), null);
                //Debug.Log($"Tile at index {tileArray[x, y].Item3} has position {tileArray[x, y].Item2}");

                i += tileSize;
            }
            i = 0;
            j += tileSize;
        }

        return tileArray;
    }
}
