using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Tilemap ground;
    [SerializeField] private Tilemap wall;
    [SerializeField] private PlayerStats playerStats;
    private PlayerMovement controls;

    private void Awake()
    {
        controls = new PlayerMovement();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
    void Start()
    {
        controls.Main.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }

    IEnumerator AutoMove(Vector2 direction)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (CanMove(direction))
            {
                playerStats.setStamina(1);
                transform.position += (Vector3)direction;
            }
        }
    }

    private void Move(Vector2 direction)
    {
        //StopAllCoroutines();
        if (CanMove(direction))
        {
            transform.position += (Vector3)direction;
            playerStats.setStamina(1);
            //StartCoroutine(AutoMove(direction));
        }
    }

    private bool CanMove(Vector2 direction)
    {
        Vector3Int gridPosition = ground.WorldToCell(transform.position + (Vector3)direction);
        if (!ground.HasTile(gridPosition) || wall.HasTile(gridPosition))
        {
            return false;
        } else
        {
            return true;
        }
    }
}
