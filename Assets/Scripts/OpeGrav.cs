using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeGrav : MonoBehaviour
{
    // public GameObject obj;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onGrav()
    {
        rb.isKinematic = false;
    }

    public void addTag()
    {
        this.tag = "Touched";
    }
}
