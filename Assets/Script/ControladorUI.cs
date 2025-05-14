using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ControladorUI : MonoBehaviour
{
    public TextMeshProUGUI textoTiempo;
    public TextMeshProUGUI textoVida;
    public TextMeshProUGUI textoEnemigos;
    public TextMeshProUGUI textoNivel;

    void Start()
    {
        // Suscribirse a los eventos
        VidaJugador.OnVidaCambiada += ActualizarTextoVida;
        GameManager.OnEnemigoEliminado += ActualizarTextoEnemigos;
        GameManager.OnNivelCambiado += ActualizarTextoNivel;
    }

    void Update()
    {
        if (GameManager.instancia != null && textoTiempo != null)
        {
            textoTiempo.text = "Tiempo: " + (int)GameManager.instancia.tiempo;
        }
    }

    private void ActualizarTextoVida(int vidaActual, int vidaMaxima)
    {
        if (textoVida != null)
        {
            textoVida.text = "Vida: " + vidaActual + "/" + vidaMaxima;
        }
    }

    private void ActualizarTextoEnemigos(int cantidad)
    {
        if (textoEnemigos != null)
        {
            textoEnemigos.text = "Enemigos: " + cantidad;
        }
    }

    private void ActualizarTextoNivel(int nivel)
    {
        if (textoNivel != null)
        {
            textoNivel.text = "Nivel: " + nivel;
        }
    }

    void OnDestroy()
    {
        
        VidaJugador.OnVidaCambiada -= ActualizarTextoVida;
        GameManager.OnEnemigoEliminado -= ActualizarTextoEnemigos;
        GameManager.OnNivelCambiado -= ActualizarTextoNivel;
    }
}