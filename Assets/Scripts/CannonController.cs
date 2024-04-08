using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class CannonController : MonoBehaviour
{
    public GameObject cannonballPrefab; // Prefab de la bola de cañón
    public GameObject cannon;          // Transform del cañón
    public Transform shootingPoint;     // Transform del punto de disparo
    public float cannonRotationSpeed = 30f;   // Velocidad de rotación del cañón  
    public float baseProjectileSpeed = 10f; // Velocidad base del proyectil
    public float maxProjectileSpeed = 20f; // Velocidad máxima del proyectil
    public float maxHoldTime = 5f;         // Tiempo máximo que se puede sostener el clic
    private float shootTime;               // Tiempo en el que se inició el disparo
    public int cannonball_destroy_after_time = 0;  // Tiempo en segundos en el que se destruirá la bola de cañón, si es 0, infinito

    //Audio clips
    public AudioClip boomSound;
    public AudioClip holdingSound;
    public TextMeshProUGUI timeText; // Referencia al objeto Text para mostrar el tiempo

    public Camera camera;
    public int yPosition = 7;
    public int zPosition = 4;

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

            // Reproducir el sonido de "y hace..."
            if (holdingSound != null)
            {
                AudioSource.PlayClipAtPoint(holdingSound, shootingPoint.position);
            }
            else
            {
                Debug.LogError("AudioClip no asignado para el sonido de explosión.");
            }
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
        // Rotación del cañón
        RotateCannon();
    }

    float CalculateProjectileSpeed(float holdTime)
    {
        // Ajusta la velocidad del proyectil en función del tiempo sostenido
        float percentage = Mathf.Clamp01(holdTime / maxHoldTime);
        float adjustedSpeed = baseProjectileSpeed + percentage * (maxProjectileSpeed - baseProjectileSpeed);
        return Mathf.Min(adjustedSpeed, maxProjectileSpeed);
    }

void RotateCannon()
{
    float horizontalInput = Input.GetAxis("Horizontal");
    float verticalInput = Input.GetAxis("Vertical");
    
    // Rota el cañón en función de la entrada del usuario
    cannon.transform.Rotate(Vector3.right, verticalInput * cannonRotationSpeed * Time.deltaTime);
    cannon.transform.Rotate(Vector3.up, horizontalInput * cannonRotationSpeed * Time.deltaTime);

    // Calcula la posición detrás del cañón
    Vector3 desiredCameraPosition = cannon.transform.position - cannon.transform.forward * yPosition + Vector3.up * zPosition;

    // Establece la posición deseada de la cámara
    camera.transform.position = desiredCameraPosition;

    // Establece la rotación de la cámara para que mire hacia el cañón
    camera.transform.rotation = Quaternion.Euler(cannon.transform.rotation.eulerAngles.x, cannon.transform.rotation.eulerAngles.y, 0f);
}

    void Shoot(float projectileSpeed)
    {
        if (cannonballPrefab != null && shootingPoint != null)
        {
            // Crea la bola de cañón en la posición del punto de disparo
            GameObject cannonball = Instantiate(cannonballPrefab, shootingPoint.position, Quaternion.identity);
            Rigidbody cannonballRb = cannonball.GetComponent<Rigidbody>();

            if (cannonballRb != null)
            {
                // Obtiene la dirección del disparo como la dirección desde el punto de disparo hacia adelante
                Vector3 shootDirection = (shootingPoint.position - cannon.transform.position).normalized;

                // Aplica fuerza al proyectil en la dirección obtenida
                cannonballRb.AddForce(shootDirection * projectileSpeed, ForceMode.VelocityChange);
                if (cannonball_destroy_after_time != 0) Destroy(cannonball, 10f);

                // Reproducir el sonido de BOOM
                if (boomSound != null)
                {
                    //AudioSource.PlayAudioSource(holdingSound); //TODO: Stop sound
                    AudioSource.PlayClipAtPoint(boomSound, shootingPoint.position);
                }
                else
                {
                    Debug.LogError("AudioClip no asignado para el sonido de explosión.");
                }
            }
            else
            {
                Debug.LogError("Rigidbody no encontrado en el prefab de la bola de cañón.");
            }
        }
        else
        {
            Debug.LogError("Prefab de la bola de cañón o punto de disparo no asignados.");
        }
    }

}
