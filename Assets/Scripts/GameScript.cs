using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {

    
	// Use this for initialization
	void Start () {

        FutileParams futileParams = new FutileParams(true, false, false, false);
        futileParams.AddResolutionLevel(1136.0f, 1.0f, 1.0f, "");

        futileParams.origin = new Vector2(0.5f, 0.5f);
        futileParams.backgroundColor = new Color(0, .03f, .113f);
        futileParams.shouldLerpToNearestResolutionLevel = true;

        Futile.instance.Init(futileParams);

        Futile.atlasManager.LoadAtlas("Atlases/NinjaCircuitAtlas");

        Futile.atlasManager.LoadFont(C.fontOne, "pressStart2P_0", "Atlases/pressStart2P", 0,0);

        World world = new World();
        Futile.stage.AddChild(world);
        Go.to(world, 1.0f, new TweenConfig().floatProp("speed", 600f).setEaseType(EaseType.QuadIn));
        
        

	}
	
	// Update is called once per frame
	void Update () {
            
	}
}
