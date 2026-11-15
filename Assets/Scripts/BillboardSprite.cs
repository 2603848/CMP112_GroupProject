using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class BillboardSprite : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Camera camera = UnityEngine.Camera.main;

        if (!EditorApplication.isPlaying && !camera) 
             camera = UnityEngine.Camera.current;
        
        if (camera != null)
            transform.rotation = Quaternion.Euler(new Vector3(0, camera.transform.rotation.eulerAngles.y, 0));
    }
}
