using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target; // Referencia al objeto de la bola
    public Vector3 offset = new Vector3(0, 5, -10); // Distancia de la cámara respecto a la bola

    void Start()
    {
        // Busca la bola por su nombre y la asigna como objetivo
        GameObject ball = GameObject.Find("ball-red");
        if (ball != null)
        {
            target = ball.transform;
        }
        else
        {
            Debug.LogError("No se encontró el objeto 'ball-red' en la escena.");
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Actualiza la posición de la cámara a una distancia específica de la bola
            transform.position = target.position + offset;

            // Opcional: Hace que la cámara mire hacia la bola
            transform.LookAt(target);
        }
    }
}
