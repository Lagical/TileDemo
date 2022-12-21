using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelkotScript : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Prayers prayers;
    [SerializeField] private BossAttack bossAttack;
    [SerializeField] private float speed = 3f;
    [SerializeField] private int damage;
    private int hpAfterDmg;
    private GameObject boss;
    private bool inLineWithPlayer = false;
    private Vector3 playerPos;
    private Vector3 projectDirection;

    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        prayers = GameObject.Find("OverheadPrayer").GetComponent<Prayers>();
        bossAttack = GameObject.Find("Boss").GetComponent<BossAttack>();
        target = GameObject.Find("Player");
        boss = GameObject.Find("Boss");
        playerPos = playerStats.getPlayerPosition();

        float xRange;
        if (playerPos.x < 0 && playerPos.y < boss.transform.position.y || playerPos.x > 0 && playerPos.y > boss.transform.position.y)
            xRange = Random.Range(0, 6.5f);
        else
            xRange = Random.Range(-6.5f, 0f);

        projectDirection = new Vector3(xRange, -5.5f, 0).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            damage = Random.Range(70, 97);
            hpAfterDmg = playerStats.getHitpoints() - damage;
            playerStats.setHitpoints(hpAfterDmg);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isProjectileLineWithPlayer() && !inLineWithPlayer)
        {
            inLineWithPlayer = true;
            playerPos = playerStats.getPlayerPosition();
            projectDirection = (playerPos - transform.position).normalized;
            speed *= 2;
        }
        transform.position += Time.deltaTime * speed * projectDirection;
    }



    private bool isProjectileLineWithPlayer()
    {
        if (inLineWithPlayer)
            return true;

        Vector3 bossProjectileVector = transform.position - boss.transform.position;
        Vector3 projectileTargetVector = target.transform.position - transform.position;

        int bossProjVecLength = (int) (bossProjectileVector).sqrMagnitude;
        int projTargetVecLength = (int) (projectileTargetVector).sqrMagnitude;

        int bossTargetVecLength = (int) (target.transform.position - boss.transform.position).sqrMagnitude;

        int hysteresis = 600;

        if (bossProjVecLength * bossProjVecLength + projTargetVecLength * projTargetVecLength >= bossTargetVecLength * bossTargetVecLength - hysteresis
            && bossProjVecLength * bossProjVecLength + projTargetVecLength * projTargetVecLength <= bossTargetVecLength * bossTargetVecLength + hysteresis
            && bossProjVecLength > 0)
        {
            return true;
        }
        return false;
    }
}
