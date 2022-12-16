using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{

    [SerializeField] GameObject[] projectiles;
    private int rand;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        while(true){
            yield return new WaitForSeconds(5f);
            rand = Random.Range(0, projectiles.Length);
            Instantiate(projectiles[rand], transform.position, Quaternion.identity);
        }
    }

    public int getAttackStyle()
    {
        return rand;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
