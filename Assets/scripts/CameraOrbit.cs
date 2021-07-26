using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraOrbit : MonoBehaviour
{
    Transform camera;
    Transform parent;

    static public Vector3 localRotation = new Vector3(-90, 15, 0);
    float cameraDistance = 55f;

    public float MouseSensitivity = 4f;
    public float ScrollSensitivity = 2f;
    public float OrbitDampening = 10f;
    public float ScrollDampening = 6f;

    public bool CameraDisabled = true;

    void Start()
    {
        camera = transform;
        parent = transform.parent;
    }

    void LateUpdate()
    {
        if (IsPointerOverUIObject())
            return;

        if (Input.GetMouseButton(0))
        {
            CameraDisabled = false;
            changeCameraTarget.changeView = false;
        }
        else
            CameraDisabled = true;

        if (!CameraDisabled)
        {
            // Rotation of the Camera based on Mouse Coordinates
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                localRotation.x += Input.GetAxis("Mouse X") * MouseSensitivity;
                localRotation.y -= Input.GetAxis("Mouse Y") * MouseSensitivity;

                // Clamp the y rotation to horizon and not flipping over at the top
                localRotation.y = Mathf.Clamp(localRotation.y, -90f, 90f);
            }

            // Actual Camera Rig Transformations
            Quaternion QT = Quaternion.Euler(localRotation.y, localRotation.x, 0);
            Debug.Log(localRotation.y);
            Debug.Log(localRotation.x);
            parent.rotation = Quaternion.Lerp(parent.rotation, QT, Time.deltaTime * OrbitDampening);
        }

        /*
        cameraDistance = Mathf.Abs(camera.localPosition.z);
        

        // Zooming input from our mouse scroll wheel
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitivity;

            // Makes camera zoom faster the further away it is from the target
            ScrollAmount *= cameraDistance * 0.3f;

            cameraDistance += ScrollAmount * -1f;

            // This makes camera go no closer than 1.5 meters from target, and no further than 100 meters
            cameraDistance = Mathf.Clamp(cameraDistance, 1.5f, 100f);
        }

        if (camera.localPosition.z != cameraDistance * -1f)
        {
            camera.localPosition = new Vector3(camera.localPosition.x, camera.localPosition.y, Mathf.Lerp(camera.localPosition.z, cameraDistance * -1f, Time.deltaTime * ScrollDampening));
        }*/
        
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
