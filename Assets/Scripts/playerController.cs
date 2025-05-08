using UnityEngine;
using System.Collections;

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
    public GameObject Canonball;
    private bool isShootingLeft = false;
    private bool isShootingRight = false;

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
        if(Input.GetKeyDown("e") && !isShootingRight) {
                StartCoroutine(shoot(1));
        }       // Int arvot: 1 = vasen, 2 = oikea
        if(Input.GetKeyDown("q") && !isShootingLeft) {
                StartCoroutine(shoot(2));
        }
        // -------- AMPUMINEN --------------
    }
    IEnumerator shoot(int suunta) {    // Int arvot: 1 = oikea, 2 = vasen
        if(suunta == 1) {
            isShootingRight = true;
            GameObject[] kanuunat = {this.transform.GetChild(0).gameObject, this.transform.GetChild(1).gameObject, this.transform.GetChild(2).gameObject};
            for(int i = 0; i < kanuunat.Length; i++) {
                GameObject kanuunanpallo = Instantiate(Canonball, kanuunat[i].transform.position, kanuunat[i].transform.rotation);
                Vector3 ampumaVoima = new Vector3(1500f, 500f, 0f);
                kanuunanpallo.GetComponent<Rigidbody>().AddRelativeForce(ampumaVoima);
                yield return new WaitForSeconds(0.5f);
            }   
            yield return new WaitForSeconds(5);
            isShootingRight = false;
        }
        if(suunta == 2) {
            isShootingLeft = true;
            GameObject[] kanuunat = {this.transform.GetChild(3).gameObject, this.transform.GetChild(4).gameObject, this.transform.GetChild(5).gameObject};
            for(int i = 0; i < kanuunat.Length; i++) {
                GameObject kanuunanpallo = Instantiate(Canonball, kanuunat[i].transform.position, kanuunat[i].transform.rotation);
                Vector3 ampumaVoima = new Vector3(1500f, 500f, 0f);
                kanuunanpallo.GetComponent<Rigidbody>().AddRelativeForce(ampumaVoima);
                yield return new WaitForSeconds(0.5f);
            }   
            yield return new WaitForSeconds(5);
            isShootingLeft = false;

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
            Vector3 xKaantonorm = new Vector3(0.002f * transform.rotation[0],0,0);
            Quaternion xKaantonormQuat = Quaternion.Euler(xKaantonorm);
            shipRB.MoveRotation(xKaantonormQuat);
            Vector3 zKaantonorm = new Vector3(0,0,0.002f * transform.rotation[2]);
            Quaternion zKaantonormQuat = Quaternion.Euler(zKaantonorm);
            shipRB.MoveRotation(zKaantonormQuat);
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
