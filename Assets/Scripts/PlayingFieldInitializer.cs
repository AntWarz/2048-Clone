using System.Collections.Generic;
using UnityEngine;

public class PlayingFieldInitializer : MonoBehaviour
{
    [SerializeField] private int _playingFieldSize;
    [SerializeField] private int squaresPerSide;
    [SerializeField] private GameObject _whiteTile;
    [SerializeField] private GameObject _greyTile;

    [SerializeField] private GameObject _startingSquare;

    public Dictionary<Vector2, bool> fieldDict = new Dictionary<Vector2, bool>();
    public bool[,] tileArrayBool;
    public Vector2[,] tileArrayVec;
    public Dictionary<int, int> freeTilesX = new Dictionary<int, int>();
    public Dictionary<int, int> freeTilesY = new Dictionary<int, int>();

    private int _tileSize;

    private void Start()
    {
        _tileSize = _playingFieldSize / 4;
        tileArrayBool = new bool[_playingFieldSize, _playingFieldSize];
        tileArrayVec = new Vector2[squaresPerSide, squaresPerSide];
        //InitializePlayingfield();
        InitializeNew();
    }

    private void Update()
    {
        GetFreeTiles();
    }

    private void InitializePlayingfield()
    {
        int firstTileX = -_playingFieldSize / 2;
        int firstTileY = -_playingFieldSize / 2;

        int squareCount = 0;

        int i = 0;  
        int j = 0;
        for (int y = 0; y < _playingFieldSize; y += _tileSize)
        {
            for (int x = 0; x < _playingFieldSize; x += _tileSize)
            {
                Vector2 tilePos = new Vector2 (firstTileX + x, firstTileY + y);
                fieldDict[tilePos] = false;
                tileArrayBool[j, i] = false;
                //Debug.Log($"Tile at {tilePos[0]}, {tilePos[1]}");
                if (y % 2 == 0 && x % 2 == 0 || !(x % 2 == 0) && !(y % 2 == 0))
                {
                    Instantiate(_whiteTile, new Vector2(tilePos[0], tilePos[1]), Quaternion.identity, this.transform);
                } else if (!(x % 2 == 0) && y % 2 == 0 || x % 2 == 0 && !(y % 2 ==0))
                {
                    Instantiate(_greyTile, new Vector2(tilePos[0], tilePos[1]), Quaternion.identity, this.transform);
                }

                float r = Random.Range(0f, 1f);
                if (r >= 0.8f && squareCount < 2)
                {
                    squareCount++;
                    fieldDict[tilePos] = true;
                    tileArrayBool[j, i] = true;
                    GameObject newTile = Instantiate(_startingSquare, new Vector2(tilePos[0], tilePos[1]), Quaternion.identity, this.transform);
                    newTile.GetComponent<SquareMovement>().squarePos[0] = tilePos.x;
                    newTile.GetComponent<SquareMovement>().squarePos[1] = tilePos.y;

                    newTile.GetComponent<SquareMovement>().arrayPos.Add((x, y));
                }
                j++;
            }
            j = 0;
            i++;
        }
    }


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
