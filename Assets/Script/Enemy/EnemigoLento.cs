using UnityEngine;

public class EnemigoLento : EnemigoBase
{
    protected override void Iniciar()
    {
        velocidad = 10;

        comportamientoActual = delegate () {
            if (jugador != null)
            {
                transform.LookAt(jugador);
            }
        };
    }

    protected override void ActualizarMovimiento()
    {
        if (comportamientoActual != null)
        {
            comportamientoActual();
        }

        if (jugador != null)
        {
            Vector3 direccion = jugador.position - transform.position;
            direccion.y = 0;
            if (direccion != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direccion);
                transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
            }
        }
    }

    void OnCollisionEnter(Collision colision)
    {
        if (colision.gameObject.CompareTag("Player"))
        {
            VidaJugador vidaJugador = colision.gameObject.GetComponent<VidaJugador>();
            if (vidaJugador != null)
            {
                vidaJugador.RecibirDaño(1);
                Destroy(gameObject);
            }
        }
    }
}
