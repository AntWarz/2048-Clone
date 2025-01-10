using System.Collections.Generic;
using UnityEngine;

public class PlayingFieldInitializer : MonoBehaviour
{
    [SerializeField] private int _playingFieldSize;
    [SerializeField] private GameObject _whiteTile;
    [SerializeField] private GameObject _greyTile;

    [SerializeField] private GameObject _startingSquare;

    public Dictionary<Vector2, bool> fieldDict = new Dictionary<Vector2, bool>();

    private int _tileSize;

    private void Start()
    {
        _tileSize = _playingFieldSize / 4;
        InitializePlayingfield();
    }

    private void InitializePlayingfield()
    {
        int firstTileX = -_playingFieldSize / 2;
        int firstTileY = -_playingFieldSize / 2;

        int squareCount = 0;

        for (int y = 0; y < _playingFieldSize; y += _tileSize)
        {
            for (int x = 0; x < _playingFieldSize; x += _tileSize)
            {
                Vector2 tilePos = new Vector2 (firstTileX + x, firstTileY + y);
                fieldDict[tilePos] = false;
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
                    GameObject newTile = Instantiate(_startingSquare, new Vector2(tilePos[0], tilePos[1]), Quaternion.identity, this.transform);
                    newTile.GetComponent<SquareMovement>().squarePos[0] = tilePos.x;
                    newTile.GetComponent<SquareMovement>().squarePos[1] = tilePos[1];
                }
            }
        }
    }
}
