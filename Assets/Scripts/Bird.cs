using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Bird : MonoBehaviour
{
    [SerializeField] float flySpeed = 500f;
    [SerializeField] float maxDragDistance = 5f;

    [SerializeField] Rigidbody2D myRigidbody;
    [SerializeField] SpriteRenderer spriteRenderer;

    Vector2 startPos;

    void Start()
    {
        myRigidbody.isKinematic = true;
        startPos = myRigidbody.position;
    }

    void OnMouseDown() 
    {
        spriteRenderer.color = Color.red;
    }

    void OnMouseUp() 
    {
        Vector2 currentPos = myRigidbody.position;
        Vector2 direction = startPos - currentPos;
        direction.Normalize();

        myRigidbody.isKinematic = false;
        myRigidbody.AddForce(direction * flySpeed);

        spriteRenderer.color = Color.white;
    }

    void OnMouseDrag() 
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0f,0f,10f);
        Vector2 desiredPos = mousePos;

        float distance = Vector2.Distance(desiredPos, startPos);
        if(distance >= maxDragDistance)
        {
            Vector2 direction = desiredPos - startPos;
            direction.Normalize();
            desiredPos = startPos + (direction * maxDragDistance);
        }

        if(desiredPos.x > startPos.x)
        {
            desiredPos.x = startPos.x;
        }
        
        myRigidbody.position = desiredPos;
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        StartCoroutine(ResetAfterDelay());
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);
        myRigidbody.isKinematic = true;
        myRigidbody.position = startPos;
        myRigidbody.velocity = Vector2.zero;
    }

}
