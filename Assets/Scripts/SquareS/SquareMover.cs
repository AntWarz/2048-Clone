using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;

public class SquareMover : MonoBehaviour
{
    public delegate void SpawnEvent();
    public static event SpawnEvent spawnEvent;

    private float _moveDuration = 0.2f;
    private void OnEnable()
    {
        PlayerInputHandler.OnMoveEvent += MoveSquare;
    }

    private void OnDisable()
    {
        PlayerInputHandler.OnMoveEvent -= MoveSquare;
    }

    private void MoveSquare(Vector2 moveDirection)
    {
        Vector2 moveAxis = moveDirection;
        (int xD, int yD) = ConvertMoveDirection(moveAxis);

        (bool, Vector2, (int, int), GameObject)[,] tileArray;
        List<(bool, Vector2, (int, int), GameObject)> allSquares;


        for (int i = 0; i < 4; i++)
        {
            tileArray = FieldInitializer.TileArray;
            allSquares = FieldScanner.GetAllOccupied(tileArray ,moveAxis);

            foreach (var tile in allSquares)
            {
                (int x, int y) = tile.Item3;
                if (x + xD < tileArray.GetLength(0) && x + xD >= 0 && y + yD < tileArray.GetLength(1) && y + yD >= 0)
                {
                    if (!tileArray[x + xD, y + yD].Item1)
                    {
                        FieldInitializer.UpdateTileArray((x + xD, y + yD), true, tile.Item4);
                        FieldInitializer.UpdateTileArray((x, y), false, null);

                        //tile.Item4.transform.position = tileArray[x + xD, y + yD].Item2;

                        Vector2 startPos = tile.Item4.transform.position;
                        Vector2 targetPos = tileArray[x + xD, y + yD].Item2;
                        StartCoroutine(MoveTileSmooth(tile.Item4, startPos, targetPos, _moveDuration));

                    } 
                }
            }
        }

        spawnEvent();
    }


    private (int, int) ConvertMoveDirection(Vector2 direction)
    {
        return (Mathf.RoundToInt(direction.x), Mathf.RoundToInt(direction.y));
    }

    private IEnumerator MoveTileSmooth(GameObject tile, Vector2 startPos, Vector2 targetPos, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Interpolate the position
            tile.transform.position = Vector2.Lerp(startPos, targetPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the tile reaches the exact target position
        tile.transform.position = targetPos;
    }
}
