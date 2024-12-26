namespace ForeverEngine.Components {
    public abstract class Component {
        protected GameObject parentGameObject;
        public abstract void Render();
        public abstract void Update(double deltaTime);

        public T GetComponent<T>() where T : Component {
            foreach(Component component in parentGameObject.GetComponents()) {
                if(component is T) {
                    return component as T;
                }
            }

            return null;
        }

        public void SetGameObject(GameObject gameObject) {
            parentGameObject = gameObject;
        }
    }
}