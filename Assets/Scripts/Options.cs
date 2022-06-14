using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    private GameObject ayarlar;
    public GameObject exit;
    public GameObject CanvasSahne;
    public Toggle MusicToggle;

    
    void Start()
    {
        ayarlar = GameObject.Find("Ayarlar");
        ayarlar.GetComponent<Button>().onClick.AddListener(Loader);
    }
    void Loader()
    {
        CanvasSahne.SetActive(true);
       
    }
    public void Quit()
    {
        CanvasSahne.SetActive(false);
    }
    public void ButtonClick()
    {
        if (MusicToggle.isOn)
        {
            AudioListener.volume = 1;
            
        }
        if (!MusicToggle.isOn)
        {
            AudioListener.volume = 0;
        }
    }
    private void Update()
    {
        
    }
}
