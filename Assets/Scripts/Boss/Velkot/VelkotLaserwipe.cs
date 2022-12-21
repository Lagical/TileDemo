using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class VelkotLaserwipe : MonoBehaviour
{

    //EERO

    [SerializeField] private float speed = 10f;
    public LineRenderer lineRenderer;
    [SerializeField] private PlayerStats playerStats;

    Renderer walls;
    private Vector2 middleLeftPoint;
    private Vector2 middleRightPoint;
    private Vector2 bottomRightPoint;
    private Vector2 bottomLeftPoint;

    private Vector2 laserPos;
    
    private GameObject boss;
    public bool laserEnabled = false;
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        walls = GameObject.FindGameObjectWithTag("Wall").GetComponent<Renderer>();
        boss = GameObject.FindGameObjectWithTag("Boss");
        float x = walls.localBounds.size.x;
        float y = walls.localBounds.size.y;
        Vector3 wallBoundsCenter = walls.localBounds.center;

        float rightSideX = wallBoundsCenter.x + x / 2 - 1;
        float leftSideX = wallBoundsCenter.x - x / 2 + 1;
        //float topSideY = wallBoundsCenter.y + y / 2 - 1;
        float bottomSideY = wallBoundsCenter.y - y / 2 + 1;

        middleLeftPoint = new Vector2(leftSideX, wallBoundsCenter.y);
        middleRightPoint = new Vector2(rightSideX, wallBoundsCenter.y);
        bottomRightPoint = new Vector2(rightSideX, bottomSideY);
        bottomLeftPoint = new Vector2(leftSideX, bottomSideY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int damage = 1;
            int hpAfterDmg = playerStats.getHitpoints() - damage;
            playerStats.setHitpoints(hpAfterDmg);
        }
    }


    public IEnumerator RightSwipe()
    {
        enableLaser();
        laserPos = new Vector2(middleLeftPoint.x - boss.transform.position.x, middleLeftPoint.y - boss.transform.position.y);
        lineRenderer.SetPosition(0, Vector2.zero);
        lineRenderer.SetPosition(1, laserPos);
        while (laserPos.y > bottomLeftPoint.y - boss.transform.position.y)
        {
            moveLaserDown();
            yield return new WaitForSeconds(0.001f);
        }
        while (laserPos.x < bottomRightPoint.x - boss.transform.position.x)
        {
            moveLaserRight();
            yield return new WaitForSeconds(0.001f);
        }
        while (laserPos.y < middleRightPoint.y - boss.transform.position.y)
        {
            moveLaserUp();
            yield return new WaitForSeconds(0.001f);
        }
        disableLaser();
    }

    private void moveLaserDown()
    {
        laserPos.y -= speed * Time.deltaTime;
        lineRenderer.SetPosition(1, laserPos);
    }
    private void moveLaserUp()
    {
        laserPos.y += speed * Time.deltaTime;
        lineRenderer.SetPosition(1, laserPos);
    }
    private void moveLaserRight()
    {
        laserPos.x += speed * Time.deltaTime;
        lineRenderer.SetPosition(1, laserPos);
    }
    private void moveLaserLeft()
    {
        laserPos.x -= speed * Time.deltaTime;
        lineRenderer.SetPosition(1, laserPos);
    }

    private void enableLaser()
    {
        laserEnabled = true;
        lineRenderer.enabled = true;
    }
    private void disableLaser()
    {
        lineRenderer.SetPosition(1, Vector3.zero);
        laserEnabled = false;
        lineRenderer.enabled = false;
    }
}
