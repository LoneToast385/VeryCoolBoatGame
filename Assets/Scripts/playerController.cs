using System;
using UnityEngine;
using System.Collections;
using Unity.Mathematics;

public class playerController : MonoBehaviour
{
    [HideInInspector]
    public float kääntyvyys = 0;
    [HideInInspector]
    public float kiihtyvyys = 0;
    public float maxKiihtyvyys = 50;
    public float maxKääntyvyys = 1;
    [HideInInspector]
    public float amountHit = 0;
    [HideInInspector]
    public float health = 100;
    private float waterLevelY = 0;
    public Rigidbody shipRB;
    public GameObject Canonball;
    public GameObject canonballShootSmoke;
    public GameObject cannonballHitParticle;
    private bool isShootingLeft = false;
    private bool isShootingRight = false;
    private KeyCode[] movementKeys;
    void Start() {
        switch(this.transform.parent.gameObject.GetComponent<playerHandler>().whichPlayer) {
        case 0:
          movementKeys = null;
          break;
        case 1:
            movementKeys = new KeyCode[] {
                KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.E, KeyCode.Q
            };
            print("Keys assigned for player 1!");
            break;
        case 2:
            movementKeys = new KeyCode[] {
                KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.PageDown, KeyCode.PageUp
            };
            print("Keys assigned for player 2!");
            break;
            default:
                Debug.LogException(new Exception("Player Controller cannot access whichPlayer or gets bad value!"));
                break;
        }
    }
    void Update() {
        // -------- KIIHTYVYYS -------------
        if(Input.GetKey(movementKeys[0]) && kiihtyvyys < maxKiihtyvyys) {
            kiihtyvyys += maxKiihtyvyys / 30;
            print(kiihtyvyys);
        }
        if(Input.GetKey(movementKeys[2]) && kiihtyvyys > 0) {
            kiihtyvyys -= maxKiihtyvyys / 30;
            if(kiihtyvyys < 0) kiihtyvyys = 0;
        }
        // -------- KIIHTYVYYS -------------
        // -------- KÄÄNTYVYYS -------------
        if(Input.GetKey(movementKeys[1]) && kääntyvyys > -maxKääntyvyys) {
            kääntyvyys -= maxKääntyvyys / 60;
        }
        if(Input.GetKey(movementKeys[3]) && kääntyvyys < maxKääntyvyys) {
            kääntyvyys += maxKääntyvyys / 60;
        }
        // -------- KÄÄNTYVYYS -------------
        // -------- AMPUMINEN -------------
        if(Input.GetKeyDown(movementKeys[4]) && !isShootingRight) {
                StartCoroutine(shoot(1));
        }       // Int arvot: 1 = vasen, 2 = oikea
        if(Input.GetKeyDown(movementKeys[5]) && !isShootingLeft) {
                StartCoroutine(shoot(2));
        }
        // -------- AMPUMINEN --------------
    }
    IEnumerator shoot(int suunta) {    // Int arvot: 1 = oikea, 2 = vasen
        if(suunta == 1) {
            isShootingRight = true;
            GameObject[] kanuunat = {this.transform.GetChild(0).gameObject, this.transform.GetChild(1).gameObject, this.transform.GetChild(2).gameObject};
            for(int i = 0; i < kanuunat.Length; i++) {
                kanuunaPalloSpawnaus(i, kanuunat);
                yield return new WaitForSeconds(0.5f);
            }   
            yield return new WaitForSeconds(5);
            isShootingRight = false;
        }
        if(suunta == 2) {
            isShootingLeft = true;
            GameObject[] kanuunat = {this.transform.GetChild(3).gameObject, this.transform.GetChild(4).gameObject, this.transform.GetChild(5).gameObject};
            for(int i = 0; i < kanuunat.Length; i++) {
                kanuunaPalloSpawnaus(i, kanuunat);
                yield return new WaitForSeconds(0.5f);
            }   
            yield return new WaitForSeconds(5);
            isShootingLeft = false;

        }
    }
    //Void to handle all cannon ball spawning logic.
    void kanuunaPalloSpawnaus(int i, GameObject[] kanuunat) 
    {
        GameObject kanuunanpallo = Instantiate(Canonball, kanuunat[i].transform.position, kanuunat[i].transform.rotation);
        Instantiate(canonballShootSmoke, kanuunat[i].transform.position, kanuunat[i].transform.rotation);
        kanuunanpallo.GetComponent<Rigidbody>().linearVelocity = this.GetComponent<Rigidbody>().linearVelocity;
        kanuunanpallo.GetComponent<Rigidbody>().angularVelocity = this.GetComponent<Rigidbody>().angularVelocity;
        Vector3 ampumaVoima = new Vector3(250f, 100f, 0f);
        kanuunanpallo.GetComponent<Rigidbody>().AddRelativeForce(ampumaVoima);
    }

    private bool inWater;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Water")
        {
            inWater = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Water")
        {
            inWater = false;
        }
    }
    public Transform[] buoyancyPoints;
    public float buoyancyStrength = 5f;
    [HideInInspector]
    public bool firstDeath = true;
    public GameObject GameManager;
    void FixedUpdate()
    {
        Vector3 laivanEulerKääntyvyys = new Vector3(0, kääntyvyys, 0);
        Quaternion deltaKääntyvyys = Quaternion.Euler(laivanEulerKääntyvyys * Time.fixedDeltaTime);
        shipRB.MoveRotation(shipRB.rotation * deltaKääntyvyys);

        shipRB.AddForce(transform.forward * kiihtyvyys * -2, ForceMode.Force);
        // -------- Kelluvuus --------------
        foreach (Transform point in buoyancyPoints)
        {
            Vector3 worldPoint = point.position;

            if (worldPoint.y < waterLevelY && inWater)
            {
                float depth = waterLevelY - worldPoint.y;
                Vector3 force = Vector3.up * depth * buoyancyStrength;
                shipRB.AddForceAtPosition(force, worldPoint);
            }
        }
        // -------- Kelluvuus ---------------

        // -------- Healthia ----------------
        health -= amountHit / 250;
        if (health <= 0)
        {
            if (firstDeath)
            {
                firstDeath = false;
                GameManager.GetComponent<GameManager>().endGame(this.transform.parent.gameObject.GetComponent<playerHandler>().whichPlayer);
            }
            if (buoyancyStrength > -1)
                buoyancyStrength -= 0.2f;
        }
        // -------- Healthia ----------------
        if (this.transform.position.y < -10)
        {
            GameManager.GetComponent<GameManager>().endGame(this.transform.parent.gameObject.GetComponent<playerHandler>().whichPlayer);
        }
    }
}
