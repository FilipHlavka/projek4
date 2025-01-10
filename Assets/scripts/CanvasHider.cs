using UnityEngine;

public class CanvasHider : MonoBehaviour
{
    [SerializeField]
    GameObject Canvas;
    void Update()
    {
        if (Vector3.Distance(transform.position, Camera.main.transform.position) > 80)
        {
            if (Canvas.activeSelf) 
            {
                Canvas.SetActive(false); 
            }
        }
        else
        {
            if (!Canvas.activeSelf) 
            {
                Canvas.SetActive(true); 
            }
        }
    }
}
