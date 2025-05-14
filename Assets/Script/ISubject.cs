using UnityEngine;

public interface ISubject
{
    void Adjuntar(IObserver observador);
    void Quitar(IObserver observador);
    void Notificar();
}