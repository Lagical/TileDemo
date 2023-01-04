using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Prayers prayers;
    [SerializeField] private BossAttack bossAttack;
    [SerializeField] private float speed = 3f;
    [SerializeField] private int damage;
    [SerializeField] private TextMeshPro hitsplashPlayer;
    private int hpAfterDmg;
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        prayers = GameObject.Find("OverheadPrayer").GetComponent<Prayers>();
        bossAttack = GameObject.Find("Boss").GetComponent<BossAttack>();
        target = GameObject.Find("Player");
        hitsplashPlayer = GameObject.Find("HitsplashPlayer").GetComponent<TextMeshPro>();
        hitsplashPlayer.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hitsplashPlayer.gameObject.SetActive(true);
            if (bossAttack.getAttackStyle() == 0 && prayers.getPrayer() == "magic")
            {
                damage = 0;
                hitsplashPlayer.text = damage.ToString();
                Destroy(gameObject);
            } 
            else if (bossAttack.getAttackStyle() == 1 && prayers.getPrayer() == "ranged")
            {
                damage = 0;
                hitsplashPlayer.text = damage.ToString();
                Destroy(gameObject);
            }
            else if (bossAttack.getAttackStyle() == 2 && prayers.getPrayer() == "melee")
            {
                damage = 0;
                hitsplashPlayer.text = damage.ToString();
                Destroy(gameObject);
            }
            else
            {
                damage = Random.Range(70, 97);
                hpAfterDmg = playerStats.getHitpoints() - damage;
                playerStats.setHitpoints(hpAfterDmg);
                hitsplashPlayer.text = "-" + damage.ToString();
                Destroy(gameObject);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        transform.up = target.transform.position - transform.position;
    }
}
