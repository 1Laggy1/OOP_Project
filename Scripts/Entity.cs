using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float hp = 100;
    public float armor = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDmg(float dmg)
    {
        if (armor != 0)
        {
            hp -= dmg*armor;
        }
        else
        {
            hp -= dmg;

        }
        if (hp <= 0)
        {
            End();
        }
    }


    public virtual void End()
    {
        Destroy(gameObject);
    }
}
