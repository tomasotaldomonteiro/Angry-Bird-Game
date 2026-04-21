using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private float Speed = 5.0f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float JumpForce = 5.0f;
    [SerializeField] private Transform StartPosition;
    private Vector3 ForceDirection;
    [SerializeField] private float ForceMultiplier = 100f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float jump = Input.GetAxis("Jump") * JumpForce;
        
        Vector3 movement = new Vector3(horizontal, 0, vertical);
        rb.AddForce(movement * Speed);

        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * JumpForce);
        }
        
        
        
    }


    private void OnMouseDown()
    {
        Debug.Log("I pressed the player");
        
    }

    private void OnMouseUp()
    {
        Vector3 endPosition = ConvertMouseInput();
        ForceDirection = StartPosition.position - endPosition;
        float forceMagnitude = ForceDirection.magnitude;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(ForceDirection * forceMagnitude * ForceMultiplier);
        rb.gravityScale = 1;

        StartCoroutine(ResetPlayer());
        
        
    }

    private void OnMouseDrag()
    {
        
        transform.position = ConvertMouseInput();
        
        
    }

    IEnumerator ResetPlayer()
    {
        yield return new WaitForSeconds(5f);
        this.transform.position = StartPosition.position;
        rb.gravityScale = 0.0f;
        rb.bodyType = RigidbodyType2D.Static;
    }
    
    
    
    private Vector3 ConvertMouseInput()
    {
        Vector3 mousePosition = Input.mousePosition;
        
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mouseWorldPosition.z = 0;

        return mouseWorldPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Building"))
        {
            Destroy(collision.gameObject);
        }
    }
   
    
        
}
