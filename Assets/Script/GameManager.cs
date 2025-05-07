using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour, ISubject
{
    public static GameManager instancia;
    public float tiempo;
    public GameObject panelGameOver;
    public TextMeshProUGUI textoFinal;
    private List<IObserver> observadores = new List<IObserver>();
    private bool juegoTerminado = false;

    private GameObject panelGameOverOriginal;
    private TextMeshProUGUI textoFinalOriginal;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        panelGameOverOriginal = panelGameOver;
        textoFinalOriginal = textoFinal;

        tiempo = 0;
        juegoTerminado = false;
        Time.timeScale = 1;

        if (panelGameOver != null)
        {
            panelGameOver.SetActive(false);
        }
    }

    void Update()
    {
        if (!juegoTerminado)
        {
            tiempo += Time.deltaTime;
            if (tiempo % 10 < Time.deltaTime)
            {
                Notificar();
            }
        }
    }

    public void Adjuntar(IObserver observador)
    {
        if (!observadores.Contains(observador))
        {
            observadores.Add(observador);
        }
    }

    public void Quitar(IObserver observador)
    {
        observadores.Remove(observador);
    }

    public void Notificar()
    {
        foreach (IObserver observador in observadores)
        {
            observador.Ejecutar(this);
        }
    }

    public void TerminarJuego()
    {
        juegoTerminado = true;
        Time.timeScale = 0;

        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true);
            if (textoFinal != null)
            {
                textoFinal.text = "Sobreviviste " + (int)tiempo + " segundos";
            }
        }
        else
        {
            Debug.Log("Panel GameOver no asignado");
        }
    }

    private void BuscarReferencias()
    {
        if (panelGameOverOriginal != null)
        {
            string nombrePanel = panelGameOverOriginal.name;
            GameObject nuevoPanel = GameObject.Find(nombrePanel);
            if (nuevoPanel != null)
            {
                panelGameOver = nuevoPanel;
                panelGameOver.SetActive(false);
            }
        }

        if (textoFinalOriginal != null && panelGameOver != null)
        {
            string nombreTexto = textoFinalOriginal.gameObject.name;
            Transform texto = panelGameOver.transform.Find(nombreTexto);
            if (texto != null)
            {
                textoFinal = texto.GetComponent<TextMeshProUGUI>();
            }
        }
    }
}