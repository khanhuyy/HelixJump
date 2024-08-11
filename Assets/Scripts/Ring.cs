using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    private GameObject player;
    public GameObject[] childRings;
    public Material safeColor;
    public Material unsafeColor;
    private float radius = 100f;
    private float force = 500f;
    private int COMBO_BECOME_INVINCIBLE = 3;
    private int currentTotalCombo = 0;
    private Renderer meshRenderer;
    private AudioSource audioSource;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        bool isInvicible = player.GetComponent<Ball>().GetInvicible();
        if (currentTotalCombo >= COMBO_BECOME_INVINCIBLE) {
            for(int i = 0; i < childRings.Length; i++) {
                meshRenderer = childRings[i].GetComponent<Renderer>();
                meshRenderer.material = safeColor;
            }
            this.enabled = false;
        }
        if (!isInvicible) {
            for(int i = 0; i < childRings.Length; i++) {
                if (childRings[i].gameObject.tag == "SafeArea") {

                    childRings[i].GetComponent<Renderer>().material = safeColor;
                } else if (childRings[i].gameObject.tag == "UnsafeArea") {
                    childRings[i].GetComponent<Renderer>().material = unsafeColor;
                }
            }
        }
        if (transform.position.y > player.transform.position.y + 0.1f) {
            audioSource.Play();
            if (player.GetComponent<Ball>().GetIsOnCombo()) {
                currentTotalCombo += 1; 
            }
            GameManager.totalPassedRings++;
            for(int i = 0; i < childRings.Length; i++) {
                childRings[i].GetComponent<Rigidbody>().isKinematic = false;
                childRings[i].GetComponent<Rigidbody>().useGravity = true;

                Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
                foreach(Collider collider in colliders) {
                    Rigidbody rb = collider.GetComponent<Rigidbody>();
                    if (rb != null) {
                        rb.AddExplosionForce(force, transform.position, radius);                    
                    }
                }
                childRings[i].GetComponent<MeshCollider>().enabled = false;
                childRings[i].transform.parent = null;
                Destroy(childRings[i].gameObject, 2f);
                Destroy(this.gameObject, 5f);
            }
            this.enabled = false;
        }
    }
}
