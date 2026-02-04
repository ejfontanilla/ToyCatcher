using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Transform topFloor;
    public Transform bottomFloor;

    public float minX = -8f;
    public float maxX = 8f;

    private Rigidbody2D rb;
    private bool onTopFloor = false;
    private Vector3 originalScale;

    public Sprite normalSprite;
    public Sprite stunnedSprite;
    public float stunDuration = 1f;
    public AudioSource hitSound;

    private SpriteRenderer sr;
    private bool isStunned = false;
    public bool IsStunned => isStunned;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;

        
        sr = GetComponent<SpriteRenderer>();

        if (normalSprite != null)
        {
            sr.sprite = normalSprite;
        }



        Debug.Log("topfloory start: " + topFloor.position.y);
        Debug.Log("bottomfloory start: " + bottomFloor.position.y);

        // Start on bottom floor
        //Vector3 pos = transform.position;
        //pos.y = bottomFloor.position.y;
        //transform.position = pos;
        //Debug.Log("start transform position:" + pos);

        // Start on bottom floor
        rb.position = new Vector2(
            rb.position.x,
            bottomFloor.position.y
        );
        Debug.Log("start transform position:" + rb.position);
    }

    void Update()
    {
        // Horizontal movement
        float moveX = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveX * moveSpeed, 0f);

        // Flip sprite
        if (moveX > 0)
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        else if (moveX < 0)
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);

        // Switch floors
        if (Input.GetKeyDown(KeyCode.Space))
        {
            onTopFloor = !onTopFloor;

            //Vector3 pos = transform.position;
            //pos.y = onTopFloor ? topFloor.position.y : bottomFloor.position.y;
            //transform.position = pos;
            //Debug.Log("Change floor  is it on top?" + onTopFloor + " transform position: " + pos);

            rb.position = new Vector2(
                rb.position.x,
                onTopFloor ? topFloor.position.y : bottomFloor.position.y
            );
            Debug.Log("Change floor  is it on top?" + onTopFloor + " transform position: " + rb.position);
        }

        // Clamp X only
        //Vector3 clampedPos = transform.position;
        //clampedPos.x = Mathf.Clamp(clampedPos.x, minX, maxX);
        //transform.position = clampedPos;

        float clampedX = Mathf.Clamp(rb.position.x, minX, maxX);
        rb.position = new Vector2(clampedX, rb.position.y);

        if (isStunned)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

    }

    public void Stun()
    {
        if (!isStunned)
            StartCoroutine(StunRoutine());
    }

    IEnumerator StunRoutine()
    {
        isStunned = true;

        sr.sprite = stunnedSprite;
        if (hitSound != null)
            hitSound.Play();

        yield return new WaitForSeconds(stunDuration);

        sr.sprite = normalSprite;
        isStunned = false;
    }

}






