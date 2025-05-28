using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class destroyThisAfterX : MonoBehaviour
{
    public float X = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(destroyAfter());
    }
    IEnumerator destroyAfter()
    {
        yield return new WaitForSeconds(X);
        Destroy(this.gameObject);
    }

}
