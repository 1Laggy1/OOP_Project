using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolSevenTeen : Weapon
{
    public PistolSevenTeen()
    {
        isAutomatic = false;
        Damage = 35;
    }
    public void Start()
    {
        isAutomatic = false;
        Damage = 35;
    }
}
