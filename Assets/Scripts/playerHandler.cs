using System;
using UnityEngine;

public class playerHandler : MonoBehaviour
{
    public GameObject cameraHandler;
    public GameObject cameraObject;
    public GameObject handledBoat;
    public float smoothSpeed = 0.125f;
    public int whichPlayer = 0;
    void LateUpdate() {
        if (this.transform.GetChild(1).gameObject.GetComponent<playerController>().firstDeath)
        {
            Vector3 desiredPosition = new Vector3(0 - handledBoat.transform.position.x, 90.2f - handledBoat.transform.position.y, -100.4f);
            cameraHandler.transform.position = handledBoat.transform.position + desiredPosition;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, cameraHandler.transform.position, smoothSpeed);
            cameraObject.transform.position = smoothedPosition;
            cameraObject.transform.LookAt(handledBoat.transform);
        }
        if (!(whichPlayer == 1 || whichPlayer == 2))
                Debug.LogException(new Exception("Player number NOT assigned for playerHandler!"));
        
    }
}
