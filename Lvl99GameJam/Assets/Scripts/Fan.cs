using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public float forcePower;
    public LayerMask layerMask;

    private Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, Mathf.Infinity, layerMask); 
        
        if (hit.collider)
        {
            rb = hit.collider.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.right * forcePower, ForceMode2D.Force);
            Debug.Log("Player in LOS of fan");
        }

        Debug.DrawRay(transform.position, transform.right * Mathf.Infinity, Color.white);
    }
}
