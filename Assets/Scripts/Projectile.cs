using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private float speed = 3f;
    [SerializeField] private int damage;
    private int hpAfterDmg;
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        target = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            damage = Random.Range(70, 97);
            hpAfterDmg = playerStats.getHitpoints() - damage;
            playerStats.setHitpoints(hpAfterDmg);
            Debug.Log("PROJECTILE PUOLELLA " + hpAfterDmg);
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
