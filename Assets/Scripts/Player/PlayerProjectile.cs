using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private BossStats bossStats;
    [SerializeField] private float speed = 3f;
    [SerializeField] private int damage;
    private int hpAfterDmg;
    void Start()
    {
        bossStats = GameObject.Find("Boss").GetComponent<BossStats>();
        target = GameObject.Find("Boss");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            damage = Random.Range(0, 15);
            hpAfterDmg = bossStats.getHitpoints() - damage;
            bossStats.setHitpoints(hpAfterDmg);
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        transform.up = target.transform.position - transform.position;
    }
}
