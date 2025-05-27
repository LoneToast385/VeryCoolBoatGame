using UnityEngine;
using System.Collections;
public class canonBallHandler : MonoBehaviour
{
    void Start() { //MOST START LOGIC IN playerController WHERE CANNONBALL IS SPAWN!!
        StartCoroutine(activate());
    }
    IEnumerator activate() {
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<SphereCollider>().enabled = true;
    }
    void OnCollisionEnter(Collision collision) {
        if(collision.collider.tag == "Ship") {
            damage(collision.collider.gameObject);
        }
        if(collision.collider.tag == "Water") {
            sink();
        }
    }
    void damage(GameObject ship) {
        ship.GetComponent<playerController>().amountHit += 1;
        ship.GetComponent<playerController>().health -= 6;
        sink();
    }
    void sink() {
        Destroy(this.gameObject);
    }
}
