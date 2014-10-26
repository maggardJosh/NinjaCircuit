using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Player : FContainer, FSingleTouchableInterface
{
    public FAnimatedSprite playerSprite;
    private FSprite shadow;

    public int level = 1;
    private Vector2 levelZeroPosition;
    private Vector2 levelDisp;

    public Player()
    {
        shadow = new FSprite("shadow");
        this.AddChild(shadow);
        shadow.alpha = .3f;

        playerSprite = new FAnimatedSprite("player");
        playerSprite.addAnimation(new FAnimation("run", new int[] { 1, 2 }, 100, true));
        playerSprite.play("run");
        this.AddChild(playerSprite);


        levelZeroPosition = new Vector2(-Futile.screen.halfWidth + playerSprite.width / 2 + 30, -Futile.screen.halfHeight + 30 + 20 + playerSprite.height / 2);

        levelDisp = new Vector2(C.floorAngleXOffset, C.floorHeight);
        updateSpritePosition();
        EnableSingleTouch();

    }

    private void updateSpritePosition()
    {
        playerSprite.SetPosition(levelZeroPosition + levelDisp * level + Vector2.up * staggerYValue + Vector2.right * staggerXValue + Vector2.up * heightOffGround);
        shadow.SetPosition(levelZeroPosition + levelDisp * level + Vector2.up * staggerYValue + Vector2.right * staggerXValue);
        shadow.scale = 1 - (heightOffGround / JUMP_HEIGHT) * .3f;
        
    }


    private int swipeLevelChange = 0;
    public void Update(World world)
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

        if (isJumping)
            JumpLogic();

        StaggerLogic();

        updateSpritePosition();
        if (!isJumping)
            world.IsOnFloor(x, level);
        else
            world.IsOnFloor(-900, 1);

    }

    float runStaggerYCount = 0;
    float runStaggerYLength = 3.0f;
    float runStaggerYSize = 20;
    float staggerYValue = 0;
    bool fullYStagger = true;

    float runStaggerXCount = 0;
    float runStaggerXLength = 2.0f;
    float runStaggerXSize = 10;
    float staggerXValue = 0;
    bool fullXStagger = true;
    private void StaggerLogic()
    {
        runStaggerYCount += Time.deltaTime;
        staggerYValue = Mathf.Sin((runStaggerYCount / runStaggerYLength) * Mathf.PI * (fullYStagger ? 2 : 1)) * runStaggerYSize;
        if (runStaggerYCount >= runStaggerYLength)
        {
            fullYStagger = RXRandom.Bool();
            runStaggerYCount -= runStaggerYLength;
        }

        runStaggerXCount += Time.deltaTime;
        staggerXValue = Mathf.Sin((runStaggerXCount / runStaggerXLength) * Mathf.PI * (fullXStagger ? 2 : 1)) * runStaggerXSize;
        if (runStaggerXCount >= runStaggerXLength)
        {
            fullXStagger = RXRandom.Bool();
            runStaggerXCount -= runStaggerXLength;
        }
    }

    private float heightOffGround = 0;
    private bool isJumping = false;
    float jumpCount = 0;
    float jumpLength = 1.5f;
    private const float JUMP_HEIGHT = 120;

    private void JumpLogic()
    {
        jumpCount += Time.deltaTime;

        heightOffGround = Mathf.Pow(Mathf.Sin((jumpCount / jumpLength) * Mathf.PI), .7f) * JUMP_HEIGHT;
        if (jumpCount > jumpLength)
        {
            isJumping = false;
            heightOffGround = 0;
        }

    }



    Vector2 startPos = Vector2.zero;
    Vector2 diff = Vector2.zero;

    bool hasStartedTouch = false;

    public bool HandleSingleTouchBegan(FTouch touch)
    {
        startPos = touch.position;
        hasStartedTouch = true;
        return true;
    }

    public void HandleSingleTouchMoved(FTouch touch)
    {
        if (hasStartedTouch)
        {
            float yDiff = touch.position.y - startPos.y;
            if (yDiff > 100)
                swipeLevelChange = 1;
            else if (yDiff < -100)
                swipeLevelChange = -1;
            if (swipeLevelChange != 0)
                hasStartedTouch = false;
        }
    }
    public void HandleSingleTouchEnded(FTouch touch)
    {
        diff = touch.position - startPos;
        hasStartedTouch = false;
        if (diff.y < 20 && diff.y > -20)
            Jump();
    }
    public void HandleSingleTouchCanceled(FTouch touch)
    {

    }


    private void Jump()
    {
        if (isJumping)
            return;
        isJumping = true;
        jumpCount = 0;
    }

}

