using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerByName : MonoBehaviour
{
    // If the player collides with the 3D object
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Read the name of the SceneChanger 3D object
            string sceneName = gameObject.name;

            // Load the scene with that name
            SceneManager.LoadScene(sceneName);
        }
    }
}