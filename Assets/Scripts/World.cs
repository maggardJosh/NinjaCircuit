using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class World : FContainer
{
    Player player;
    FLabel counter;

    WorldSection[] sections = new WorldSection[C.NUM_SECTIONS];
    public float speed { get; set; }
    FContainer[] worldLayer = new FContainer[C.SECTION_ROWS];
    public World()
    {
        for (int i = 0; i < C.SECTION_ROWS; i++)
        {
            worldLayer[i] = new FContainer();
            worldLayer[i].shouldSortByZ = true;
        }
        speed = 0;
        for (int i = 0; i < C.NUM_SECTIONS; i++)
        {
            sections[i] = new WorldSection(i > 0 ? sections[i - 1].currentPreset.sectionType : -1);
            sections[i].x = C.SECTION_SIZE * C.floorWidth * i;
            for (int j = 0; j < C.SECTION_ROWS; j++)
                worldLayer[j].AddChild(sections[i].floorLevels[j]);
        }
        Futile.instance.SignalUpdate += Update;

        player = new Player(this);
        worldLayer[(int)player.level].AddChild(player);
        for (int i = C.SECTION_ROWS-1; i >= 0; i--)
        {
            worldLayer[i].SetPosition(-Futile.screen.halfWidth, -Futile.screen.halfHeight);
            this.AddChild(worldLayer[i]);
        }
        counter = new FLabel(C.fontOne, "Distance: 0ft");
        counter.y = Futile.screen.halfHeight - 50;
        this.AddChild(counter);
    }

    public float getXScroll()
    {
        return worldLayer[0].x;
    }

    public bool IsOnFloor(float x, int level)
    {
        for (int i = 0; i < sections.Length; i++)
            if (sections[i].IsOnFloor(x - worldLayer[level].x, level))
                return true;
        return false;
    }

    private float distance = 0;
    private int sectionInd = 0;
    private void Update()
    {
        for (int i = 0; i < C.SECTION_ROWS; i++)
            worldLayer[i].x -= speed * Time.deltaTime;
        distance += (speed * Time.deltaTime) * (6/C.floorWidth);
        counter.text = "Distance: " + distance.ToString("0.00") + "ft";
        float totalSectionWidth = C.SECTION_SIZE * C.floorWidth;

        if (worldLayer[0].x + Futile.screen.halfWidth + 200 < -totalSectionWidth)
        {
            for (int i = 0; i < C.SECTION_ROWS; i++)
                worldLayer[i].x += totalSectionWidth;
            for (int i = 0; i < sections.Length; i++)
                sections[i].x -= totalSectionWidth;

            int farthestSectionInd = sectionInd == 0 ? C.NUM_SECTIONS - 1 : sectionInd - 1;
            sections[sectionInd].x = sections[farthestSectionInd].x + totalSectionWidth;
            sections[sectionInd].LoadRandomPreset(sections[farthestSectionInd].currentPreset.sectionType);
            sections[sectionInd].MoveToFront();
            sectionInd++;
            if (sectionInd >= C.NUM_SECTIONS)
                sectionInd = 0;
        }

        player.Update(this);
    }

    public void transitionPlayer(int newLevel)
    {
        player.RemoveFromContainer();
        worldLayer[newLevel].AddChild(player);
    }
}

