using UnityEngine;

public class BoxSelector : MonoBehaviour
{
    Camera cam;
    [SerializeField]
    RectTransform boxVisual;

    Rect box;
    Vector2 stPosition = Vector2.zero;
    Vector2 endPosition = Vector2.zero;
    
    void Start()
    {
        cam = Camera.main;
        Visualize();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            stPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            endPosition = Input.mousePosition;
            SelectorBox();
            Visualize();
        }

        if (Input.GetMouseButtonUp(0))
        {
            CheckForUnits();
            stPosition = Vector2.zero;
            endPosition = Vector2.zero;
            Visualize();
        }
    }

    void Visualize()
    {
        Vector2 startBoxPosition = stPosition;
        Vector2 endBoxPosition = endPosition;
        Vector2 center = (startBoxPosition + endBoxPosition) / 2;

        // Calculate the size of the selection box
        Vector2 size = new Vector2(Mathf.Abs(startBoxPosition.x - endBoxPosition.x), Mathf.Abs(startBoxPosition.y - endBoxPosition.y));

        // Set box position and size
        box.position = new Vector2(Mathf.Min(startBoxPosition.x, endBoxPosition.x), Mathf.Min(startBoxPosition.y, endBoxPosition.y));
        box.size = size;

        // Adjust the visual box position and size
        boxVisual.anchoredPosition = center - (boxVisual.parent as RectTransform).sizeDelta / 2f;
        boxVisual.sizeDelta = size;
    }

    void SelectorBox()
    {
        // Define the box boundaries based on mouse position
        box.xMin = Mathf.Min(stPosition.x, Input.mousePosition.x);
        box.xMax = Mathf.Max(stPosition.x, Input.mousePosition.x);
        box.yMin = Mathf.Min(stPosition.y, Input.mousePosition.y);
        box.yMax = Mathf.Max(stPosition.y, Input.mousePosition.y);
    }

    void CheckForUnits()
    {
        foreach (Movement u in MovementController.instance.units)
        {
            // Convert unit's world position to screen position
            Vector2 screenPosition = cam.WorldToScreenPoint(u.transform.position);

            // Check if the screen position is within the selection box
            if (box.Contains(screenPosition))
            {
                Debug.Log("Unit selected");

                // Add unit to selected units list
                MovementController.instance.AddToListBox(u);
                MovementController.instance.unselect = true;
            }
          
        }
    }
}
