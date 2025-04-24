using UnityEngine;

public class playerController : MonoBehaviour
{
    private float kääntyvyys = 0;
    private float kiihtyvyys = 0;
    public float maxKiihtyvyys = 1;
    public float maxKääntyvyys = 1;

    private bool isInWater = false;
    public Rigidbody shipRB;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey("w") && kiihtyvyys < maxKiihtyvyys) {
            kiihtyvyys += maxKiihtyvyys / 10;
        }
        if(Input.GetKey("s") && kiihtyvyys > 0) {
            kiihtyvyys -= maxKiihtyvyys / 10;
            if(kiihtyvyys < 0) kiihtyvyys = 0;
        }
        print(kiihtyvyys);
        if(kääntyvyys > maxKääntyvyys && kääntyvyys < -maxKääntyvyys) {
            if(Input.GetKey(KeyCode.A)) {
                kääntyvyys += maxKääntyvyys / 10;
                print("Kääntyy!");
            }
            if(Input.GetKey(KeyCode.D)) {
                kääntyvyys -= maxKääntyvyys / 10;
                print("Kääntyy!");
            }
        }
        shipRB.AddForce(transform.forward * kiihtyvyys, ForceMode.Force);
        if(isInWater) 
            shipRB.AddForce(transform.up * (((0 - transform.position.y))*((0 - transform.position.y))), ForceMode.Force);
        if(transform.position.y > 1)
            shipRB.AddForce(transform.up * -1, ForceMode.VelocityChange);
    }
    void OnTriggerEnter(Collider collider) {
        if(collider.tag == "Water") {
            isInWater = true;
        }
    }
        void OnTriggerExit(Collider collider) {
        if(collider.tag == "Water") {
            isInWater = false;
        }
    }
}
