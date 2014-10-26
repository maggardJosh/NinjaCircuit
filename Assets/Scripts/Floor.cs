using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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
    public FAnimatedSprite floorSprite;
    private Floor()
    {
        isActive = true;
        floorSprite = new FAnimatedSprite("floor");
        floorSprite.addAnimation(new FAnimation("floor1", new int[] { 1 }, 100, true));
        floorSprite.color = NORMAL_COLOR;
        this.AddChild(floorSprite);
    }
    private readonly Color HIGHLIGHT_COLOR = Color.white;
    private readonly Color NORMAL_COLOR = new Color(.7f, .7f, .7f);

    private bool isHighlighted = false;
    public void Highlight()
    {
        if (!isHighlighted)
        {
            Go.killAllTweensWithTarget(floorSprite);
            Go.to(floorSprite, .5f, new TweenConfig().colorProp("color", HIGHLIGHT_COLOR).setEaseType(EaseType.QuadOut));
            isHighlighted = true;   
        }
    }

    public void Dehighlight()
    {
        if (isHighlighted)
        {
            Go.killAllTweensWithTarget(floorSprite);
            Go.to(floorSprite, .5f, new TweenConfig().colorProp("color", NORMAL_COLOR).setEaseType(EaseType.QuadOut));
            isHighlighted = false;   
        }
    }

    public void setFloorType(int newFloorType)
    {        
        floorSprite.isVisible = true;
        switch(newFloorType)
        {
            case 0: floorSprite.isVisible = false; break;
            case 1: floorSprite.play("floor1"); break;
            default: RXDebug.Log("unknown floor type " + newFloorType); floorSprite.play("floor1"); break;
        }
    }
}

