using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

        FutileParams futileParams = new FutileParams(true, false, false, false);
        futileParams.AddResolutionLevel(568.0f, 1.0f, 1.0f, ""); // Scale up to 1136

        futileParams.origin = new Vector2(0.5f, 0.5f);
        futileParams.backgroundColor = new Color(0, .03f, .113f);
        futileParams.shouldLerpToNearestResolutionLevel = true;

        Futile.instance.Init(futileParams);

        Futile.atlasManager.LoadAtlas("Atlases/NinjaCircuitAtlas");

        World world = new World();
        world.SetPosition(-Futile.screen.halfWidth, -Futile.screen.halfHeight);
        Futile.stage.AddChild(world);

        FAnimatedSprite player = new FAnimatedSprite("player");
        player.addAnimation(new FAnimation("run", new int[] { 1,2 },100,true));
        player.play("run");
        player.SetPosition(new Vector2(-Futile.screen.halfWidth + player.width / 2 + 30, -Futile.screen.halfHeight + 30 + 20 + 60 + player.height/2));
        Futile.stage.AddChild(player);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
