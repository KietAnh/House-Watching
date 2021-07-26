using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeCameraTarget : MonoBehaviour
{
    public Transform cam;
    public Transform[] views;
    public float transitionSpeed = 1f;

    public Transform currentView;

    public Dropdown dropdown;

    static public bool changeView = false;

    void Update()
    {
        if (changeView == true)
        {
            int dropdownValue = dropdown.value;
            currentView = views[dropdownValue];
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (changeView == true)
        {
            // Lerp position
            cam.position = Vector3.Lerp(cam.position, currentView.position, Time.deltaTime * transitionSpeed);

            Vector3 currentAngle = new Vector3( // cam.parent -> cam
                Mathf.LerpAngle(cam.parent.rotation.eulerAngles.x, currentView.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
                Mathf.LerpAngle(cam.parent.rotation.eulerAngles.y, currentView.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
                Mathf.LerpAngle(cam.parent.rotation.eulerAngles.z, currentView.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed)
            );

            cam.parent.eulerAngles = currentAngle;
            if (Vector3.Distance(cam.position, currentView.position) < 0.001f)
            {
                changeView = false;
            }

            CameraOrbit.localRotation = new Vector3(cam.parent.rotation.eulerAngles.y, cam.parent.rotation.eulerAngles.x, 0);
        }
    }

    public void ChangeView()
    {
        changeView = true;
    }
}
