using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitchController : MonoBehaviour
{
    private GameObject player;
    private GameObject evilPlayer;
    private GameObject mainCamera;

    public GameObject NormalMap;
    public GameObject EvilMap;
    public float maxDistanceFromPlayer;

    [HideInInspector]
    public int index = 0; // 0 --> normal mod, 1 --> evil mod
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }
    private void Update()
    {
        if (index == 0)
        {
            NormalMap.SetActive(true);
            EvilMap.SetActive(false);
        }
        else if (index == 1)
        {
            NormalMap.SetActive(false);
            EvilMap.SetActive(true);
        }

        if (evilPlayer)
        {
            EvilPlayerMovementRestriction();
            Debug.Log(Vector3.Distance(evilPlayer.transform.position, player.transform.position));
        }

    }
    public void SwitchCharacter()
    {
        if (index == 0)
        {
            //player.GetComponent<SpriteRenderer>().color = Color.red;
            evilPlayer = Instantiate(player);
            evilPlayer.GetComponent<SpriteRenderer>().color = Color.red;
            mainCamera.GetComponent<CameraFollow>().target = evilPlayer.transform;
            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<BoxCollider2D>().isTrigger = true;
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;


            index = 1;
        }
        else if (index == 1)
        {
            //player.GetComponent<SpriteRenderer>().color = Color.black;
            Destroy(evilPlayer);
            mainCamera.GetComponent<CameraFollow>().target = player.transform;
            player.GetComponent<PlayerController>().enabled = true;
            player.GetComponent<BoxCollider2D>().isTrigger = false;
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            index = 0;
        }
       
    }
    void EvilPlayerMovementRestriction()
    {
        float rightRestriction = player.transform.position.x + maxDistanceFromPlayer;
        float leftRestriction = player.transform.position.x - maxDistanceFromPlayer;
        float upRestriction = player.transform.position.y + maxDistanceFromPlayer;
        float downRestriction = player.transform.position.y - maxDistanceFromPlayer;

        if (evilPlayer.transform.position.x > rightRestriction)
        {
            evilPlayer.transform.position = new Vector3(rightRestriction, evilPlayer.transform.position.y, evilPlayer.transform.position.z);
        }
        else if (evilPlayer.transform.position.x < leftRestriction)
        {
            evilPlayer.transform.position = new Vector3(leftRestriction, evilPlayer.transform.position.y, evilPlayer.transform.position.z);
        }
        if (evilPlayer.transform.position.y > upRestriction)
        {
            evilPlayer.transform.position = new Vector3(evilPlayer.transform.position.x, upRestriction, evilPlayer.transform.position.z);
        }
        else if (evilPlayer.transform.position.y < downRestriction)
        {
            evilPlayer.transform.position = new Vector3(evilPlayer.transform.position.x, downRestriction, evilPlayer.transform.position.z);
        }
    }
}
