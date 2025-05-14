using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MenuPrincipal : MonoBehaviour
{
    public Button botonJugar;

    void Start()
    {
        botonJugar.onClick.AddListener(() => 
        {
            SceneManager.LoadScene("SampleScene");
        });

    }
}