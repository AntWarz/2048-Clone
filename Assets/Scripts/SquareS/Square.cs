using TMPro;
using UnityEngine;

public class Square : SquareStats
{
    public int Value
    {
        get { return _squareValue; }
        set { _squareValue = value; }
    }

    public (int, int) ArrayIndex
    {
        get { return _arrayIndex; }
        set { _arrayIndex = value; }
    }

    public bool HasSurroundingEqual
    {
        get { return _hasSurroundingEqual; }
    }


    private TextMeshPro _valueText;
    private void Start()
    {
        _valueText = transform.Find("Value Text").GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        _hasSurroundingEqual = FieldScanner.EqualsSurroundingSquare(ArrayIndex, Value);
        //Debug.Log($"Square at {(ArrayIndex.Item1, ArrayIndex.Item2)} has surrounding squares with same value: {_hasSurroundingEqual}");
        _valueText.text = _squareValue.ToString();
    }
}
