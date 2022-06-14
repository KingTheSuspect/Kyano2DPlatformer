using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    private GameObject ayarlar;
    public GameObject CanvasSahne;
    public Toggle MusicToggle;
    private bool durum;

    
    void Start()
    {
        ayarlar = GameObject.Find("Ayarlar");
        ayarlar.GetComponent<Button>().onClick.AddListener(Loader);
    }
    void Loader()
    {
        CanvasSahne.SetActive(true);
        
        
    }
    public void ButtonClick()
    {
        if (MusicToggle.isOn)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
    }
}
