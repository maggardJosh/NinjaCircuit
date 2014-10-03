using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Floor : FContainer
{
    private static Floor[] floorList;
    const int MAX_FLOORS = 100;
    public static Floor getFloor(int type)
    {
        if (floorList == null)
            floorList = new Floor[MAX_FLOORS];
        for (int x = 0; x < floorList.Length; x++)
        {
            if (floorList[x] == null)
            {
                Floor newFloor = new Floor();
                newFloor.setFloorType(type);
                floorList[x] = newFloor;
                return newFloor;
            }
            else if (!floorList[x].isActive)
            {
                return floorList[x];
            }
        }
        RXDebug.Log("Somehow reached max number of floors.. HOW!?");
        return floorList[RXRandom.Int(MAX_FLOORS)];
    }

    public bool isActive = false;
    public int floorType = 0;
    FAnimatedSprite floorSprite;
    private Floor()
    {
        isActive = true;
        floorSprite = new FAnimatedSprite("floor");
        floorSprite.addAnimation(new FAnimation("floor1", new int[] { 1 }, 100, true));
        this.AddChild(floorSprite);
    }

    public void setFloorType(int newFloorType)
    {
        RXDebug.Log(newFloorType);
        floorSprite.isVisible = true;
        switch(newFloorType)
        {
            case 1: floorSprite.isVisible = false; break;
            case 2: floorSprite.play("floor1"); break;
            default: RXDebug.Log("unknown floor type " + newFloorType); floorSprite.play("floor1"); break;
        }
    }
}

