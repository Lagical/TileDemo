using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStats : MonoBehaviour
{
    [SerializeField] private GameStatus gameStatus;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private int hitpoints = 99;
    [SerializeField] private Text bossHP;

    void Start()
    {
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
