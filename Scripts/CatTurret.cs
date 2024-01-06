using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatTurret : MonoBehaviour
{
    [SerializeField]
    Weapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = GameObject.Find("Player").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Vector3 enemyPosition = collision.transform.position;

            Vector3 lookDirection = enemyPosition - transform.position;
            lookDirection.z = 0f;

            transform.right = lookDirection.normalized;
            weapon.Shoot();
        }
    }
}
