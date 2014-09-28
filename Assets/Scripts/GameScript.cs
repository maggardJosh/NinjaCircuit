using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

        FutileParams futileParams = new FutileParams(true, false, false, false);
        futileParams.AddResolutionLevel(568.0f, 1.0f, 1.0f, ""); // Scale up to 1136

        futileParams.origin = new Vector2(0.5f, 0.5f);
        futileParams.backgroundColor = new Color(0, 0, 0);
        futileParams.shouldLerpToNearestResolutionLevel = true;

        Futile.instance.Init(futileParams);

        Futile.atlasManager.LoadAtlas("Atlases/NinjaCircuitAtlas");

        FAnimatedSprite test = new FAnimatedSprite("player");
        test.addAnimation(new FAnimation("run", new int[] { 1,2 },100,true));
        test.play("run");
        Futile.stage.AddChild(test);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
