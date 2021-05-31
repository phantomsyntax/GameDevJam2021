using UnityEngine;
using UnityEngine.Events;

namespace PhantomSyntax.Scripts.ScriptableObjects {
    public class GameEventListener : MonoBehaviour {
        [Header("GameEvent Observer Settings")]
        [SerializeField] private GameEvent gameEvent;
        [SerializeField] private UnityEvent unityEvent;
        
        private void OnEnable() {
            if (gameEvent != null) {
                gameEvent.Register(this);
            }
        }

        public void EventTriggered() {
            unityEvent.Invoke();
        }
        
        private void OnDisable() {
            if (gameEvent != null) {
                gameEvent.Unregister(this);
            }
        }
    }
}