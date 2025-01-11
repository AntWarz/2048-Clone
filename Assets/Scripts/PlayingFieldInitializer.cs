using System.Collections.Generic;
using UnityEngine;

public class PlayingFieldInitializer : MonoBehaviour
{
    [SerializeField] private int _playingFieldSize;
    [SerializeField] private int squaresPerSide;
    [SerializeField] private GameObject _whiteTile;
    [SerializeField] private GameObject _greyTile;

    [SerializeField] private GameObject _startingSquare;

    public bool[,] tileArrayBool;
    public Vector2[,] tileArrayVec;

    public Dictionary<int, int> freeTilesX = new Dictionary<int, int>();
    public Dictionary<int, int> freeTilesY = new Dictionary<int, int>();

    private int _tileSize;

    private void Start()
    {
        //Setting tile size
        _tileSize = _playingFieldSize / 4;

        //Initializing the two tile arrays
        tileArrayBool = new bool[squaresPerSide, squaresPerSide];
        tileArrayVec = new Vector2[squaresPerSide, squaresPerSide];

        //Initialize the playing field
        InitializeNew();
    }

    //Initialization of playing field and drawing of playing field and spawning moveable squares
    private void InitializeNew()
    {
        int squareCount = 0;
        int lowerLeft = -_tileSize;
        int i = 0;
        int j = 0;
        for (int y = 0; y < squaresPerSide; y++)
        {
            for (int x = 0; x < squaresPerSide; x++)
            {
                Vector2 instPos = new Vector2(i + _tileSize + lowerLeft, j + _tileSize + lowerLeft);
                if (y % 2 == 0 && x % 2 == 0 || !(x % 2 == 0) && !(y % 2 == 0))
                {
                    Instantiate(_whiteTile, instPos, Quaternion.identity, this.transform);
                }
                else if (!(x % 2 == 0) && y % 2 == 0 || x % 2 == 0 && !(y % 2 == 0))
                {
                    Instantiate(_greyTile, instPos, Quaternion.identity, this.transform);
                }
                tileArrayBool[x, y] = false;
                tileArrayVec[x, y] = instPos;


/*                if (y == 0 && x == 0 || y == 0 && x == 1)
                {
                    squareCount++;
                    GameObject square = Instantiate(_startingSquare, instPos, Quaternion.identity, this.transform);
                    square.GetComponent<SquareMovement>().arrayPos.Add((x, y));
                    tileArrayBool[x, y] = true;
                }*/

                float r = Random.Range(0f, 1f);
                if (r >= 0.8f && squareCount < 2)
                {
                    squareCount++;
                    GameObject square = Instantiate(_startingSquare, instPos, Quaternion.identity, this.transform);
                    square.GetComponent<SquareMovement>().arrayPos.Add((x, y));
                    tileArrayBool[x, y] = true;
                }

                i += _tileSize;
            }
            i = 0;
            j += _tileSize;
        }
    }

    //Retrieving free tiles for each axis and filling of free tile arrays
    public void GetFreeTiles()
    {
        for (int y = 0; y < squaresPerSide; y++)
        {
            int countX = 0;
            for (int x = 0; x < squaresPerSide; x++)
            {
                if (!tileArrayBool[x, y])
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
                if (!tileArrayBool[x, y])
                {
                    countY++;
                    freeTilesX[x] = countY;
                }

            }
        }
    }
}
