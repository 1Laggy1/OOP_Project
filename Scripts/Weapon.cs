using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject Bullet;
    public float fireRate = 0.5f;
    public float Damage = 5;
    float nextFireTime = 0.0f;
    [SerializeField]
    Vector3 handsPosition = new Vector3(0, 0, 0);
    [SerializeField]
    Transform bulletPosition;
    [SerializeField]
    public bool isAutomatic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void PickUp()
    {
        transform.localPosition = handsPosition;
        transform.localRotation = Quaternion.identity;
        transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z) ;
    }

    public virtual void Shoot(GameObject Root_GO = null)
    {
        if (Time.time > nextFireTime)
        {
            // Викликаємо метод Shoot()
            Bullet bul = Instantiate(Bullet, bulletPosition.position, transform.rotation).GetComponent<Bullet>();
            bul.dmg += Damage;
            if (Root_GO != null)
            {
                bul.Root_GO = Root_GO;
            }
            // Оновлюємо час наступного пострілу
            nextFireTime = Time.time + fireRate;
        }
        
    }
}
