using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;
using UnityEditor;
using UnityEditor.Build.Content;

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


        tileArray = TileArrayHolder.TileArray;
        allSquares = FieldScanner.GetAllOccupied(tileArray ,moveAxis);

        bool moving = true;
        bool fieldHasChanged = false;
        
        foreach (var tile in allSquares)
        {
            (int x, int y) = tile.Item3;
            moving = true;
            while (moving)
            {
                if (IndicesInBounds(x + xD, y + yD, tileArray.GetLength(0), 0, tileArray.GetLength(1), 0) && tile.Item4 != null)
                {
                    if (!tileArray[x + xD, y + yD].Item1)
                    {
                        //Register square at the next tile position
                        TileArrayHolder.UpdateTileArray((x + xD, y + yD), true, tile.Item4);

                        //Remove square from current tile position
                        TileArrayHolder.UpdateTileArray((x, y), false, null);


                        UpdateTilePosition(tile, tileArray, x + xD, y + yD);

                        (x, y) = (x + xD, y + yD);
                        fieldHasChanged = true;

                    }
                    else if (tileArray[x + xD, y + yD].Item1)
                    {

                        //Get the values of the square on the next tile and the current square
                        int nextValue = tileArray[x + xD, y + yD].Item4.GetComponent<Square>().Value;
                        int thisValue = tileArray[x, y].Item4.GetComponent<Square>().Value;
                        Debug.Log($"Next tile at {(x + xD, y + yD)}has value {nextValue}");

                        if (nextValue == thisValue)
                        {
                            Destroy(tileArray[x + xD, y + yD].Item4);
                            tile.Item4.GetComponent<Square>().Value = thisValue * 2;

                            //Setting current square to be on the next tile
                            TileArrayHolder.UpdateTileArray((x + xD, y + yD), true, tile.Item4);

                            //Remove current square from current tile
                            TileArrayHolder.UpdateTileArray((x, y), false, null);

                            UpdateTilePosition(tile, tileArray, x + xD, y + yD);
                            fieldHasChanged = true;
                        } 
                        moving = false;
                    }
                } else
                {
                    moving = false;
                }
            }
        }
        //Fire event to spawn a new tile 
        if (fieldHasChanged)
        {
            spawnEvent();
        }
    }


    private (int, int) ConvertMoveDirection(Vector2 direction)
    {
        return (Mathf.RoundToInt(direction.x), Mathf.RoundToInt(direction.y));
    }
    

    private IEnumerator MoveTileSmooth(GameObject square, Vector2 startPos, Vector2 targetPos, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            //Squares can be deleted while movement is not complete
            //Checking that square is not null is necessary to avoid a Missing refeference exception
            if (square == null)
            {
                Debug.Log("Square was destroyed during movement.");
                yield break; 
            }
            square.transform.position = Vector2.Lerp(startPos, targetPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;

            yield return null;

        }

        if (square != null)
        {
            square.transform.position = targetPos;
        }
    }

    private void UpdateTilePosition((bool, Vector2, (int, int), GameObject) tile, (bool, Vector2, (int, int), GameObject)[,] tileArray, int x, int y)
    {
        Vector2 startPos = tile.Item4.transform.position;
        Vector2 targetPos = tileArray[x, y].Item2;

        //Move the square
        StartCoroutine(MoveTileSmooth(tile.Item4, startPos, targetPos, _moveDuration));
    }

    private bool IndicesInBounds(int x, int y, int boundXUpper, int boundXLower, int boundYUpper, int boundYLower)
    {
        if (x < boundXUpper && y < boundYUpper && x >= boundXLower && y >= boundYLower) return true;

        else return false;

    }
}
