using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour, ISubject
{
    public static GameManager instancia;
    public float tiempo;
    private List<IObserver> observadores = new List<IObserver>();
    private bool juegoTerminado = false;

    public int nivel = 1;
    public int enemigosEliminados = 0;

    public static Action<int> OnNivelCambiado;
    public static Action<int> OnEnemigoEliminado;

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
        tiempo = 0;
        juegoTerminado = false;
        nivel = 1;
        enemigosEliminados = 0;
        Time.timeScale = 1;
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
        PlayerPrefs.SetInt("TiempoSobrevivido", (int)tiempo);
        PlayerPrefs.SetInt("EnemigosEliminados", enemigosEliminados);
        PlayerPrefs.SetInt("NivelAlcanzado", nivel);
        PlayerPrefs.Save();

        SceneManager.LoadScene("PantallaDerrota");
    }

    public void EnemigoDerrotado()
    {
        enemigosEliminados++;
        OnEnemigoEliminado?.Invoke(enemigosEliminados);

        if (enemigosEliminados % 10 == 0)
        {
            nivel++;
            OnNivelCambiado?.Invoke(nivel);
        }
    }
}