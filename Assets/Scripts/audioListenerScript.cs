using UnityEngine;

public class audioListenerScript : MonoBehaviour
{
    public GameObject ship1;
    public GameObject ship2;
    void Update()
    {
        this.transform.position = new Vector3((ship1.transform.position.x + ship2.transform.position.x) / 2, (ship1.transform.position.y + ship2.transform.position.y) / 2, (ship1.transform.position.z + ship2.transform.position.z) / 2);
    }
}
