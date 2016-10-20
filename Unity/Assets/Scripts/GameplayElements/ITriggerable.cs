using UnityEngine;

namespace Assets.Scripts.GameplayElements
{
    interface ITriggerable
    {
        void TriggerEnter(Collider col);

        void TriggerExit(Collider col);
    }
}
