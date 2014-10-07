using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class World : FContainer
{
    WorldSection[] sections = new WorldSection[C.NUM_SECTIONS];
    public float speed { get; set; }
    public World()
    {
        speed = 0;
        for (int i = 0; i < C.NUM_SECTIONS; i++)
        {
            sections[i] = new WorldSection(i > 0 ? sections[i-1].currentPreset.sectionType : -1);
            sections[i].SetPosition(C.SECTION_SIZE * C.floorWidth * i, 0);
            this.AddChild(sections[i]);
        }
        Futile.instance.SignalUpdate += Update;
    }

    private int sectionInd = 0;
    private void Update()
    {
        this.x -= speed * Time.deltaTime;
        float totalSectionWidth = C.SECTION_SIZE * C.floorWidth;

        if (this.x + Futile.screen.halfWidth + 50 < -totalSectionWidth)
        {
            this.x += totalSectionWidth;
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

    }
}

