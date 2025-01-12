using NUnit.Framework;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class SquareMovement : MonoBehaviour
{
    public Vector2 squarePos = new Vector2();
    public List<(int, int)> arrayPos = new List<(int, int)>();

    private InputAction _moveAction;

    private bool _moving = false;
    private Vector2 _moveAxis = new Vector2();

    private int repeatCount = 0;

    private PlayingFieldInitializer _fieldInitializer;

    private void Start()
    {
        //Find the input action
        _moveAction = InputSystem.actions.FindAction("Move");

        //Get an instance of PlayingFieldInitializer
        //_fieldInitializer = FindFirstObjectByType<PlayingFieldInitializer>();

        //Call method in field initializer to fill up arrays of free tiles
        //_fieldInitializer.GetFreeTiles();
    }

    private void Update()
    {
        //React to player input
        GetInputAction();

        //Move the square until it can not anymore
        if (_moving)
        {
            repeatCount++;
            MoveSquare();
            Debug.Log("Moving");
        }
    }



    private void MoveSquare()
    {

        //Get the free tiles for every axis by calling method in fieldinitializer
        int freeY = _fieldInitializer.freeTilesY[arrayPos[0].Item2];
        int freeX = _fieldInitializer.freeTilesY[arrayPos[0].Item1];

        if (_moveAxis == new Vector2(1, 0) || _moveAxis == new Vector2(-1, 0))
        {
            if (_moveAxis.x > 0)
            {
                //Check if index is in bound, there are free tiles for this X axis and that the next tile is free
                if (arrayPos[0].Item1 + 1 < 4 && freeX > 0 && !_fieldInitializer.tileArrayBool[arrayPos[0].Item1 + 1, arrayPos[0].Item2])
                {
                    _fieldInitializer.tileArrayBool[arrayPos[0].Item1, arrayPos[0].Item2] = false;

                    arrayPos[0] = (arrayPos[0].Item1 + 1, arrayPos[0].Item2);
                    this.transform.position = _fieldInitializer.tileArrayVec[arrayPos[0].Item1, arrayPos[0].Item2];
                    
                    _fieldInitializer.tileArrayBool[arrayPos[0].Item1, arrayPos[0].Item2] = true;
                } else if (repeatCount >= 2)
                {
                    _moving = false;
                    repeatCount = 0;
                }
            }
            else if (_moveAxis.x < 0)
            {
                if (arrayPos[0].Item1 - 1 >= 0 && freeX > 0 && !_fieldInitializer.tileArrayBool[arrayPos[0].Item1 - 1, arrayPos[0].Item2])
                {
                    _fieldInitializer.tileArrayBool[arrayPos[0].Item1, arrayPos[0].Item2] = false;
                    
                    arrayPos[0] = (arrayPos[0].Item1 - 1, arrayPos[0].Item2);
                    this.transform.position = _fieldInitializer.tileArrayVec[arrayPos[0].Item1, arrayPos[0].Item2];
                    
                    _fieldInitializer.tileArrayBool[arrayPos[0].Item1, arrayPos[0].Item2] = true;
                }
                else if (repeatCount >= 2)
                {
                    _moving = false;
                    repeatCount = 0;
                }
            }

        } else if (_moveAxis == new Vector2(0, 1) || _moveAxis == new Vector2(0, -1))
        {
            if (_moveAxis.y > 0)
            {
                if (arrayPos[0].Item2 + 1 < 4 && freeY > 0 && !_fieldInitializer.tileArrayBool[arrayPos[0].Item1, arrayPos[0].Item2 + 1])
                {
                    _fieldInitializer.tileArrayBool[arrayPos[0].Item1, arrayPos[0].Item2] = false;
                    
                    arrayPos[0] = (arrayPos[0].Item1, arrayPos[0].Item2 + 1);
                    this.transform.position = _fieldInitializer.tileArrayVec[arrayPos[0].Item1, arrayPos[0].Item2];
                    
                    _fieldInitializer.tileArrayBool[arrayPos[0].Item1, arrayPos[0].Item2] = true;
                }
                else if (repeatCount >= 2)
                {
                    _moving = false;
                    repeatCount = 0;
                }
            }
            else if (_moveAxis.y < 0)
            {
                if (arrayPos[0].Item2 - 1 >= 0 && freeY > 0 && !_fieldInitializer.tileArrayBool[arrayPos[0].Item1, arrayPos[0].Item2 - 1])
                {
                    _fieldInitializer.tileArrayBool[arrayPos[0].Item1, arrayPos[0].Item2] = false;
                    
                    arrayPos[0] = (arrayPos[0].Item1, arrayPos[0].Item2 - 1);
                    this.transform.position = _fieldInitializer.tileArrayVec[arrayPos[0].Item1, arrayPos[0].Item2];
                    
                    _fieldInitializer.tileArrayBool[arrayPos[0].Item1, arrayPos[0].Item2] = true;
                }
                else if (repeatCount >= 2)
                {
                    _moving = false;
                    repeatCount = 0;
                }
            }

        }
    }

    private void GetInputAction()
    {
        if (_moveAction.WasPressedThisFrame())
        {
            _moving = true;
            _moveAxis = _moveAction.ReadValue<Vector2>();
            _fieldInitializer.GetFreeTiles(); 
        }
    }
}
