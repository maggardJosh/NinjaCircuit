using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Player : FContainer, FSingleTouchableInterface
{
    public FAnimatedSprite playerSprite;

    public int level = 1;
    private Vector2 levelZeroPosition;
    private Vector2 levelDisp;

    public Player()
    {
        playerSprite = new FAnimatedSprite("player");
        playerSprite.addAnimation(new FAnimation("run", new int[] { 1, 2 }, 100, true));
        playerSprite.play("run");
        this.AddChild(playerSprite);

        levelZeroPosition = new Vector2(-Futile.screen.halfWidth + playerSprite.width / 2 + 30, -Futile.screen.halfHeight + 30 + 20 + playerSprite.height / 2);
        
        levelDisp = new Vector2(C.floorAngleXOffset, C.floorHeight);
        playerSprite.SetPosition(levelZeroPosition + levelDisp * level);
        EnableSingleTouch();
        Futile.instance.SignalUpdate += Update;
    }

    private int swipeLevelChange = 0;
    public void Update()
    {
        int oldLevel = level;

        level += swipeLevelChange;
        swipeLevelChange = 0;
        

        if (Input.GetKeyDown(KeyCode.UpArrow))
            level++;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            level--;
        level = Mathf.Clamp(level, 0, C.SECTION_ROWS - 1);
        if (oldLevel != level)
        {
            Go.killAllTweensWithTarget(playerSprite);
            Vector2 newPos = levelZeroPosition + levelDisp * level;
            Go.to(playerSprite, .3f, new TweenConfig().floatProp("x", newPos.x).floatProp("y", newPos.y).setEaseType(EaseType.QuadOut));
        }

    }
    Vector2 startPos = Vector2.zero;
    Vector2 diff = Vector2.zero;
    public bool HandleSingleTouchBegan(FTouch touch)
    {
        startPos = touch.position;   
        return true;
    }

    public void HandleSingleTouchMoved(FTouch touch)
    {
        
    }
    public void HandleSingleTouchEnded(FTouch touch)
    {
        diff = touch.position - startPos;
        if (diff.y > 100)
            swipeLevelChange = 1;
        else if (diff.y < -100)
            swipeLevelChange = -1;
    }
    public void HandleSingleTouchCanceled(FTouch touch)
    {

    }


}

