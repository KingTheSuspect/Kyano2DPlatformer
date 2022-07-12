using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitchController : MonoBehaviour
{
    private GameObject player;

    public GameObject NormalMap;
    public GameObject EvilMap;

    [HideInInspector]
    public int index = 0; // 0 --> normal mod, 1 --> evil mod
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
            player.GetComponent<SpriteRenderer>().color = Color.red;
            
            index = 1;
        }
        else if (index == 1)
        {
            player.GetComponent<SpriteRenderer>().color = Color.black;

            index = 0;
        }
       
    }
}
