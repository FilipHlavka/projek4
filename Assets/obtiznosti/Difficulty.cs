using UnityEngine;

public class Difficulty : MonoBehaviour
{
    [SerializeField]
    public int diff = 0;
    public static Difficulty Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    

    public void ChangeDifficulty(int index)
    {
        diff = index;
    }
}
