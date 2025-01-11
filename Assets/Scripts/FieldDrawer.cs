using UnityEngine;

public class FieldDrawer : MonoBehaviour
{
    [SerializeField] private GameObject _whiteTile;
    [SerializeField] private GameObject _greyTile;
    [SerializeField] private int _squaresPerSide;
    [SerializeField] private int _tileSize;


    private int _squareCount = 0;

    private void Start()
    {
        DrawField();
    }

    private void DrawField()
    {
        (bool, Vector2)[,] tileArray = FieldInitializer.InitializeFielArrays(_tileSize, _squaresPerSide);
        for (int y = 0; y < _squaresPerSide; y++)
        {
            for (int x = 0; x < _squaresPerSide; x++)
            {
                Vector2 instPos = tileArray[x, y].Item2;
                if (y % 2 == 0 && x % 2 == 0 || !(x % 2 == 0) && !(y % 2 == 0))
                {
                    Instantiate(_whiteTile, instPos, Quaternion.identity, this.transform);
                }
                else if (!(x % 2 == 0) && y % 2 == 0 || x % 2 == 0 && !(y % 2 == 0))
                {
                    Instantiate(_greyTile, instPos, Quaternion.identity, this.transform);
                }
            }
        }
    }
}
