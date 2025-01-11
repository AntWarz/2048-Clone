using UnityEngine;

public static class FieldInitializer
{
    public static (bool, Vector2)[,] InitializeFielArrays(int tileSize, int squaresPerSide)
    {
        (bool, Vector2)[,] tileArray;
        tileArray = new (bool, Vector2)[squaresPerSide, squaresPerSide];

        int lowerLeft = -tileSize;
        int i = 0;
        int j = 0;
        for (int y = 0; y < squaresPerSide; y++)
        {
            for (int x = 0; x < squaresPerSide; x++)
            {
                Vector2 instPos = new Vector2(i + tileSize + lowerLeft, j + tileSize + lowerLeft);
                tileArray[x, y] = (false, instPos);

                i += tileSize;
            }
            i = 0;
            j += tileSize;
        }

        return tileArray;
    }
}
