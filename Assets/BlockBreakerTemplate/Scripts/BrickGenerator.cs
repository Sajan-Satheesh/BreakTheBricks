using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public abstract class BrickGenerator 
{
    protected bool colorReverse = false;
    protected bool hitReverse = false;
    protected int colorId = 0;                    //'colorId' is used to tell which color is currently being used on the bricks. Increased by 1 every row of bricks
    protected int hitCount = 1;
    protected float brickHeight = 0f;
    protected float brickWidth = 0f;
    protected float brickGap = 0.1f;
    protected int brickCountX;                                     //The amount of bricks that will be spawned horizontally (Odd numbers are recommended)
    protected int brickCountY;
    protected int colorsLength;
    protected int maxHitCount;
    protected Vector3 position;
    protected Vector3 offset;
    [SerializeField] protected List<BrickData> brickDatas = new List<BrickData>();

    protected void SetOffset()
    {
        offset = new Vector3(brickWidth/2 + brickGap/2, brickHeight/2, 0);
    }
    public List<BrickData> CreatePositionalPattern(int _brickCountX, int _brickCountY, Vector3 brickSize, float _brickGap, int totalAvailableColors, int _maxHitCount)
    {
        colorReverse = false;
        hitReverse = false;
        brickDatas?.Clear();
        brickCountX = _brickCountX;
        brickCountY = _brickCountY;
        brickHeight = brickSize.y;
        brickWidth = brickSize.x;
        brickGap = Mathf.Abs(_brickGap);
        colorsLength = totalAvailableColors;
        maxHitCount = _maxHitCount;
        SetOffset();
        GenerationMethod();
        return brickDatas;
    }

    protected abstract void GenerationMethod();

}
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

