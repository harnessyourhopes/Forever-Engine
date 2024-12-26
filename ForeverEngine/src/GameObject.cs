using ForeverEngine.Components;

namespace ForeverEngine {
    public class GameObject
    {
        private string _name = "GameObject";
        private List<Component> _components = new();

        public String name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public List<Component> GetComponents() {
            return _components;
        }

        public T AddComponent<T>(T component) where T : Component {
            component.SetGameObject(this);
            _components.Add(component);
            return component;
        }

        public T GetComponent<T>() where T : Component {
            foreach(Component component in _components) {
                if(component is T) {
                    return component as T;
                }
            }

            return null;
        }

        public void Update(double deltaTime) {
            foreach (var component in _components) {
                component.Update(deltaTime);
            }
        }

        public void Render() {
            foreach (var component in _components) {
                component.Render();
            }
        }
    }
}