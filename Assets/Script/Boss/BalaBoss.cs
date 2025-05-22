using UnityEngine;

public class BalaBoss : MonoBehaviour
{
    public float velocidad = 15f;
    public float tiempoDestruccion = 3f;

    void Start()
    {
        Destroy(gameObject, tiempoDestruccion);

        GameObject boss = GameObject.FindWithTag("Boss");
        if (boss != null)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), boss.GetComponent<Collider>());
        }
    }

    void Update()
    {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }

    void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag("Player"))
        {
            VidaJugador vida = otro.GetComponent<VidaJugador>();
            if (vida != null)
            {
                vida.RecibirDaño(2);
            }
            Destroy(gameObject);
        }
    }
}