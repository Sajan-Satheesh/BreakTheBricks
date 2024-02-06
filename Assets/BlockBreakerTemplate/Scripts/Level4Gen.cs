using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Gen : BrickGenerator
{
    int repStartIndex = 1;
    int repIndex = 1;
    protected override void GenerationMethod()
    {
        for (int y = brickCountY / 2; y > (int)brickHeight; y -= 2)
        {
            if (colorId == colorsLength || colorId == -1) //If the 'colorId' is equal to the 'colors' array length. This means there is no more colors left
            {
                colorReverse = !colorReverse;
                colorId = colorReverse ? colorId - 2 : colorId + 2;
            }

            if (hitCount > maxHitCount || hitCount == 0)
            {
                hitReverse = !hitReverse;
                hitCount = hitReverse ? hitCount - 2 : hitCount + 2;
            }
            resetRepIndex(ref repStartIndex, 1, 3, false);
            repIndex = repStartIndex;
            for (int x = -brickCountX / 2; x < brickCountX/2; x++)
            {
                resetRepIndex(ref repIndex, 3, 1);
                if (repIndex == 1) Set1(x, y);
                if (repIndex == 2) Set2(x, y);
                repIndex++;
            }
            repStartIndex--;
            colorId = colorReverse ? colorId - 1 : colorId + 1;                      //Increases the 'colorId' by 1 as a new row is about to be made
            hitCount = hitReverse ? hitCount - 1 : hitCount + 1;
        }
        for (int y = 0; y > -brickCountY/2; y--)
        {
            for (int x = -brickCountX / 2; x < brickCountX / 2; x++)
            {
                position = new Vector3((x * (brickWidth + brickGap)), (y * (brickHeight + brickGap)), 0);                       //The 'pos' variable is where the brick will spawn at
                brickDatas.Add(new BrickData(position + offset, colorId, hitCount));
            }   
        }
    }
    private void Set1(int row, int col)
    {
        position = new Vector3((row * (brickWidth + brickGap)), (col * (brickHeight) - brickHeight), 0);                       //The 'pos' variable is where the brick will spawn at
        brickDatas.Add(new BrickData(position + offset, colorId, hitCount));
    }
    private void Set2(int row, int col)
    {
        position = new Vector3((row * (brickWidth + brickGap)), (col * brickHeight), 0);                       //The 'pos' variable is where the brick will spawn at
        brickDatas.Add(new BrickData(position + offset, colorId, hitCount));
    }

    private void resetRepIndex(ref int index, int endIndex, int startIndex, bool greater = true)
    {
        if (!greater)
        {
            if (index < endIndex) index = startIndex;
            return;
        }
        if (index > endIndex) index = startIndex;
    }
}
