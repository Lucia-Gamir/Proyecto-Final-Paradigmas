using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 10f; // Velocidad de movimiento de la bola

    private Rigidbody rb;

    void Start()
    {
        // Obtén el componente Rigidbody de la bola
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Detecta la entrada del teclado
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calcula la dirección del movimiento
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Aplica fuerza al Rigidbody para mover la bola
        rb.AddForce(movement * speed);
    }
}
