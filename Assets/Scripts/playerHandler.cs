using UnityEngine;

public class playerHandler : MonoBehaviour
{
    public GameObject cameraHandler;
    public GameObject camera;
    public GameObject handledBoat;
    // Update is called once per frame
    void Update() {
        Vector3 cameraOffset = new Vector3(0, 153.7f - handledBoat.transform.position.y, -131.4f);
        cameraHandler.transform.position = handledBoat.transform.position + cameraOffset;
        camera.transform.position = cameraHandler.transform.position;
    }
}
