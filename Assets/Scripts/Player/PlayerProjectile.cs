using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private BossStats bossStats;
    [SerializeField] private float speed = 3f;
    [SerializeField] private int damage;
    [SerializeField] private TextMeshPro hitsplash;
    private HealthBar bossHB;
    private int hpAfterDmg;
    void Start()
    {
        bossStats = GameObject.Find("Boss").GetComponent<BossStats>();
        bossHB = GameObject.Find("BossHB").GetComponent<HealthBar>();
        target = GameObject.Find("Boss");
        hitsplash = GameObject.Find("Hitsplash").GetComponent<TextMeshPro>();
        hitsplash.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            hitsplash.gameObject.SetActive(true);
            damage = Random.Range(0, 15);
            hpAfterDmg = bossStats.getHitpoints() - damage;
            bossStats.setHitpoints(hpAfterDmg);
            bossHB.setHealthBar(damage);
            Debug.Log(damage);
            hitsplash.text = "-"+damage.ToString();
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
