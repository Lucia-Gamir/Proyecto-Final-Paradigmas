using UnityEngine;

public class AddRigidbodyAndColliderToChildren : MonoBehaviour
{
    void Start()
    {
        // Añade los componentes al propio prefab
        AddComponents(gameObject);

        // Itera por todos los objetos hijo y les añade los componentes
        foreach (Transform child in transform)
        {
            AddComponents(child.gameObject);
        }
    }

    // Método para agregar Rigidbody y BoxCollider si no existen, con configuraciones específicas
    private void AddComponents(GameObject obj)
    {
        // Añadir y configurar Rigidbody
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = obj.AddComponent<Rigidbody>();
            rb.isKinematic = true;       // Activar isKinematic
            rb.useGravity = false;       // Desactivar gravedad
        }

        // Añadir BoxCollider si no existe
        if (obj.GetComponent<BoxCollider>() == null)
        {
            obj.AddComponent<BoxCollider>();
        }
    }
}
