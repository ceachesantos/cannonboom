using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBarController : MonoBehaviour
{
    public CannonController cannonController;
    private bool isMousePressed = false; // Indica si el botón del ratón está siendo presionado
    private float pressStartTime; // Tiempo en el que se inició la pulsación del ratón
    private float maxHoldTime = 5f; // Tiempo máximo que se puede sostener el clic
    public Slider progressBar; // Referencia al objeto Slider que actuará como la barra de progreso
    public TextMeshProUGUI timeText; // Referencia al objeto Text para mostrar el tiempo actual

    void Start()
    {
        if (progressBar == null)
        {
            Debug.LogError("Referencia a Slider no asignada en el inspector.");
        }

        if (timeText == null)
        {
            Debug.LogError("Referencia a Text no asignada en el inspector.");
        }
    }

    void Update()
    {
        // Actualiza el tiempo actual sostenido y la barra de progreso
        UpdateHoldTime();
    }

    void UpdateHoldTime()
    {
        maxHoldTime = cannonController.maxHoldTime;
        // Si el botón del ratón está siendo presionado
        if (Input.GetMouseButtonDown(0))
        {
            isMousePressed = true;
            pressStartTime = Time.time;
        }
        // Si el botón del ratón está siendo soltado
        else if (Input.GetMouseButtonUp(0))
        {
            isMousePressed = false;
        }

        // Si el botón del ratón está siendo presionado, actualiza la barra de progreso
        if (isMousePressed && progressBar != null && timeText != null)
        {
            // Calcula el tiempo actual sostenido
            float holdTime = Time.time - pressStartTime;

            // Limita el tiempo actual sostenido al máximo permitido
            holdTime = Mathf.Min(holdTime, maxHoldTime);

            // Actualiza el texto mostrando el tiempo actual
            timeText.text = "Tiempo actual: " + holdTime.ToString("F2") + "s";

            // Calcula el progreso de la barra en función del tiempo actual sostenido
            float progress = Mathf.Clamp01(holdTime / maxHoldTime);

            // Actualiza el valor de la barra de progreso
            progressBar.value = progress;
        }
        Debug.Log("maxHoldTime: " + maxHoldTime);
        //Debug.Log("maxHoldTime: " + maxHoldTime);
    }
}
