using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
public class canonBallHandler : MonoBehaviour
{
    public GameObject cannonballHitParticle;

    void Start() { //MOST START LOGIC IN playerController WHERE CANNONBALL IS SPAWN!!
        StartCoroutine(activate());
    }
    IEnumerator activate() {
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<SphereCollider>().enabled = true;
    }
    void OnCollisionEnter(Collision collision) {
        if(collision.collider.tag == "Ship") {
            damage(collision.collider.gameObject, collision.contacts[0]);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Water") {
            sink();
        }
    }
    void damage(GameObject ship, ContactPoint contact) {
        ship.GetComponent<playerController>().amountHit += 1;
        ship.GetComponent<playerController>().health -= 6;
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        GameObject particleInstance = Instantiate(cannonballHitParticle, pos, rot);
        int randInt = Random.Range(1, 3);
        particleInstance.transform.GetChild(randInt).gameObject.SetActive(true);
        sink();
    }
    void sink() {
        Destroy(this.gameObject);
    }
}
