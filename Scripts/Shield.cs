using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Entity
{
    Transform player_tr;
    Transform this_tr;
    // Start is called before the first frame update
    void Start()
    {
        hp = 20;
        armor = 0;
        player_tr = GameObject.Find("Player").GetComponent<Transform>();
        this_tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        this_tr.position = player_tr.position;
    }
}
