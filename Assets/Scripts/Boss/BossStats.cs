using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossStats : MonoBehaviour
{
    [SerializeField] private GameStatus gameStatus;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private int hitpoints;
    [SerializeField] private TextMeshProUGUI bossHP;
    void Start()
    {
    }

    public Vector3 getbossPosition()
    {
        return transform.position;
    }

    void OnMouseDown()
    {
        playerAttack.tryToAttack();
    }

    public int getHitpoints()
    {
        return hitpoints;
    }

    public int setHitpoints(int newHitpoints)
    {
        bossHP.text = "BossHP: " + newHitpoints.ToString() + "/" + 250;
        return hitpoints = newHitpoints;
    }

    void Update()
    {
        if (hitpoints < 1)
        {
            gameStatus.Restart();
        }
    }
}
