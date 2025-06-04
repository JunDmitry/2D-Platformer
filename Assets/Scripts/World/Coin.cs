using System;
using UnityEngine;

public class Coin : MonoBehaviour, IIntractable
{
    [SerializeField] private int _value;

    public event Action<Coin> Collected;

    public int Value => _value;

    public void Interact()
    {
        Collected?.Invoke(this);
    }
}