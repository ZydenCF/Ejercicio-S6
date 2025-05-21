using UnityEngine;
using System;

public class Jugador : MonoBehaviour
{
    public float velocidad;
    public GameObject balaPrefab;
    public Transform puntoDisparo;
    public float tiempoEntreDisparos = 0.5f;
    private float tiempoUltimoDisparo = 0f;

    private Rigidbody rb;
    private delegate void AccionDisparo();
    private AccionDisparo disparar;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        disparar = delegate () {
            Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);
            tiempoUltimoDisparo = Time.time;
        };

        VidaJugador.OnJugadorMuerto += () => {
            this.enabled = false;
        };
    }

    void Update()
    {
        Mover();
        RotarHaciaMouse();
        if (Input.GetMouseButtonDown(0) && PuedeDisparar())
        {
            disparar();
        }
    }

    bool PuedeDisparar()
    {
        return Time.time >= tiempoUltimoDisparo + tiempoEntreDisparos;
    }

    void Mover()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direccion = new Vector3(horizontal, 0, vertical).normalized;
        Vector3 nuevaPos = transform.position + direccion * velocidad * Time.deltaTime;
        GetComponent<Rigidbody>().MovePosition(nuevaPos);
    }

    void RotarHaciaMouse()
    {
        Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plano = new Plane(Vector3.up, Vector3.zero);
        float distancia;
        if (plano.Raycast(rayo, out distancia))
        {
            Vector3 punto = rayo.GetPoint(distancia);
            Vector3 direccion = punto - transform.position;
            direccion.y = 0;
            if (direccion != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direccion);
            }
        }
    }

    void OnDestroy()
    {
        VidaJugador.OnJugadorMuerto -= () => {
            this.enabled = false;
        };
    }
}