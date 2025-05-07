using UnityEngine;

public class EnemigoZigZag : EnemigoBase
{
    private float tiempo;

    protected override void Iniciar()
    {
        velocidad = 15;
        tiempo = 0;
    }

    protected override void ActualizarMovimiento()
    {
        if (jugador != null)
        {
            tiempo += Time.deltaTime;

            Vector3 direccion = jugador.position - transform.position;
            direccion.y = 0;

            if (direccion != Vector3.zero)
            {
                Vector3 lateral = Vector3.Cross(Vector3.up, direccion).normalized;
                Vector3 zigzag = direccion.normalized + lateral * Mathf.Sin(tiempo * 20);

                transform.rotation = Quaternion.LookRotation(zigzag);
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
