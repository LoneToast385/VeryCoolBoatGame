using UnityEngine;

public class playerController : MonoBehaviour
{
    private float kääntyvyys = 0;
    private float kiihtyvyys = 0;
    public float maxKiihtyvyys = 10;
    public float maxKääntyvyys = 1;
    public int amountHit = 0;
    private float health = 100;
    private bool isInWater = false;
    public Rigidbody shipRB;

    void Update() {
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
        // -------- KÄÄNTYVYYS -------------
        // -------- AMPUMINEN -------------
        if(Input.GetKey("q")) {
                shoot(1);
        }       // Int arvot: 1 = vasen, 2 = oikea
        if(Input.GetKey("e")) {
                shoot(2);
        }
        // -------- AMPUMINEN --------------
    }
    void shoot(int suunta) {    // Int arvot: 1 = vasen, 2 = oikea
        if(suunta == 1) {
            GameObject[] kanuunat = {this.transform.GetChild(0).gameObject, this.transform.GetChild(1).gameObject, this.transform.GetChild(2).gameObject};
            for(int i = 0; i <= kanuunat.Length; i++) {

            }
        }
        if(suunta == 2) {
            GameObject[] kanuunat = {this.transform.GetChild(3).gameObject, this.transform.GetChild(4).gameObject, this.transform.GetChild(5).gameObject};
            

        }
    }
    void FixedUpdate()
    {
        Vector3 laivanEulerKääntyvyys = new Vector3(0, kääntyvyys, 0);
        Quaternion deltaKääntyvyys = Quaternion.Euler(laivanEulerKääntyvyys * Time.fixedDeltaTime);
        shipRB.MoveRotation(shipRB.rotation * deltaKääntyvyys);

        // -------- Kelluvuus --------------
        if(isInWater) 
            shipRB.AddForce(transform.up * 4 * (((0 - transform.position.y))*((0 - transform.position.y))), ForceMode.Force);
        if(transform.position.y > 1)
            shipRB.AddForce(transform.up * -1, ForceMode.VelocityChange);
        // -------- Kelluvuus ---------------

        // -------- Healthia ----------------
        health -= amountHit / 100;
        if(health <= 0) {
            Destroy(this.gameObject);
        }
        // -------- Healthia ----------------
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
