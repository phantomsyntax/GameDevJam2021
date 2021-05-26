using UnityEngine;

namespace PhantomSyntax.Scripts.Utility {
    public class LoopTileOffset : MonoBehaviour {
        [Header("Renderer and Material Settings")]
        [SerializeField] private Renderer _meshRenderer;
        [SerializeField] private float tileOffsetSpeed = 0.8f;
        private Material _meshRendererMaterial;
        
        // Start is called before the first frame update
        void Start()
        {
            if (!_meshRenderer) {
                _meshRenderer = GetComponent<Renderer>();
            }
            // Grab the current material on the renderer
            _meshRendererMaterial = _meshRenderer.material;
            
        }

        // Update is called once per frame
        void Update() {
            // Scroll the tile material offset on the X axis to simulate movement
            float offset = Time.time * tileOffsetSpeed;
            Vector2 offsetVector = new Vector2(0.0f, -offset);
            // Offset main material texture
            _meshRendererMaterial.mainTextureOffset = offsetVector;
            // Offset secondary material map
            _meshRendererMaterial.SetTextureOffset("_DetailAlbedoMap", offsetVector);
        }
    }
}
