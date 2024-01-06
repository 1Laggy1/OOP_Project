using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Root_GO;
    [SerializeField]
    public float speed = 50f;
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    public float distance = 100f;
    public float dmg = 0;
    Vector3 start = Vector3.zero;
    // Start is called before the first frame update
    virtual public void Start()
    {
        start = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(start, transform.position) >= distance)
        {
            Destroy(this.gameObject);
        }
        rb.velocity = transform.right * speed; //* Time.deltaTime * 100;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity entity = collision.GetComponent<Entity>();
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
        {
            if (Root_GO != collision.gameObject)
            {
                Destroy(this.gameObject);
                if (entity)
                {
                    entity.GetDmg(dmg);
                }
            }

        }

    }

}
