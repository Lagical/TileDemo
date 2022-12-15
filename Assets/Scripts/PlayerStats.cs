using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private GameStatus gameStatus;
    [SerializeField] private int hitpoints = 99;
    [SerializeField] private Text playerHP;

    void Start()
    {
    }

    public int getHitpoints()
    {
        return hitpoints;
    }

    public int setHitpoints(int newHitpoints)
    {
        playerHP.text = "PlayerHP: " + newHitpoints.ToString() + "/" + 99;
        return hitpoints = newHitpoints;
    }
    
    void Update()
    {
        if(hitpoints < 1){
            gameStatus.Restart();
        }
    }
}
