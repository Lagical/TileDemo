using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{

    [SerializeField] GameObject[] projectiles;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        while(true){
            yield return new WaitForSeconds(5f);
            int rand = Random.Range(0, projectiles.Length);
            Instantiate(projectiles[rand], transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
