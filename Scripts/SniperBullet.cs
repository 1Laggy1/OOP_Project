using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : Bullet
{

    // Start is called before the first frame update
    override public void Start()
    {
        distance = 200;
        speed = 70f;
        dmg = 100f;
        base.Start();
    }
}
