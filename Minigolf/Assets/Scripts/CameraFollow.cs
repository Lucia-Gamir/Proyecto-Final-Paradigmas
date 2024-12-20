using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform ball; // La bola que la c�mara sigue.
    public float heightAboveBall = 5f; // Altura fija sobre la bola.
    public float distanceBehindBall = 1f; // Distancia fija en el eje Z detr�s de la bola.
    public float rotationSpeed = 5f; // Velocidad de rotaci�n en el eje Y.
    public float verticalRotationSpeed = 2f; // Velocidad de rotaci�n vertical (mirar arriba/abajo).
    public float minVelocityToRotate = 0.1f; // Velocidad m�nima de la bola para permitir rotaci�n.
    public float maxVerticalAngle = 80f; // �ngulo m�ximo para rotaci�n vertical (para evitar volcar la c�mara).
    public float headOffset = 2f; // Ajuste para subir la c�mara y ver m�s all� de la bola.

    private Rigidbody ballRigidbody;
    private float currentRotationAngle = 0f; // �ngulo actual de rotaci�n alrededor de la bola (horizontal).
    private float verticalAngle = 0f; // �ngulo actual de rotaci�n en el eje Y (vertical).
    private bool isCameraFrozen = false; // Indica si la c�mara est� congelada (movimiento detenido).

    void Start()
    {
        if (ball == null)
        {
            Debug.LogError("�Por favor, asigna la bola al script!");
            return;
        }

        // Obtener el Rigidbody de la bola.
        ballRigidbody = ball.GetComponent<Rigidbody>();

        if (ballRigidbody == null)
        {
            Debug.LogError("�La bola debe tener un Rigidbody para este script!");
            return;
        }
    }

    void Update()
    {
        if (ball == null || ballRigidbody == null) return;

        // Detectar si la bola est� quieta antes de permitir la rotaci�n libre.
        if (ballRigidbody.velocity.magnitude < minVelocityToRotate)
        {
            // Detectar si el clic est� presionado para congelar la c�mara.
            if (Input.GetMouseButton(0)) // 0 es para el clic izquierdo
            {
                isCameraFrozen = true; // Congelar c�mara si el clic est� presionado.
            }
            else
            {
                isCameraFrozen = false; // Descongelar c�mara cuando el clic se suelta.
            }

            if (!isCameraFrozen)
            {
                // Rotar alrededor de la bola en el eje Y (horizontal).
                float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
                currentRotationAngle += mouseX;

                // Rotar la c�mara en el eje X (vertical), limitando la rotaci�n para evitar volcarse.
                float mouseY = Input.GetAxis("Mouse Y") * verticalRotationSpeed;
                verticalAngle -= mouseY;
                verticalAngle = Mathf.Clamp(verticalAngle, -maxVerticalAngle, maxVerticalAngle); // Limitar el rango de rotaci�n vertical.

                // Crear la rotaci�n final combinada en los ejes X (vertical) y Y (horizontal).
                Quaternion rotation = Quaternion.Euler(verticalAngle, currentRotationAngle, 0);
                Vector3 offset = rotation * new Vector3(0, heightAboveBall + headOffset, -distanceBehindBall);

                // Actualizar la posici�n de la c�mara con la rotaci�n calculada.
                transform.position = ball.position + offset;
                transform.LookAt(ball.position);
            }
        }
        else
        {
            // Cuando la bola se est� moviendo, hacer que la c�mara siga su direcci�n sin rotar libremente.
            // Posicionar la c�mara detr�s de la bola, mirando hacia la direcci�n de la bola.
            Vector3 direction = ballRigidbody.velocity.normalized; // Direcci�n del movimiento de la bola
            Vector3 behindBallPosition = ball.position + direction * distanceBehindBall + new Vector3(0, heightAboveBall + headOffset, 0);

            // Actualizar la posici�n de la c�mara detr�s de la bola.
            transform.position = behindBallPosition;

            // Asegurarse de que la c�mara mire hacia la bola (mirar hacia adelante en direcci�n de la bola).
            transform.LookAt(ball.position);
        }
    }
}
