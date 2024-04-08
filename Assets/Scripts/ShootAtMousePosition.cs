using UnityEngine;

public class ShootAtMousePosition : MonoBehaviour
{
    public GameObject cannonballPrefab; // Prefab de la bola de cañón
    public Transform cannonTransform;   // Transform del cañón

    public float projectileSpeed = 10f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detecta clic izquierdo
        {
            AimAtMouse();
            Shoot();
        }
    }

    void AimAtMouse()
    {
        if (Camera.main != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Vector3 targetDirection = hit.point - cannonTransform.position;
                targetDirection.y = 0;

                if (targetDirection != Vector3.zero)
                {
                    cannonTransform.rotation = Quaternion.LookRotation(targetDirection.normalized);
                }
            }
        }
        else
        {
            Debug.LogError("Cámara principal no encontrada. Asigna la cámara manualmente en el inspector.");
        }
    }

    void Shoot()
    {
        if (cannonballPrefab != null && cannonTransform != null)
        {
            // Crea la bola de cañón en la posición y rotación del cañón
            GameObject cannonball = Instantiate(cannonballPrefab, cannonTransform.position, cannonTransform.rotation);
            Rigidbody cannonballRb = cannonball.GetComponent<Rigidbody>();

            if (cannonballRb != null)
            {
                // Aplica fuerza al proyectil en la dirección del cañón, considerando la inclinación
                cannonballRb.AddForce(cannonTransform.forward * projectileSpeed, ForceMode.VelocityChange);
            }
            else
            {
                Debug.LogError("Rigidbody no encontrado en el prefab de la bola de cañón.");
            }
        }
        else
        {
            Debug.LogError("Prefab de la bola de cañón o transform del cañón no asignados.");
        }
    }
}
