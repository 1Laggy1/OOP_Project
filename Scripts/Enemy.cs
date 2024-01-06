using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
    public float speed = 3f;
    private Rigidbody2D rb;
    public Weapon weapon;
    [SerializeField]
    Transform WeaponCarry_tr;
    [SerializeField]
    Rigidbody2D WeaponCarry_rb;
    public float rotationSpeed = 5.0f;
    public Vector3 targetPosition = Vector3.zero;
    Behaviour behaviour = Behaviour.Idol;
    NavMeshAgent agent;
    GameObject detected_GO;
    Transform detected_tr;
    bool detected;
    float weapon_y;
    float MoveWidth = 10;

    public enum Behaviour
    {
        Idol,
        Attack,
        Escape
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        weapon_y = WeaponCarry_tr.localScale.y;
    }

    public async virtual Task<int> NewMove()
    {
        targetPosition = new Vector2(1000, 1000);
        await Task.Delay(Random.Range(1000, 5000));
        if (this)
        {
            Vector3 randomDirection = Random.insideUnitSphere * MoveWidth;
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, 10f, 1);
            targetPosition = hit.position;
            agent.SetDestination(targetPosition);
            Vector3 direction = targetPosition - transform.position;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
            if (detected_tr == null)
            {
                WeaponMove(direction);
            }
        }
        await Task.Delay(0);
        return 0;
    }

    void WeaponMove(Vector3 direction)
    {

        //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDirection = (direction - transform.position).normalized;
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

    public async virtual void Move()
    {
        if (targetPosition.x == 0 || Vector3.Distance(targetPosition, this.transform.position) < 1)
        {
            await NewMove();
        }

    }

    void Update()
    {
        Move();
        Shooting();
    }

    public void OnWallEyes()
    {
        Debug.Log(name + ": " + "Wall");
    }

    public void OnEyes(GameObject detected_go)
    {
        detected_GO = detected_go;
        detected_tr = detected_GO.transform;
        Debug.Log(name + ": " + "Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "")
        {

        }
    }

    public override void End()
    {
        weapon.transform.parent = null;
        base.End();
    }
    public void Shooting()
    {
        if (detected_tr != null)
        {
            if (weapon != null)
            {
                WeaponMove(detected_tr.position);
            }
            weapon.Shoot(this.gameObject);
        }
    }
}
