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
    [SerializeField] private Texture2D cursor;
    [SerializeField] private Collider2D bossCollider;
    [SerializeField] private BossMinionSpawner spawner;
    private bool spawned = false;
    void Start()
    {
    }

    public Vector3 getbossPosition()
    {
        return transform.position;
    }

    void OnMouseDown()
    {
        playerAttack.tryToAttack(transform.position);
    }

    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }
    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public bool checkPosition(Transform testi)
    {
        return bossCollider.bounds.Contains(testi.position);
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
        if(hitpoints < 150 && spawned == false)
        {
            spawner.spawnHealers();
            spawned = true;
        }
    }
}
