using UnityEngine;
using System;

public class Bala : MonoBehaviour
{
    public float velocidad = 20f;
    public float tiempoDestruccion = 5f;
    public static Action<GameObject> OnImpacto;

    void Start()
    {
        Destroy(gameObject, tiempoDestruccion);

        GameObject jugador = GameObject.FindWithTag("Player");
        if (jugador != null)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), jugador.GetComponent<Collider>());
        }
    }

    void Update()
    {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }

    void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag("Enemigo") || otro.CompareTag("Boss"))
        {
            OnImpacto?.Invoke(otro.gameObject);

            if (otro.CompareTag("Boss"))
            {
                Boss boss = otro.GetComponent<Boss>();
                if (boss != null)
                {
                    boss.RecibirDanio(1f);
                }
            }
            else
            {
                EnemigoEscudero enemigo = otro.GetComponent<EnemigoEscudero>();
                if (enemigo != null)
                {
                    enemigo.RecibirDaño();
                }
                else
                {
                    Destroy(otro.gameObject);
                }
            }

            Destroy(gameObject);
        }
    }
}