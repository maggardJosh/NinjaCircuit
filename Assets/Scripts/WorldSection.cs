﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class WorldSection 
{
    Floor[] floorList = new Floor[C.SECTION_ROWS * C.SECTION_SIZE];
    public FContainer[] floorLevels = new FContainer[C.SECTION_ROWS];
    public SectionPreset currentPreset;
    private float _x;
    
    public float x
    {
        get { return _x; }
        set
        {
            _x = value;
            for (int i = 0; i < C.SECTION_ROWS; i++)
            { 
                floorLevels[i].x = value; 
            }
        }
    }

    public void MoveToFront()
    {
        for (int i = C.SECTION_ROWS-1; i >= 0; i--)
            floorLevels[i].MoveToFront();
    }

    public WorldSection(int lastSectionPreset = -1)
    {
        for (int i = 0; i < C.SECTION_ROWS; i++)
        {
            floorLevels[i] = new FContainer();
            for (int j = 0; j < C.SECTION_SIZE; j++)
            {
                int index = i * C.SECTION_SIZE + j;
                floorList[index] = Floor.getFloor(1);
                floorLevels[i].AddChild(floorList[index]);
                floorList[index].SetPosition(new Vector2((.5f + j) * C.floorWidth + i * C.floorAngleXOffset, 20 + 30 + (C.floorHeight * i)));
            }
        }
        LoadRandomPreset(lastSectionPreset);
    }

    public bool IsOnFloor(float x, int level)
    {
        int index = Mathf.FloorToInt((x - this.x) / C.floorWidth - 1.5f);
        if (index < 0 || index >= C.SECTION_SIZE)
        {
            foreach (Floor f in floorList)
                f.Dehighlight();
            return false;   //Not even in this section
        }


        int floorListIndex = index + level * C.SECTION_SIZE;
        floorList[floorListIndex].Highlight();
        for (int i = 0; i < floorList.Length; i++)
            if (i != floorListIndex)
                floorList[i].Dehighlight();
        
        return currentPreset.floorTypes[index, C.SECTION_ROWS- level-1] == 1;
    }

    public void LoadRandomPreset(int lastSectionPreset)
    {
        SectionPreset s = SectionPreset.getRandomPreset();
        if (lastSectionPreset == -1)
                s = SectionPreset.getPreset(0);
        
        
        while (s.sectionType == lastSectionPreset)      //Make sure we don't get the same preset twice in a row
            s = SectionPreset.getRandomPreset();
        this.currentPreset = s;
        for (int i = 0; i < C.SECTION_ROWS; i++)
            for (int j = 0; j < C.SECTION_SIZE; j++)
                floorList[i * C.SECTION_SIZE + j].setFloorType(s.floorTypes[j, C.SECTION_ROWS - 1 - i]);
    }
}

