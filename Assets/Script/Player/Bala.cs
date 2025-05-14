using UnityEngine;
using System;

public class Bala : MonoBehaviour
{
    public float velocidad;
    public float tiempoDestruccion;

    public static Action<GameObject> OnImpacto;

    void Start()
    {
        Destroy(gameObject, tiempoDestruccion);
        Physics.IgnoreCollision(GetComponent<Collider>(),
        GameObject.FindWithTag("Player").GetComponent<Collider>());
    }

    void Update()
    {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }

    void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag("Enemigo"))
        {
            OnImpacto?.Invoke(otro.gameObject);

            EnemigoEscudero enemigo = otro.GetComponent<EnemigoEscudero>();
            if (enemigo != null)
            {
                enemigo.RecibirDaño();
            }
            else
            {
                Destroy(otro.gameObject);
            }
            Destroy(gameObject);
        }
    }
}