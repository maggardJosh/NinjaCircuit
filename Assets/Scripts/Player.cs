using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Player : FContainer
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

        Futile.instance.SignalUpdate += Update;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            level++;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            level--;
        level = Mathf.Clamp(level, 0, C.SECTION_ROWS - 1);
        playerSprite.SetPosition(levelZeroPosition + levelDisp * level);

    }


}

