using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class World : FContainer
{
    Player player;

    WorldSection[] sections = new WorldSection[C.NUM_SECTIONS];
    public float speed { get; set; }
    FContainer worldLayer = new FContainer();
    FContainer playerLayer = new FContainer();
    public World()
    {
        speed = 0;
        for (int i = 0; i < C.NUM_SECTIONS; i++)
        {
            sections[i] = new WorldSection(i > 0 ? sections[i-1].currentPreset.sectionType : -1);
            sections[i].SetPosition(C.SECTION_SIZE * C.floorWidth * i, 0);
            worldLayer.AddChild(sections[i]);
        }
        Futile.instance.SignalUpdate += Update;

        player = new Player();
        playerLayer.AddChild(player);
        worldLayer.SetPosition(-Futile.screen.halfWidth, -Futile.screen.halfHeight);
        this.AddChild(worldLayer);
        this.AddChild(playerLayer);
    }

    public bool IsOnFloor(float x, int level)
    {
        for (int i = 0; i < sections.Length; i++)
            if (sections[i].IsOnFloor(x - worldLayer.x, level))
                return true;
        return false;
    }

    private int sectionInd = 0;
    private void Update()
    {
        worldLayer.x -= speed * Time.deltaTime;
        float totalSectionWidth = C.SECTION_SIZE * C.floorWidth;

        if (worldLayer.x + Futile.screen.halfWidth + 200 < -totalSectionWidth)
        {
            worldLayer.x += totalSectionWidth;
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
}

