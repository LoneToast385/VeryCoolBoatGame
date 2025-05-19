using UnityEngine;
using UnityEngine.UI;

public class playerHUDHandler : MonoBehaviour
{
    public GameObject handledBoat;
    private GameObject healthBar;
    private GameObject helmWheel; 
    private GameObject accelBar;
    void Start()
    {
        /*
        Healthbar and helmwheel gameobjects NEED to be in this order for script to work
        If something's borked, check unity child hirerarchy, it needs to match the refrence order here.
        */
        healthBar = this.transform.GetChild(0).gameObject;
        helmWheel = this.transform.GetChild(1).gameObject;
        accelBar = this.transform.GetChild(2).gameObject;
    }
    void Update()
    {
        healthBar.GetComponent<Slider>().value = handledBoat.GetComponent<playerController>().health;
        helmWheel.transform.rotation = Quaternion.Euler(0, 0, 520 * handledBoat.GetComponent<playerController>().kääntyvyys);
        accelBar.GetComponent<Slider>().value = handledBoat.GetComponent<playerController>().kiihtyvyys;
    }
}
