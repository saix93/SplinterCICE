using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : SoldierShooting
{
    [SerializeField]
    LayerMask cameraRaycastLayerMask;

    PlayerMovement playerMovement;
    PlayerInteraction playerInteraction;
    CursorManager cursorManager;
    Player player;

    protected override void Awake()
    {
        base.Awake();
        playerMovement = this.GetComponent<PlayerMovement>();
        playerInteraction = this.GetComponent<PlayerInteraction>();
        cursorManager = Resources.Load<CursorManager>("CursorManager");
        player = GetComponent<Player>();
    }

    private void Update()
    {
        CheckAiming();

        // Actualiza el peso de la animación de apuntado
        UpdateAimingWeight();
    }

    void CheckAiming()
    {
        // Si se mantiene pulsado click derecho...
        if (Input.GetMouseButton(1) && !playerInteraction.IsInteracting())
        {
            // ...se comienza a apuntar y si no estaba apuntando...
            if (!aiming)
            {
                // ...comienza a apuntar y detiene su movimiento
                aiming = true;
                playerMovement.StopMoving();
            }

            // Mira hacia el punto donde está el ratón
            Ray cameraRaycast = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(cameraRaycast, out hitInfo, Mathf.Infinity, cameraRaycastLayerMask))
            {
                Vector3 aimingPoint = hitInfo.point;
                aimingPoint.y = this.transform.position.y;
                playerMovement.SetRotation(targetForward: aimingPoint - this.transform.position);
                // TODO: Preparar animaciones para la rotación del personaje

                // Si está apuntando a un enemigo...
                if (hitInfo.collider.CompareTag("Enemy"))
                {
                    if (player.CanSeeTarget(hitInfo.collider.GetComponent<Soldier>()))
                    {
                        // ...se pone el cursor de ataque y, al hacer click, mata al enemigo
                        cursorManager.SetCursorType(ECursorType.Attack);

                        if (Input.GetMouseButtonDown(0))
                        {
                            hitInfo.collider.GetComponent<Soldier>().Die();
                        }
                    }
                    else
                    {
                        cursorManager.SetCursorType(ECursorType.Forbidden);
                    }
                }
                else
                {
                    // Se vuelve al cursor por defecto
                    cursorManager.SetCursorType(ECursorType.None);
                }
            }
        }
        else
        {
            // ...deja de apuntar, se vuelve al cursor por defect
            if (aiming)
            {
                cursorManager.SetCursorType(ECursorType.None);
                aiming = false;
            }
        }
    }
}
