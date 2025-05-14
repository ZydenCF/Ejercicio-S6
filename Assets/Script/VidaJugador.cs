using UnityEngine;
using System;

public class VidaJugador : MonoBehaviour
{
    public int vida;
    public int vidaMaxima = 3;

    public static event Action<int, int> OnVidaCambiada; 
    public static event Action OnJugadorMuerto;

    void Start()
    {
        vida = vidaMaxima;
        OnVidaCambiada?.Invoke(vida, vidaMaxima);
    }

    public void RecibirDaño(int cantidad)
    {
        vida -= cantidad;
        OnVidaCambiada?.Invoke(vida, vidaMaxima);

        if (vida <= 0)
        {
           
            OnJugadorMuerto?.Invoke();

            if (GameManager.instancia != null)
            {
                GameManager.instancia.TerminarJuego();
            }
            Destroy(gameObject);
        }
    }

    public void RecuperarVida(int cantidad)
    {
        vida = Mathf.Min(vida + cantidad, vidaMaxima);
        
        OnVidaCambiada?.Invoke(vida, vidaMaxima);
    }
}
