using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class World : FContainer
{

    public World()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 3; j++)
            {

                FSprite floor = new FSprite("floor");
                floor.sortZ = 3 - j;
                if (RXRandom.Float() < .5f)
                    this.AddChild(floor);
                floor.SetPosition(new Vector2((.5f + i) * C.sectionWidth, 20 + 30 + (60 * j)));
            }
            this.shouldSortByZ = true;
        }
        Futile.instance.SignalUpdate += Update;
    }

    private void Update()
    {
        this.x -= 150 * Time.deltaTime;
    }
}

