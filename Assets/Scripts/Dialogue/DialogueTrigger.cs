using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject buttonVisual;
    private bool isPlayerInRange;
    public TextAsset inkJSON;

    void Awake()
    {
        buttonVisual.SetActive(false);
        isPlayerInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        // KEYCODE.E Yanlýþ bir kullaným burayý düzelteceðim.

        if (isPlayerInRange && !DialogueManager.GetInstance().isDialoguePlaying)
        {
            buttonVisual.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
        }
        else
        {
            buttonVisual.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
