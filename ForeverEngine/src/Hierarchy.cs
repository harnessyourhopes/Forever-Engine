using System.Runtime.CompilerServices;
using ForeverEngine.Graphics;
using ForeverEngine.Graphics.Lighting;

namespace ForeverEngine {
    public class Hierarchy
    {
        private Camera _camera;
        private Skybox _skybox;
        private List<Light> _lights;
        private List<GameObject> _gameObjects;
        
        public object SelectedObject;

        public Camera Camera
        {
            get
            {
                return _camera;
            }
        }
        
        public Hierarchy() {
            _lights = new List<Light>();
            _gameObjects = new List<GameObject>();
        }

        public void AddLight(Light light) {
            _lights.Add(light);
        }

        public void AddGameObject(GameObject gameObject) {
            _gameObjects.Add(gameObject);
        }

        public List<Light> GetLights()
        {
            return _lights;
        }
        
        public List<GameObject> GetGameObjects()
        {
            return _gameObjects;
        }
        
        public void Render()
        {
            // Render order
            // 1 Lights
            // 2 Game Objects
            // 3 Image UI
            // 4 Immediate Mode GUI
            
            foreach (var light in _lights)
            {
                //light.Render();   
            }
            
            foreach (var gameObject in _gameObjects) {
                if (gameObject == SelectedObject)
                {
                   // float modelMatrix = 
                }
            
                gameObject.Render();
            }
        }

        public void Update(float deltaTime)
        {
            // Render order
            // 1 Lights
            // 2 Game Objects
            // 3 Image UI
            // 4 Immediate Mode GUI
            
            foreach (var light in _lights)
            {
                //light.Render();   
            }
            
            foreach (var gameObject in _gameObjects)
            {
                gameObject.Update(deltaTime);
            }
        }
    }
}