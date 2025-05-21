using UnityEngine;

public class BossPatron4 : IBossStrategy
{
    public void Ejecutar(Boss boss)
    {
        if (boss.objetivo != null)
        {
            Vector3 direccion = (boss.objetivo.position - boss.transform.position).normalized;
            boss.transform.position += direccion * boss.velocidad * Time.deltaTime;
            boss.Saltar();
        }
    }
}

