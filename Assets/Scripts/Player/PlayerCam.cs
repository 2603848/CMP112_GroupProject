#region Eren Coded This part
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerCam : MonoBehaviour
{
    //Sensitivity
    public float sensX = 150f;
    public float sensY = 150f;

    public Transform orientation;
    public Transform camHolder;

    private Camera cam;

    //FOV and Tilt
    public float fovNormal = 80f;

    private float targetXRotation, targetYRotation, currentXRotation, currentYRotation;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        MouseRotation();
    }

    #region MouseRotation
    private void MouseRotation() 
    { 
        //Get looking input (mouse input)
        float mouseX = Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;
        //There is technically no point to calculating y rotation as sensY is permanently set to 0.
        //However good to have incase it is needed in the future.
        targetYRotation += mouseX;
        targetXRotation -= mouseY;
        targetXRotation = Mathf.Clamp(targetXRotation, -90f, 90f);

        targetXRotation = Mathf.MoveTowards(currentXRotation, targetXRotation, 30f);
        targetYRotation = Mathf.MoveTowards(currentYRotation, targetYRotation, 30f);

        currentXRotation = Mathf.Lerp(currentXRotation, targetXRotation, Time.deltaTime * 15f);
        currentYRotation = Mathf.Lerp(currentYRotation, targetYRotation, Time.deltaTime * 15f);

        camHolder.localRotation = Quaternion.Euler(currentXRotation, 0f, 0f);
        orientation.localRotation = Quaternion.Euler(0f, currentYRotation, 0f);
    }
    #endregion
}
#endregion