using UnityEngine;

public class FieldDrawer : MonoBehaviour
{
    [SerializeField] private GameObject _whiteTile;
    [SerializeField] private GameObject _greyTile;

    private void Start()
    {
        DrawField();
    }

    private void DrawField()
    {
        (bool, Vector2, (int, int), GameObject)[,] tileArray = FieldInitializer.TileArray;
        for (int y = 0; y < tileArray.GetLength(0); y++)
        {
            for (int x = 0; x < tileArray.GetLength(0); x++)
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
