using UnityEngine;

public class playerController : MonoBehaviour
{
    private float kääntyvyys = 0;
    private float kiihtyvyys = 0;
    public float maxKiihtyvyys = 10;
    public float maxKääntyvyys = 1;

    private bool isInWater = false;
    public Rigidbody shipRB;


    void FixedUpdate()
    {
        // -------- KIIHTYVYYS -------------
        if(Input.GetKey("w") && kiihtyvyys < maxKiihtyvyys) {
            kiihtyvyys += maxKiihtyvyys / 15;
        }
        if(Input.GetKey("s") && kiihtyvyys > 0) {
            kiihtyvyys -= maxKiihtyvyys / 15;
            if(kiihtyvyys < 0) kiihtyvyys = 0;
        }
        shipRB.AddForce(transform.forward * kiihtyvyys, ForceMode.Force);
        // -------- KIIHTYVYYS -------------
        // -------- KÄÄNTYVYYS -------------
        if(Input.GetKey("a") && kääntyvyys > -maxKääntyvyys) {
            kääntyvyys -= maxKääntyvyys / 15;
        }
        if(Input.GetKey("d") && kääntyvyys < maxKääntyvyys) {
            kääntyvyys += maxKääntyvyys / 15;
        }
        Vector3 laivanEulerKääntyvyys = new Vector3(0, kääntyvyys, 0);
        Quaternion deltaKääntyvyys = Quaternion.Euler(laivanEulerKääntyvyys * Time.fixedDeltaTime);
        shipRB.MoveRotation(shipRB.rotation * deltaKääntyvyys);

        // -------- KÄÄNTYVYYS -------------

        // -------- Kelluvuus --------------
        if(isInWater) 
            shipRB.AddForce(transform.up * 4 * (((0 - transform.position.y))*((0 - transform.position.y))), ForceMode.Force);
        if(transform.position.y > 1)
            shipRB.AddForce(transform.up * -1, ForceMode.VelocityChange);
        // -------- Kelluvuus ---------------
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
