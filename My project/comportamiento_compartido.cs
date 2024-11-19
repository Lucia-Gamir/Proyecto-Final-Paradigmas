using UnityEngine;

public class AddComponentToChildren : MonoBehaviour
{
    // Cambia "YourComponentType" por el componente que deseas añadir
    void Start()
    {
        foreach (Transform child in transform)
        {
            // Añade el componente a cada hijo directamente
            if (child.gameObject.GetComponent<YourComponentType>() == null)
            {
                child.gameObject.AddComponent<YourComponentType>();
            }
        }
    }
}
