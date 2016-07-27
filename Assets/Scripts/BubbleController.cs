using UnityEngine;
using System.Collections;
using Player;

public class BubbleController : MonoBehaviour {

    public Material shieldMaterial;
    public Material speedMaterial;
    public Material damageMaterial;

    private Energy playerEnergy;
    private Renderer bubbleRenderer;
    private AudioSource audioSrc;
    private bool key1Toggled;
    private bool key2Toggled;
    private bool key3Toggled;

    void Start() {
        //Get our player's energy script
        playerEnergy = GetComponentInParent<Energy>();
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
                }
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
}
