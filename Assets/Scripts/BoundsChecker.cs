using Unity.VisualScripting;
using UnityEngine;

// Destroys objects that escape the world boundaries
public class BoundsChecker : MonoBehaviour
{
    void Update()
    {
        if (GameController.WorldBounds.Contains(this.transform.position) == false)
        {
            Destroy(this.gameObject);
        }
    }
}
