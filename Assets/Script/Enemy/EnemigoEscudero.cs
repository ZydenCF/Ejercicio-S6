using UnityEngine;
public class EnemigoEscudero : EnemigoBase
{
    public int vidaMaxima = 2;
    private int vidaActual;
    public float velocidadMovimiento = 12f;  

    void Start()
    {
       
        vidaActual = vidaMaxima;

        ActualizarColorPorVida();

        velocidad = velocidadMovimiento;
    }

    protected override void Iniciar()
    {
       
        if (vidaActual <= 0)
        {
            vidaActual = vidaMaxima;
            ActualizarColorPorVida();
        }

        if (velocidad <= 0)
        {
            velocidad = velocidadMovimiento;
        }
    }

    protected override void ActualizarMovimiento()
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
        else
        {
            GameObject jugadorObj = GameObject.FindWithTag("Player");
            if (jugadorObj != null)
            {
                jugador = jugadorObj.transform;
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

    void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag("Bala"))
        {
            RecibirDaño();
        }
    }
    public void RecibirDaño()
    {
        vidaActual--;
        ActualizarColorPorVida();

        if (vidaActual <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void ActualizarColorPorVida()
    {
        if (GetComponent<Renderer>() != null)
        {
            if (vidaActual == vidaMaxima)
            {
                GetComponent<Renderer>().material.color = Color.blue;
            }
            else if (vidaActual == 2)
            {
                GetComponent<Renderer>().material.color = Color.magenta;
            }
        }
    }
}