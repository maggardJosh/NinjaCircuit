using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class WorldSection : FContainer
{
    Floor[] floorList = new Floor[C.SECTION_ROWS * C.SECTION_SIZE];
    FContainer[] floorLevels = new FContainer[C.SECTION_ROWS];

    public WorldSection()
    {
        for (int i = 0; i < C.SECTION_ROWS; i++)
        {
            floorLevels[i] = new FContainer();
            for (int j = 0; j < C.SECTION_SIZE; j++)
            {
                int index = i * C.SECTION_SIZE + j;
                floorList[index] = Floor.getFloor(1);
                floorLevels[i].AddChild(floorList[index]);
                floorList[index].SetPosition(new Vector2((.5f + j) * C.floorWidth, 20 + 30 + (60 * i)));
            }
        }
        for (int i = C.SECTION_ROWS - 1; i >= 0; i--)
            this.AddChild(floorLevels[i]);
        LoadRandomPreset();
    }

    public void LoadRandomPreset()
    {
        SectionPreset s = SectionPreset.getRandomPreset();
        for (int i = 0; i < C.SECTION_ROWS; i++)
            for (int j = 0; j < C.SECTION_SIZE; j++)
                floorList[i * C.SECTION_SIZE + j].setFloorType(s.floorTypes[j,i]);
    }
}

