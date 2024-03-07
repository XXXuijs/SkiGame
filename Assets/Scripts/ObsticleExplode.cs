using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticleExplode : Obstacle
{
    public override void HitPlayer (GameObject player){
        base.HitPlayer(player);
        Destroy(gameObject);
    }
}
