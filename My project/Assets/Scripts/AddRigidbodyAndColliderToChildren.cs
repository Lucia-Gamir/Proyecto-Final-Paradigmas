using UnityEngine;

public class AddRigidbodyAndColliderToChildren : MonoBehaviour
{
    void Start()
    {
        // A�ade los componentes al propio prefab
        AddComponents(gameObject);

        // Itera por todos los objetos hijo y les a�ade los componentes
        foreach (Transform child in transform)
        {
            AddComponents(child.gameObject);
        }
    }

    // M�todo para agregar Rigidbody y BoxCollider si no existen, con configuraciones espec�ficas
    private void AddComponents(GameObject obj)
    {
        // A�adir y configurar Rigidbody
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = obj.AddComponent<Rigidbody>();
            rb.isKinematic = true;       // Activar isKinematic
            rb.useGravity = false;       // Desactivar gravedad
        }

        // A�adir BoxCollider si no existe
        if (obj.GetComponent<BoxCollider>() == null)
        {
            obj.AddComponent<BoxCollider>();
        }
    }
}
