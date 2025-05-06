using System;
using UnityEngine;

namespace Ystanovka.Systems
{
    public abstract class AbstractSystem<T> : MonoBehaviour
    {
        public abstract event Action<T> OnValueChanged;
    }
}