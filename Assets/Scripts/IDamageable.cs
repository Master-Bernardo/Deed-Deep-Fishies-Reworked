using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable<T>
{
    void TakeDamage(T damage);
}

//for objects which needs to be pushed upon taking tamage etc...
public interface IPusheable<T>
{
    void Push(T force);
}
