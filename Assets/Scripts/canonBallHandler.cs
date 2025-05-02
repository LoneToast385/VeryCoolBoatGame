using UnityEngine;

public class canonBallHandler : MonoBehaviour
{
    
    void Start() {
        Rigidbody cannonballRB = this.gameObject.GetComponent<Rigidbody>();
        cannonballRB.AddForce(0,0,0);
    }
    void OnTriggerEnter(Collider collider) {
        if(collider.tag == "Ship") {
            damage(collider.gameObject);
        }
        if(collider.tag == "Water") {
            sink();
        }
    }
    void damage(GameObject ship) {
        ship.GetComponent<playerController>().amountHit += 1;
    }
    void sink() {
        Destroy(this.gameObject);
    }
}
