using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform ball; // La bola que la cámara sigue.
    public float heightAboveBall = 5f; // Altura fija sobre la bola.
    public float distanceBehindBall = 1f; // Distancia fija en el eje Z detrás de la bola.
    public float rotationSpeed = 5f; // Velocidad de rotación en el eje Y.
    public float verticalRotationSpeed = 2f; // Velocidad de rotación vertical (mirar arriba/abajo).
    public float minVelocityToRotate = 0.1f; // Velocidad mínima de la bola para permitir rotación.
    public float maxVerticalAngle = 80f; // Ángulo máximo para rotación vertical (para evitar volcar la cámara).
    public float headOffset = 2f; // Ajuste para subir la cámara y ver más allá de la bola.

    private Rigidbody ballRigidbody;
    private float currentRotationAngle = 0f; // Ángulo actual de rotación alrededor de la bola (horizontal).
    private float verticalAngle = 0f; // Ángulo actual de rotación en el eje Y (vertical).
    private bool isCameraFrozen = false; // Indica si la cámara está congelada (movimiento detenido).

    void Start()
    {
        if (ball == null)
        {
            Debug.LogError("¡Por favor, asigna la bola al script!");
            return;
        }

        // Obtener el Rigidbody de la bola.
        ballRigidbody = ball.GetComponent<Rigidbody>();

        if (ballRigidbody == null)
        {
            Debug.LogError("¡La bola debe tener un Rigidbody para este script!");
            return;
        }
    }

    void Update()
    {
        if (ball == null || ballRigidbody == null) return;

        // Detectar si la bola está quieta antes de permitir la rotación libre.
        if (ballRigidbody.velocity.magnitude < minVelocityToRotate)
        {
            // Detectar si el clic está presionado para congelar la cámara.
            if (Input.GetMouseButton(0)) // 0 es para el clic izquierdo
            {
                isCameraFrozen = true; // Congelar cámara si el clic está presionado.
            }
            else
            {
                isCameraFrozen = false; // Descongelar cámara cuando el clic se suelta.
            }

            if (!isCameraFrozen)
            {
                // Rotar alrededor de la bola en el eje Y (horizontal).
                float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
                currentRotationAngle += mouseX;

                // Rotar la cámara en el eje X (vertical), limitando la rotación para evitar volcarse.
                float mouseY = Input.GetAxis("Mouse Y") * verticalRotationSpeed;
                verticalAngle -= mouseY;
                verticalAngle = Mathf.Clamp(verticalAngle, -maxVerticalAngle, maxVerticalAngle); // Limitar el rango de rotación vertical.

                // Crear la rotación final combinada en los ejes X (vertical) y Y (horizontal).
                Quaternion rotation = Quaternion.Euler(verticalAngle, currentRotationAngle, 0);
                Vector3 offset = rotation * new Vector3(0, heightAboveBall + headOffset, -distanceBehindBall);

                // Actualizar la posición de la cámara con la rotación calculada.
                transform.position = ball.position + offset;
                transform.LookAt(ball.position);
            }
        }
        else
        {
            // Cuando la bola se está moviendo, hacer que la cámara siga su dirección sin rotar libremente.
            // Posicionar la cámara detrás de la bola, mirando hacia la dirección de la bola.
            Vector3 direction = ballRigidbody.velocity.normalized; // Dirección del movimiento de la bola
            Vector3 behindBallPosition = ball.position + direction * distanceBehindBall + new Vector3(0, heightAboveBall + headOffset, 0);

            // Actualizar la posición de la cámara detrás de la bola.
            transform.position = behindBallPosition;

            // Asegurarse de que la cámara mire hacia la bola (mirar hacia adelante en dirección de la bola).
            transform.LookAt(ball.position);
        }
    }
}
