using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ControladorUI : MonoBehaviour
{
    public TextMeshProUGUI textoTiempo;
    public TextMeshProUGUI textoVida;
    private VidaJugador vidaJugador;

    void Start()
    {
        GameObject jugadorObj = GameObject.FindWithTag("Player");
        if (jugadorObj != null)
        {
            vidaJugador = jugadorObj.GetComponent<VidaJugador>();
        }
        else
        {
            Debug.LogError("No se encontró el jugador con tag 'Player'");
        }
    }

    void Update()
    {
        if (GameManager.instancia != null && textoTiempo != null)
        {
            textoTiempo.text = "Tiempo: " + (int)GameManager.instancia.tiempo;
        }
        if (vidaJugador != null && textoVida != null)
        {
            textoVida.text = "Vida: " + vidaJugador.vida;
        }
    }
}