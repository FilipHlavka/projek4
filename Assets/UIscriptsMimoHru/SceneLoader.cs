using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static SceneLoader Instance;

    public void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Time.timeScale = 1.0f;
    }
    public void Load(string scene)
    {
        
        SceneManager.LoadScene(scene);
    }
}
