using System.Collections.Generic;
using UnityEngine;

public static class FieldScanner
{
    public static void GetFreeTiles(int squaresPerSide, (bool, Vector2)[,] tileArray)
    {
        Dictionary<int, int> freeTilesX = new Dictionary<int, int>();
        Dictionary<int, int> freeTilesY = new Dictionary<int, int>();

        for (int y = 0; y < squaresPerSide; y++)
        {
            int countX = 0;
            for (int x = 0; x < squaresPerSide; x++)
            {
                if (!tileArray[x, y].Item1)
                {
                    countX++;
                    freeTilesY[y] = countX;
                }

            }
        }

        for (int x = 0; x < squaresPerSide; x++)
        {
            int countY = 0;
            for (int y = 0; y < squaresPerSide; y++)
            {
                if (!tileArray[x, y].Item1)
                {
                    countY++;
                    freeTilesX[x] = countY;
                }

            }
        }
    }
}
