using UnityEngine;
using System.Collections;
using Player;

public class BubbleController : MonoBehaviour {

    public Material shieldMaterial;
    public Material speedMaterial;
    public Material damageMaterial;

    public float speedBoost;

    private Energy playerEnergy;
    private PlayerController playControl;
    private Renderer bubbleRenderer;
    private AudioSource audioSrc;
    private float currentSpeed;
    public bool key1Toggled;
    public bool key2Toggled;
    public bool key3Toggled;

    void Start() {
        //Get our player's energy script
        playerEnergy = GetComponentInParent<Energy>();
        playControl = GetComponentInParent<PlayerController>();
        currentSpeed = playControl.speed;
        bubbleRenderer = GetComponent<Renderer>();

        audioSrc = GetComponent<AudioSource>();
        //Start bubble invisible
        ShrinkBubble();
        key1Toggled = key2Toggled = key3Toggled = false;
    }

    void Update() {
        if (playerEnergy.GetEnergy() == 0f)
        {
            ShrinkBubble();
        }
        else
        {
            CheckKeys();
            if (key1Toggled)
            {

            }
            else
            {

            }
            if (key2Toggled)
            {
                playControl.speed = speedBoost;
            }
            else
            {
                playControl.speed = currentSpeed;
            }
            if (key3Toggled)
            {

            }
        }
    }

    private void ExpandBubble(Material bubbleMaterial) {
        transform.localScale = new Vector3(9f, 9f, 9f);
        bubbleRenderer.material = bubbleMaterial;
        audioSrc.Play();
    }

    private void ShrinkBubble() {
        transform.localScale = new Vector3(0f, 0f, 0f);
        audioSrc.Stop();
    }

    private void CheckKeys() {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            if (key1Toggled)
            {
                ShrinkBubble();
                key1Toggled = false;
            }
            else
            {
                ExpandBubble(shieldMaterial);
                key1Toggled = true;
                key2Toggled = key3Toggled = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            if (key2Toggled)
            {
                ShrinkBubble();
                key2Toggled = false;
            }
            else
            {
                ExpandBubble(speedMaterial);
                key2Toggled = true;
                key1Toggled = key3Toggled = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            if (key3Toggled)
            {
                ShrinkBubble();
                key3Toggled = false;
            }
            else
            {
                ExpandBubble(damageMaterial);
                key3Toggled = true;
                key2Toggled = key1Toggled = false;
            }
        }
    }
}
