using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class Level1Gen : LevelGenerator
{
    protected override void GenerationMethod()
    {
        for (int x = -(brickCountX / 2); x < (brickCountX / 2); x++)
        {
            if (colorId >= colorsLength || colorId == -1) //If the 'colorId' is equal to the 'colors' array length. This means there is no more colors left
            {
                colorReverse = !colorReverse;
                colorId = colorReverse ? colorId = colorsLength - 2 : colorId = 1;
            }

            if (hitCount > maxHitCount || hitCount == 0)
            {
                hitReverse = !hitReverse;
                hitCount = hitReverse ? hitCount - 2 : hitCount + 2;
            }
            for (int y = -(brickCountY / 2); y < (brickCountY / 2); y++)
            {
                position = new Vector3((x * (brickWidth + brickGap)), ( y * (brickHeight + brickGap)), 0);                    //The 'pos' variable is where the brick will spawn at
                brickDatas.Add(new BrickData(position + offset, colorId, hitCount));
            }
            colorId = colorReverse ? colorId - 1 : colorId + 1;                      //Increases the 'colorId' by 1 as a new row is about to be made
            hitCount = hitReverse ? hitCount - 1 : hitCount + 1;
        }
    }

}
