using OpenTK.Mathematics;

namespace ForeverEngine.Components {
    public class Transform : Component {
        Matrix4 modelMatrix;
        private bool modelMatrixNeedsUpdated = true;

        private Vector3 position;
        private Quaternion rotation;
        private Vector3 scale;

        public Vector3 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                this.modelMatrixNeedsUpdated = true;
            }
        }

        public Quaternion Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
                this.modelMatrixNeedsUpdated = true;
            }
        }

        public Vector3 Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
                this.modelMatrixNeedsUpdated = true;
            }
        }

        public Transform() {
            modelMatrix = new Matrix4();
            position = new Vector3(0, 0, 0);
            rotation = new Quaternion(0, 0, 0);
            scale = new Vector3(1, 1, 1);
        }

        public Transform(Vector3 position, Vector3 scale) {
            modelMatrix = new Matrix4();
            this.position = position;
            rotation = new Quaternion(0, 0, 0);
            this.scale = scale;
            UpdateModelMatrix();
        }

        private void UpdateModelMatrix()
        {
            // prevents fucking of mesh on negative scaling
            Vector3 absScale = new Vector3(float.Abs(scale.X), float.Abs(scale.Y), float.Abs(scale.Z));
            
            modelMatrix = Matrix4.CreateScale(absScale) * 
                          Matrix4.CreateTranslation(position) *
                          Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Z)) *
                          Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotation.Y)) *
                          Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotation.X));
            modelMatrixNeedsUpdated = false;
        }

        public Matrix4 GetModelMatrix() {
            if(modelMatrixNeedsUpdated) {
                UpdateModelMatrix();
            }

            return modelMatrix;
        }

        public Vector3 GetPosition() {
            return position;
        }

        public void SetPosition(Vector3 position) {
            this.position = position;
            modelMatrixNeedsUpdated = true;
        }

        public void SetPosition(float x, float y, float z) {
            position = new Vector3(x, y, z);
            modelMatrixNeedsUpdated = true;
        }

        public Vector3 GetScale() {
            return scale;
        }

        public void SetScale(Vector3 scale) {
            this.scale = scale;
            modelMatrixNeedsUpdated = true;
        }

        public void SetScale(float x, float y, float z) {
            scale = new Vector3(x, y, z);
            modelMatrixNeedsUpdated = true;
        }

        public void SetRotation(Quaternion rotation) {
            this.rotation = rotation;
            modelMatrixNeedsUpdated = true;
        }

        public void SetRotation(Vector3 rotation) {
            this.rotation = new Quaternion(rotation.X, rotation.Y, rotation.Z);
            modelMatrixNeedsUpdated = true;
        }

        public void SetRotation(float x, float y, float z) {
            rotation = new Quaternion(x, y, z);
            modelMatrixNeedsUpdated = true;
        }

        public Quaternion GetRotation() {
            return rotation;
        }

        public override void Render()
        {
            //throw new NotImplementedException();
        }

        public override void Update(double deltaTime)
        {
            //throw new NotImplementedException();
        }
    }
}