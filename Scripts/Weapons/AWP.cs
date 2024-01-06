using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AWP : Weapon
{
    public AWP()
    {
        isAutomatic = false;
        Damage = 20;
    }
    public void Start()
    {
        isAutomatic = false;
        Damage = 20;
    }
}
