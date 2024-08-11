using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float rotationSpeed = 200.0f;
    public GameObject splitPrefab;
    private float bouncingSpeed = 500f;
    private Rigidbody rb;
    private float MAX_FALL_SPEED = -9f;
    private bool isOnCombo = false;
    private bool endInvinciblePhase = false;
    private bool invicible = false;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private SoundEffectController sfxController;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public bool GetIsOnCombo()
    {
        return isOnCombo;
    }

    public bool GetInvicible()
    {
        return invicible;
    }


    public void SetInvicible(bool state)
    {
        invicible = state;
    }

    private void Update() {
        if (isOnCombo) {
            Debug.Log("BoOM~");
        }
    }

    private void LateUpdate() {
        if (rb.velocity.y <= MAX_FALL_SPEED) {
            rb.velocity = new Vector3(0, MAX_FALL_SPEED, 0);
        }
    }

    private void OnCollisionEnter(Collision other) {
        rb.velocity = new Vector3(0, bouncingSpeed, 0) * Time.deltaTime;
        GameObject newSplit = Instantiate(splitPrefab, new Vector3(transform.position.x, other.transform.position.y + 0.19f, transform.position.z), transform.rotation);
        newSplit.transform.localScale = Vector3.one * Random.Range(0.3f, 0.5f);
        newSplit.transform.parent = other.transform;
        string tag = other.transform.tag;
        if(tag == "SafePart") {
            sfxController.PlayBounce();
            isOnCombo = false;
            endInvinciblePhase = true;
        }
        if(tag == "UnsafePart") {
            sfxController.PlayLose();
            GameManager.gameOver = true;
            Time.timeScale = 0;
        }
        if(tag == "LastArea" || tag == "LastPart") {
            sfxController.PlayWin();
            GameManager.levelWin = true;
        }
    }
    
}
