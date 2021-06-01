using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.PlayerLoop;

namespace PhantomSyntax.Scripts.ScriptableObjects {
    [CreateAssetMenu]
    public class GameEvent : ScriptableObject {
        private List<GameEventListener> listeners = new List<GameEventListener>();

        public void Triggered() {
            foreach (GameEventListener gameEventListener in listeners) {
                gameEventListener.EventTriggered();
            }
        }

        public void Register(GameEventListener listener) {
            listeners.Add(listener);
        }

        public void Unregister(GameEventListener listener) {
            listeners.Remove(listener);
        }
    }
}