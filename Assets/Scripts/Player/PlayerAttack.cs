using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject[] projectiles;
    [SerializeField] BossStats bossStats;
    [SerializeField] SpriteRenderer attackRangeIndicator;
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
        if(distanceBetween > 8)
        {
            StartCoroutine(rangeIndicator());
        }
        if (attacking == false && distanceBetween < 8)
        {
            attackingLoop = true;
            attacking = true;
            distanceBetweenLoop = Vector3.Distance(transform.position, bossStats.getbossPosition());
            StartCoroutine(Attack());
        }
    }

    private IEnumerator rangeIndicator()
    {
        attackRangeIndicator.enabled = true;
        yield return new WaitForSeconds(0.25f);
        attackRangeIndicator.enabled = false;
    }

    private IEnumerator Attack()
    {
        while (bossStats.getHitpoints()>1 && attackingLoop == true && distanceBetweenLoop < 8)
        {
            distanceBetweenLoop = Vector3.Distance(transform.position, bossStats.getbossPosition());
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
