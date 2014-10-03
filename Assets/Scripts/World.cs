using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class World : FContainer
{
    WorldSection[] sections = new WorldSection[C.NUM_SECTIONS];
    public World()
    {
        for (int i = 0; i < C.NUM_SECTIONS; i++)
        {
            sections[i] = new WorldSection();
            sections[i].SetPosition(C.SECTION_SIZE * C.floorWidth * i, 0);
            this.AddChild(sections[i]);
        }
        Futile.instance.SignalUpdate += Update;
    }

    private void Update()
    {
        this.x -= 150 * Time.deltaTime;
    }
}

