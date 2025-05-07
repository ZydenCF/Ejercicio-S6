using UnityEngine;

public class VidaJugador : MonoBehaviour
{
    public int vida;

    void Start()
    {
        vida = 3;
    }

    public void RecibirDaño(int cantidad)
    {
        vida -= cantidad;

        if (vida <= 0)
        {
            if (GameManager.instancia != null)
            {
                GameManager.instancia.TerminarJuego();
            }
            Destroy(gameObject);
        }
    }
}
