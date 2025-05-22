using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Boss : MonoBehaviour
{
    public float vidaMaxima = 3f;
    private float vidaActual;
    private IBossStrategy estrategiaActual;
    private static int cantidadApariciones = 0;

    public Transform objetivo;
    public float velocidad = 10f;
    private Vector3 direccionActual;
    private Rigidbody cuerpo;

    public GameObject proyectil;
    public Transform puntoDisparo;
    private float tiempoDisparo;
    private float intervaloDisparo = 1.5f;

    public int patronActual;

    void Start()
    {
        vidaActual = vidaMaxima;
        cuerpo = GetComponent<Rigidbody>();

        GameObject jugador = GameObject.FindWithTag("Player");
        if (jugador != null)
        {
            objetivo = jugador.transform;
        }

        CambiarEstrategia();
    }

    void Update()
    {
        if (estrategiaActual != null)
        {
            estrategiaActual.Ejecutar(this);
        }
    }

    public void CambiarEstrategia()
    {
        cantidadApariciones++;
        patronActual = (cantidadApariciones - 1) % 4 + 1;

        if (patronActual == 1)
        {
            estrategiaActual = new BossPatron1();
        }
        else if (patronActual == 2)
        {
            estrategiaActual = new BossPatron2();
        }
        else if (patronActual == 3)
        {
            estrategiaActual = new BossPatron3();
        }
        else
        {
            estrategiaActual = new BossPatron4();
        }
    }

    public void RecibirDanio(float cantidad)
    {
        vidaActual -= cantidad;

        if (vidaActual <= 0)
        {
            GameManager.instancia.EnemigoDerrotado();
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision colision)
    {
        if (colision.gameObject.CompareTag("Player"))
        {
            VidaJugador vidaJugador = colision.gameObject.GetComponent<VidaJugador>();
            if (vidaJugador != null)
            {
                vidaJugador.RecibirDaño(2);
                Destroy(gameObject);
            }
        }
        else if (colision.gameObject.layer == LayerMask.NameToLayer("ParedInvisible"))
        {
            if (estrategiaActual is BossPatron2)
            {
                if (colision.contacts.Length > 0)
                {
                    direccionActual = Vector3.Reflect(direccionActual, colision.contacts[0].normal);
                }
            }
            else if (estrategiaActual is BossPatron4)
            {
                Destroy(gameObject);
            }
        }
    }

    public void EstablecerDireccion(Vector3 direccion)
    {
        direccionActual = direccion;
    }

    public Vector3 ObtenerDireccion()
    {
        return direccionActual;
    }

    public void Disparar()
    {
        if (objetivo != null && proyectil != null && puntoDisparo != null)
        {
            if (Time.time >= tiempoDisparo + intervaloDisparo)
            {
                Vector3 direccion = (objetivo.position - puntoDisparo.position).normalized;
                Quaternion rotacion = Quaternion.LookRotation(direccion);
                GameObject bala = Instantiate(proyectil, puntoDisparo.position, rotacion);
                bala.GetComponent<Rigidbody>().linearVelocity = direccion * 15f;
                tiempoDisparo = Time.time;
            }
        }
    }
}