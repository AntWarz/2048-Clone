using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class SquareMover : MonoBehaviour
{

    private InputAction _moveAction;

    private void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
    }
    private void Update()
    {
        if (_moveAction.WasPressedThisFrame())
        {
            MoveSquare();
        }
    }

    private void MoveSquare()
    {
        (bool, Vector2, (int, int), GameObject)[,] tileArray = FieldInitializer.TileArray;
        List<(bool, Vector2, (int, int), GameObject)> allSquares = new List<(bool, Vector2, (int, int), GameObject)>();
        Vector2 moveAxis = _moveAction.ReadValue<Vector2>();
        (int xD, int yD) = ConvertMoveDirection(moveAxis);
        allSquares = FieldScanner.GetAllOccupied(tileArray ,moveAxis);


        foreach (var tile in allSquares)
        {
            (int x, int y) = tile.Item3;
            Debug.Log($"Current array Position: {(x, y)} with world position: {tile.Item2}");
            Debug.Log($"Converted Moving direction: {(xD, yD)}");
            Debug.Log($"New index: {(x + xD, y + yD)} results in new world position: {tile.Item2}");
            if (x + xD < tileArray.GetLength(0) && x + xD >= 0 && y + yD < tileArray.GetLength(1) && y + yD >= 0)
            {
                if (!tileArray[x + xD, y + yD].Item1)
                {
                    tile.Item4.transform.position = tileArray[x + xD, y + yD].Item2;
                    tileArray[x, y].Item1 = false;
                    tileArray[x + xD, y + yD].Item1 = true;
                    tileArray[x + xD, y + yD].Item4 = tile.Item4;
                    tileArray[x, y].Item4 = null;
                }
            }
        }
    }


    private (int, int) ConvertMoveDirection(Vector2 direction)
    {

        return (Mathf.RoundToInt(direction.x), Mathf.RoundToInt(direction.y));

    }
}
