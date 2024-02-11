using UnityEngine;

public struct BrickData
{
    public Vector3 positon;
    public int colorIndex;
    public int hitCount;
    public BrickData(Vector3 _position, int _colorIndex, int _hitCount)
    {
        positon = _position;
        colorIndex = _colorIndex;
        hitCount = _hitCount;
    }

}
