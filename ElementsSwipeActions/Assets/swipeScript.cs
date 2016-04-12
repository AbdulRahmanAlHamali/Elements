using UnityEngine;
using System.Collections;

public class swipeScript : MonoBehaviour {
    private Touch initialTouch = new Touch();
    private float distance = 0;
    private bool hasSwiped = false;
    private bool isJumping = false;
    private bool isSliding = false;
    private float jumpHeight = 8f;
    private float restingHeight = 0f;
    private float slideTimer = 0f;
    private Rigidbody2D rb;
    private CircleCollider2D c;

    void Awake()
    {
        //Debug.Log("1");
        rb = GetComponent<Rigidbody2D>();
        c = GetComponent<CircleCollider2D>();
        restingHeight = rb.position.y;
    }

    void FixedUpdate() {
        //Debug.Log("2");
        if (isSliding && slideTimer > 0f)
        {
            slideTimer -= Time.deltaTime;
        }
        if (isJumping && rb.position.y <= restingHeight && rb.velocity.y.Equals(0f))
        {
            jumpEnd();
        }
        if (isSliding && slideTimer<= 0f)
        {
            slideEnd();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping && !isSliding)
        {
            jumpStart();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isJumping && !isSliding)
        {
            slideStart();
        }
        foreach (Touch t in Input.touches)
        {
            if (t.phase == TouchPhase.Began)
            {
                initialTouch = t;
            }
            else if (t.phase == TouchPhase.Moved && !hasSwiped)
            {
                float deltaX = initialTouch.position.x - t.position.x;
                float deltaY = initialTouch.position.y - t.position.y;
                distance = Mathf.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                bool swipedSideways = Mathf.Abs(deltaX) > Mathf.Abs(deltaY);

                if (distance > 10f)
                {
                    if (swipedSideways && deltaX > 0) //swipe left
                    {
                        
                    }
                    else if (swipedSideways && deltaX <= 0) //swipe right
                    {

                    }
                    else if (!swipedSideways && deltaY > 0) //swipe down
                    {
                        slideStart();
                    }
                    else if (!swipedSideways && deltaY <= 0) //swipe up
                    {
                        jumpStart();
                    }
                    hasSwiped = true;
                }
            }
            else if(t.phase == TouchPhase.Ended)
            {
                initialTouch = new Touch();
                hasSwiped = false;
            }
        }
    }
    void OnCollision2DStay()
    {
        Debug.Log("OnCollision2DStay");
    }
    void jumpStart()
    {
        isJumping = true;
        //rb.AddForce(Vector2.up * jumpHeight);
        rb.velocity = new Vector2(0f, jumpHeight);
    }
    void jumpEnd()
    {
        isJumping = false;
    }
    void slideStart() 
    {
        isSliding = true;
        float slideHeight = rb.transform.localScale.y / 2;
        rb.transform.localScale -= new Vector3(0f, slideHeight);
        c.radius /= 2;
        slideTimer = 1f;
    }
    void slideEnd()
    {
        float currentlHeight = rb.transform.localScale.y;
        rb.transform.localScale += new Vector3(0f, currentlHeight);
        c.radius *= 2;
        isSliding = false;
    }
}
