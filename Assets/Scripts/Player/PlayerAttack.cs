using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject[] projectiles;
    [SerializeField] BossStats bossStats;
    private bool attacking = false;
    private bool attackingLoop = false;
    private float distanceBetween;
    private float distanceBetweenLoop;

    void Start()
    {
        
    }

    private void Update()
    {
    }

    public void tryToAttack()
    {
        distanceBetween = Vector3.Distance(transform.position, bossStats.getbossPosition());
        if (attacking == false && distanceBetween < 8)
        {
            attackingLoop = true;
            attacking = true;
            distanceBetweenLoop = Vector3.Distance(transform.position, bossStats.getbossPosition());
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        while (bossStats.getHitpoints()>1 && attackingLoop == true && distanceBetweenLoop < 8)
        {
            distanceBetweenLoop = Vector3.Distance(transform.position, bossStats.getbossPosition());
            int rand1 = Random.Range(0, projectiles.Length);
            Instantiate(projectiles[rand1], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2.5f);
            distanceBetweenLoop = Vector3.Distance(transform.position, bossStats.getbossPosition());
            if (distanceBetweenLoop < 8)
            {
                int rand = Random.Range(0, projectiles.Length);
                Instantiate(projectiles[rand], transform.position, Quaternion.identity);
            } else if (distanceBetweenLoop > 8)
            {
                attacking = false;
                attackingLoop = false;
            }
        }
    }
}
