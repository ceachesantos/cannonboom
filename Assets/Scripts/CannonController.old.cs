/*using UnityEngine;
using TMPro;

public class CannonController : MonoBehaviour
{
    public GameObject cannonballPrefab; // Prefab de la bola de cañón
    public Transform cannonTransform;   // Transform del cañón

    public float verticalFactor = 2f; // Ajusta este valor para cambiar la inclinación
    public float baseProjectileSpeed = 10f; // Velocidad base del proyectil
    public float maxProjectileSpeed = 20f; // Velocidad máxima del proyectil
    public float maxHoldTime = 5f; // Tiempo máximo que se puede sostener el clic
    private float shootTime; // Tiempo en el que se inició el disparo

    public TextMeshProUGUI timeText; // Referencia al objeto Text para mostrar el tiempo

    void Start()
    {
        if (timeText == null)
        {
            Debug.LogError("Referencia al objeto Text no asignada.");
        }
        else
        {
            timeText.text = "Tiempo pulsado: 0.00s";
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detecta clic izquierdo al presionar
        {
            // Al iniciar el clic, guarda el tiempo actual
            shootTime = Time.time;
        }

        if (Input.GetMouseButtonUp(0)) // Detecta clic izquierdo al soltar
        {
            // Calcula cuánto tiempo ha pasado desde que se inició el clic
            float holdTime = Time.time - shootTime;

            // Actualiza el texto mostrando el tiempo pulsado
            if (timeText != null)
            {
                timeText.text = "Tiempo pulsado: " + holdTime.ToString("F2") + "s";
            }

            // Ajusta la velocidad del proyectil en función del tiempo sostenido
            float currentProjectileSpeed = CalculateProjectileSpeed(holdTime);

            // Llama a la función Shoot con la velocidad del proyectil actual
            Shoot(currentProjectileSpeed);
        }
    }

    float CalculateProjectileSpeed(float holdTime)
    {
        // Ajusta la velocidad del proyectil en función del tiempo sostenido
        float percentage = Mathf.Clamp01(holdTime / maxHoldTime);
        float adjustedSpeed = baseProjectileSpeed + percentage * (maxProjectileSpeed - baseProjectileSpeed);
        return Mathf.Min(adjustedSpeed, maxProjectileSpeed);
    }

    void Shoot(float projectileSpeed)
    {
        if (cannonballPrefab != null && cannonTransform != null)
        {
            // Obtiene la posición del ratón en la pantalla y convierte a un rayo en el espacio del juego
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Crea la bola de cañón en la posición y rotación del cañón
            GameObject cannonball = Instantiate(cannonballPrefab, cannonTransform.position, cannonTransform.rotation);
            Rigidbody cannonballRb = cannonball.GetComponent<Rigidbody>();

            if (cannonballRb != null)
            {
                // Obtiene la dirección del disparo desde el rayo y ajusta la inclinación
                Vector3 shootDirection = ray.direction + Vector3.up * verticalFactor;

                // Normaliza la dirección y aplica fuerza al proyectil
                cannonballRb.AddForce(shootDirection.normalized * projectileSpeed, ForceMode.VelocityChange);
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
}*/