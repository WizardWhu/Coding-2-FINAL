using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int currentSceneIndex;

    public void OnPressR(InputAction.CallbackContext context)
    {
        if (context.performed) ReloadScene();
    }
    private void Awake()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

}
