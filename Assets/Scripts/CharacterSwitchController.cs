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
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            index = 1;
        }
        else if (index == 1)
        {
            //player.GetComponent<SpriteRenderer>().color = Color.black;
            Destroy(evilPlayer);
            mainCamera.GetComponent<CameraFollow>().target = player.transform;
            player.GetComponent<PlayerController>().enabled = true;
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            index = 0;
        }
       
    }
}
