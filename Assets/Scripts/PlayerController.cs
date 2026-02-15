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

    public float floorSlideDuration = 0.25f;
    private bool isSliding = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;

        
        sr = GetComponent<SpriteRenderer>();

        if (normalSprite != null)
        {
            sr.sprite = normalSprite;
        }

        rb.position = new Vector2(
            rb.position.x,
            bottomFloor.position.y
        );
    }

    void Update()
    {
        if (isSliding) return;

        float moveX = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveX * moveSpeed, 0f);

        if (moveX > 0)
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        else if (moveX < 0)
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            onTopFloor = !onTopFloor;

            StartCoroutine(SlideToFloor(onTopFloor ? topFloor.position.y : bottomFloor.position.y));
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlaySFX(
                    SoundManager.Instance.laneSwitchSound
                );
            }
            if (isSliding) return;
        }

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

    System.Collections.IEnumerator SlideToFloor(float targetY)
    {
        if (isSliding) yield break;

        isSliding = true;

        Vector2 startPos = transform.position;
        Vector2 endPos = new Vector2(startPos.x, targetY);

        float elapsed = 0f;

        while (elapsed < floorSlideDuration)
        {
            float t = elapsed / floorSlideDuration;
            t = Mathf.SmoothStep(0f, 1f, t);
            transform.position = Vector2.Lerp(startPos, endPos, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
        isSliding = false;
    }

    public void Freeze()
    {
        enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = Vector2.zero;
    }

}






