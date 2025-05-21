using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Boss : MonoBehaviour
{
    public float vidaMaxima = 3f;
    private float vidaActual;
    private IBossStrategy estrategiaActual;
    private int cantidadApariciones;

    public Transform objetivo;
    public float velocidad = 10f;
    private Vector3 direccionActual;
    private Rigidbody cuerpo;
    private bool puedeSaltar;

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
        int tipo = cantidadApariciones % 4;

        if (tipo == 0)
        {
            estrategiaActual = new BossPatron1();
        }
        else if (tipo == 1)
        {
            estrategiaActual = new BossPatron2();
        }
        else if (tipo == 2)
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
            direccionActual = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0f, UnityEngine.Random.Range(-1f, 1f)).normalized;
        }
        if (estrategiaActual is BossPatron2 || estrategiaActual is BossPatron3)
        {
            direccionActual = Vector3.Reflect(direccionActual, colision.contacts[0].normal);
        }
        if (estrategiaActual is BossPatron4)
        {
            puedeSaltar = true;
        }
    }

    public void Saltar()
    {
        if (puedeSaltar && cuerpo != null)
        {
            cuerpo.AddForce(Vector3.up * 300f);
            puedeSaltar = false;
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
}