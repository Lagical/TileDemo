using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Minion : MonoBehaviour
{
    [SerializeField] protected BossStats bossStats;
    [SerializeField] protected PlayerStats playerStats;
    [SerializeField] protected int attackDamage = 3;
    [SerializeField] protected float movementSpeed = 15f;
    [SerializeField] protected int health = 60;
    [SerializeField] protected float attackRange = 5f;
    [SerializeField] protected private float bossInteractionTime = 2f;  // Sekunteina
    [SerializeField] protected private float playerInteractionTime = 2f;  // Sekunteina
    [SerializeField] protected private int healPower = 5;
    [SerializeField] private PlayerAttack playerAttack;

    private Texture2D cursor;
    private float lastInteractionTime = 0f;
    private bool beingAttacked = false;
    private GameObject ground;
    private Vector3 centerPosition;
    private bool onMove;
    private float moveTime;
    private float waitTime;
    protected virtual void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        playerAttack = GameObject.Find("Player").GetComponent<PlayerAttack>();
        bossStats = GameObject.Find("Boss").GetComponent<BossStats>();
        ground = GameObject.FindGameObjectWithTag("Ground");
        cursor = GameObject.Find("Boss").GetComponent<Texture2D>();

        float width = ground.transform.GetComponent<Renderer>().localBounds.size.x;
        float height = ground.transform.GetComponent<Renderer>().localBounds.size.y;
        Vector3 center = ground.transform.GetComponent<Renderer>().localBounds.center;

        float randX = Random.Range(-width / 2, width / 2) + center.x;
        float randY = Random.Range(-height / 2, height / 2) + center.y;

        centerPosition = new Vector3(randX, randY, 0f);
        destinationPos = transform.position;
    }
    protected virtual void Update()
    {
        moveTime += Time.deltaTime;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
        if (destinationPos != transform.position)
        {
            onMove = true;
            transform.up = destinationPos - transform.position;
        }
        else
            onMove = false;
    }

    public void SetBossInteractionTime(float interactionTime)
    {
        bossInteractionTime = interactionTime;
    }
    public float GetBossInteractionTime()
    {
        return bossInteractionTime;
    }
    public void SetPlayerInteractionTime(float interactionTime)
    {
        playerInteractionTime = interactionTime;
    }
    public float GetPlayerInteractionTime()
    {
        return playerInteractionTime;
    }

    public void SetAttackRange(float attackRange)
    {
        this.attackRange = attackRange;
    }
    public float GetMovementSpeed()
    {
        return movementSpeed;
    }
    public void SetMovementSpeed(float movementSpeed)
    {
        this.movementSpeed = movementSpeed;
    }
    public float GetAttackRange()
    {
        return attackRange;
    }
    public int GetHealth()
    {
        return health;
    }

    public int GetAttackDamage()
    {
        return attackDamage;
    }
    public void SetHealth(int health)
    {
        this.health = health;
    }
    protected void SetHealPower(int healPower)
    {
        this.healPower = healPower;
    }
    protected int GetHealPower()
    {
        return healPower;
    }
    public bool IsAttacked()
    {
        return beingAttacked;
    }
    public void HitMinion(int damage)
    {
        beingAttacked = true;
        SetHealth(GetHealth() - damage);
    }

    void OnMouseDown()
    {
        playerAttack.tryToAttack(transform.position);
    }

    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }
    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    protected virtual void InteractionWithBoss()
    {
        // Lapsi luokassa m‰‰ritys, esim minionin isku tai heal
    }
    protected virtual void InteractionWithPlayer()
    {
        // Lapsi luokassa m‰‰ritys, esim minionin isku tai heal
    }
    protected void FollowPlayer()
    {
        lastInteractionTime += Time.deltaTime;
        destinationPos = playerStats.transform.position;
        float distance = Vector3.Distance(transform.position, playerStats.getPlayerPosition());
        if (distance > attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerStats.transform.position, movementSpeed * Time.deltaTime);
            return;
        }

        if (lastInteractionTime >= playerInteractionTime)
        {
            InteractionWithPlayer(); // M‰‰ritys lapsiluokassa
            lastInteractionTime = 0f;
        }
    }
    protected void FollowBoss()
    {
        lastInteractionTime += Time.deltaTime;
        destinationPos = bossStats.transform.position;
        float distance = Vector3.Distance(transform.position, bossStats.getbossPosition());
        if (distance > attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, bossStats.transform.position, movementSpeed * Time.deltaTime);
            return;
        }
        if (lastInteractionTime >= bossInteractionTime)
        {
            InteractionWithBoss(); // M‰‰ritys lapsiluokassa
            lastInteractionTime = 0f;
        }
    }
    private Vector3 destinationPos;
    private bool justMoved;
    protected void WalkAround()
    {
        if (onMove && !justMoved)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinationPos, movementSpeed * Time.deltaTime);
            if (transform.position == destinationPos)
                justMoved = true;
            return;
        }

        if (justMoved)
        {
            moveTime = 0;
            waitTime = Random.Range(0.5f, 5f);
            justMoved = false;
        }

        if(moveTime < waitTime)
            return;

        Vector3 desVec = rafflePosition();  // Arpoo vektorin
        destinationPos = desVec + transform.position;

        if (!ground.GetComponent<Tilemap>().HasTile(Vector3Int.FloorToInt(destinationPos)))
            destinationPos = getInBoundsVec(desVec);

        transform.position = new Vector3(transform.position.x + 0.0001f, transform.position.y, 0f); // Liikuttaa paikkaa v‰h‰n, jotta onMove muuttuja vaihtuu trueksi
    }

    private Vector3 rafflePosition()
    {
        float centerPosDistance = Vector3.Distance(transform.position, centerPosition);
        float ratio = 1.0f / (centerPosDistance + 1);
        Vector3 miniToCentVec = (centerPosition - transform.position).normalized;

        float x, y;
        float rand = Random.Range(0f, 1f);
        if (rand <= ratio)
            x = -miniToCentVec.x / miniToCentVec.x;
        else
            x = miniToCentVec.x / Mathf.Abs(miniToCentVec.x);

        rand = Random.Range(0f, 1f);
        if (rand <= ratio)
            y = -miniToCentVec.y / miniToCentVec.y;
        else
            y = miniToCentVec.y / Mathf.Abs(miniToCentVec.y);

        float randDistanceX = Random.Range(1f, 4f);
        float randDistanceY = Random.Range(1f, 4f);

        Vector3 desVec = new Vector3(x * randDistanceX, y * randDistanceY, 0);
        return desVec;
    }
    private Vector3 getInBoundsVec(Vector3 desVec)
    {
        float scaleX = (1 - Mathf.Abs((GetComponent<SpriteRenderer>().size.x / 2f + 1) / desVec.x));
        float scaleY = (1 - Mathf.Abs((GetComponent<SpriteRenderer>().size.y / 2 + 1) / desVec.y));

        Vector3 scaleVector = new Vector3(scaleX, scaleY, 1);
        Vector3 closestPoint = ground.GetComponent<Renderer>().bounds.ClosestPoint(destinationPos);

        closestPoint.Scale(scaleVector);
        Debug.Log(scaleVector);

        return closestPoint;
    }
}
