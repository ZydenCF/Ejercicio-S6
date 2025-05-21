using UnityEngine;

public class BossPatron3 : IBossStrategy
{
    private bool direccionAsignada;

    public void Ejecutar(Boss boss)
    {
        if (!direccionAsignada)
        {
            Vector3 aleatorio = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0f, UnityEngine.Random.Range(-1f, 1f)).normalized;
            boss.EstablecerDireccion(aleatorio);
            direccionAsignada = true;
        }
        boss.transform.Rotate(Vector3.up * 180f * Time.deltaTime);
        boss.transform.position += boss.ObtenerDireccion() * boss.velocidad * Time.deltaTime;
    }
}