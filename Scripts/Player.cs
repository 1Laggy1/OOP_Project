using UnityEditor;
using UnityEngine;

public class Player : Entity
{
    public float speed = 3f;
    private Rigidbody2D rb;
    public Weapon weapon;
    [SerializeField]
    Transform WeaponCarry_tr;
    public float rotationSpeed = 5.0f;
    bool readyToPickUp;
    bool readyToDrop;
    [SerializeField]
    Inventory inventory;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        WeaponMove();
        Shooting();
        ReadyToPickUp();
        ReadyToDrop();
        OpenInventory();
    }
    void Move()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontalInput, verticalInput);

        movement.Normalize();

        rb.velocity = movement * speed; //* Time.deltaTime*(777/4f);
        if (movement.magnitude == 0)
        {
            rb.velocity = Vector2.zero;
        }
    }

    void Shooting()
    {
        if (weapon != null)
            if (weapon.isAutomatic)
            {
                if (Input.GetButton("Fire1"))
                {
                    weapon.Shoot(this.gameObject);
                }
            }
            else
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    weapon.Shoot(this.gameObject);
                }
            }
    }

    void WeaponMove()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        WeaponCarry_tr.eulerAngles = new Vector3(0, WeaponCarry_tr.eulerAngles.y, angle);
        if (angle < -90 || angle > 90)
        {
            WeaponCarry_tr.localScale = new Vector3(WeaponCarry_tr.localScale.x, -1f, WeaponCarry_tr.localScale.z);
        }
        else
        {
            WeaponCarry_tr.localScale = new Vector3(WeaponCarry_tr.localScale.x, 1f, WeaponCarry_tr.localScale.z);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (readyToPickUp)
        {
            readyToPickUp = false;
            GameObject go = collision.gameObject;
            if (go.tag == "Weapon")
            {
                PickUp(go);
            }
            else if (go.tag == "Item")
            {
                inventory.PickUp(collision.GetComponent<Item>());
            }
            else if (go.tag == "Chest")
            {
                inventory.ShowChest(collision.GetComponent<Chest>());
            }
        }
    }
    void PickUp(GameObject go)
    {
        //WeaponCarry_tr.rotation = Quaternion.identity;
        go.transform.parent = WeaponCarry_tr;
        go.GetComponent<Weapon>().PickUp();
        weapon = go.GetComponent<Weapon>();
    }

    void ReadyToPickUp()
    {
        if (Input.GetButtonDown("Interact"))
        {
            readyToPickUp = true;
            Debug.Log(readyToPickUp);
        }

        if (Input.GetButtonUp("Interact"))
        {
            readyToPickUp = false;
            Debug.Log(readyToPickUp);
        }
    }
    void ReadyToDrop()
    {
        if (Input.GetButtonDown("Drop"))
        {
            Drop();
        }
    }
    void OpenInventory()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventory.Show();
        }
    }
    void Drop()
    {
        if (weapon != null)
        {
            weapon = null;
            WeaponCarry_tr.DetachChildren();
        }
    }
}
