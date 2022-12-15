using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{

    [SerializeField] Tilemap ground;
    [SerializeField] Tilemap wall;
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

    IEnumerator Example(Vector2 direction)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (CanMove(direction))
            {
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
            //StartCoroutine(Example(direction));
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
