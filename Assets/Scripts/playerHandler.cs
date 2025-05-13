using System;
using UnityEngine;

public class playerHandler : MonoBehaviour
{
    public GameObject cameraHandler;
    public GameObject camera;
    public GameObject handledBoat;
    public int whichPlayer = 0;
    // Update is called once per frame
    void Update() {
        Vector3 cameraOffset = new Vector3(0, 102.2f - handledBoat.transform.position.y, -68.4f);
        cameraHandler.transform.position = handledBoat.transform.position + cameraOffset;
        camera.transform.position = cameraHandler.transform.position;
        if(!(whichPlayer == 1 || whichPlayer == 2)) {
            Debug.LogException(new Exception("Player number NOT assigned for playerHandler!"));
        }
    }
}
