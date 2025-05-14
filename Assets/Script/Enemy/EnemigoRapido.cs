using UnityEngine;
using System;

public class EnemigoRapido : EnemigoBase
{
    protected override void Iniciar()
    {
        velocidad = 20;
    }

    protected override void ActualizarMovimiento()
    {
        Action moverHaciaJugador = () => 
        {
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
        };

        moverHaciaJugador();
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