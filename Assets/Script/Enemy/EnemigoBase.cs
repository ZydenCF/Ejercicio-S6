using UnityEngine;
using System;

public abstract class EnemigoBase : MonoBehaviour, IObserver
{
    public float velocidad;
    protected Transform jugador;

    protected delegate void ComportamientoEnemigo();
    protected ComportamientoEnemigo comportamientoActual;

    void Start()
    {
        GameObject jugadorObj = GameObject.FindWithTag("Player");
        if (jugadorObj != null)
        {
            jugador = jugadorObj.transform;
        }
        if (GameManager.instancia != null)
        {
            GameManager.instancia.Adjuntar(this);
        }

        GameManager.OnNivelCambiado += (nivel) => {
            velocidad += nivel * 0.2f;
        };

        Iniciar();
    }

    void Update()
    {
        Vector3 posicion = transform.position;
        posicion.y = 0.5f;
        transform.position = posicion;
        ActualizarMovimiento();
    }

    public void Ejecutar(ISubject sujeto)
    {
        if (sujeto is GameManager)
        {
            velocidad += 0.5f;
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
            }
        }
    }

    void OnDestroy()
    {
        if (GameManager.instancia != null)
        {
            GameManager.instancia.Quitar(this);
            GameManager.instancia.EnemigoDerrotado();
        }

        GameManager.OnNivelCambiado -= (nivel) => {
            velocidad += nivel * 0.2f;
        };
    }

    protected abstract void Iniciar();
    protected abstract void ActualizarMovimiento();
}