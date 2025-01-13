using TMPro;
using UnityEngine;

public class Square : SquareStats
{
    public int Value
    {
        get { return _squareValue; }
        set { _squareValue = value; }
    }
    private TextMeshPro _valueText;
    private void Start()
    {
        _valueText = transform.Find("Value Text").GetComponent<TextMeshPro>();

        _squareValue = 2;
    }

    private void Update()
    {
        _valueText.text = _squareValue.ToString();
    }
}
