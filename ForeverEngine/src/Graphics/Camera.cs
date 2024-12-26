using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace ForeverEngine.Graphics {
    public class Camera {
        private Matrix4 _viewMatrix;
        private Matrix4 _projectionMatrix;

        private bool _viewMatrixNeedsUpdated = true;
        private bool _projectionMatrixNeedsUpdated = true;

        private Vector3 _position;
        private Vector3 _up;
        private Vector3 _forward;

        private float _yaw;
        private float _pitch;

        private float _zNear = 0.1f;
        private float _zFar = 1000f;
        private float _fov = 70;
        private float _sensitivity = 0.2f;

        private float _aspectRatio;

        public Vector3 GetPosition() {
            return this._position;
        }

        public Matrix4 GetViewMatrix() {
            if(_viewMatrixNeedsUpdated) {
                _viewMatrix = Matrix4.LookAt(_position, _position + _forward, _up);
            }
            
            return _viewMatrix;
        }

        public Matrix4 GetProjectionMatrix() {
            if(_projectionMatrixNeedsUpdated) {
                _projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(_fov), _aspectRatio, _zNear, _zFar);
            }
            
            return _projectionMatrix;
        }

        public Camera(float aspectRatio) {
            this._aspectRatio = aspectRatio;
            _position = new Vector3(0, 0, -3);
            _forward = new Vector3(0, 0, -1);
            _up = new Vector3(0, 1, 0);

            GetViewMatrix();
            GetProjectionMatrix();
        }
        Vector2 _lastPos;
        bool _firstMove = true;
        public void Input(float deltaTime, KeyboardState input, MouseState mouse) {
            if (!mouse.IsButtonDown(MouseButton.Button2))
            {
                _firstMove = true;
                return;
            }
            float speed = 12f * deltaTime;
            if (input.IsKeyDown(Keys.W))
            {
                _position += _forward * speed; //Forward 
            }

            if (input.IsKeyDown(Keys.S))
            {
                _position -= _forward * speed; //Backwards
            }

            if (input.IsKeyDown(Keys.A))
            {
                _position -= Vector3.Normalize(Vector3.Cross(_forward, _up)) * speed; //Left
            }

            if (input.IsKeyDown(Keys.D))
            {
                _position += Vector3.Normalize(Vector3.Cross(_forward, _up)) * speed; //Right
            }

            if (input.IsKeyDown(Keys.Space))
            {
                _position += _up * speed; //Up 
            }

            if (input.IsKeyDown(Keys.LeftShift))
            {
                _position -= _up * speed; //Down
            }

            if(input.IsKeyDown(Keys.Equal)) {
                TranslateFOV( 40 * deltaTime);
            }

            if(input.IsKeyDown(Keys.Minus)) {
                TranslateFOV(-40 * deltaTime);
            }

            if (_firstMove)
            {
                _lastPos = new Vector2(mouse.X, mouse.Y);
                _firstMove = false;
            }
            else
            {
                float deltaX = mouse.X - _lastPos.X;
                float deltaY = mouse.Y - _lastPos.Y;
                _lastPos = new Vector2(mouse.X, mouse.Y);

                _yaw += deltaX * _sensitivity;
                _pitch -= deltaY * _sensitivity;

                _pitch = Math.Clamp(_pitch, -89, 89);
            }
            _forward.X = (float)Math.Cos(MathHelper.DegreesToRadians(_pitch)) * (float)Math.Cos(MathHelper.DegreesToRadians(_yaw));
            _forward.Y = (float)Math.Sin(MathHelper.DegreesToRadians(_pitch));
            _forward.Z = (float)Math.Cos(MathHelper.DegreesToRadians(_pitch)) * (float)Math.Sin(MathHelper.DegreesToRadians(_yaw));
            _forward = Vector3.Normalize(_forward);
            _viewMatrixNeedsUpdated = true;
            
        }

        public void TranslatePosition(Vector3 translation) {
            _position += translation * _forward;
            _viewMatrixNeedsUpdated = true;
        }

        public void TranslatePosition(float x, float y, float z) {
            TranslatePosition(new Vector3(x, y, z));
        }

        public void TranslatePitch(float pitch) {
            this._pitch += pitch;
            _viewMatrixNeedsUpdated = true;
        }

        public void TranslateYaw(float yaw) {
            this._yaw += yaw;
            _viewMatrixNeedsUpdated = true;
        }

        public void SetFOV(float fov) {
            this._fov = Math.Clamp(fov, 40, 120);
            _projectionMatrixNeedsUpdated = true;
        }

        public void TranslateFOV(float amount) {
            _fov -= amount;
            _fov = Math.Clamp(_fov, 40, 120);
            _projectionMatrixNeedsUpdated = true;
        }

        public void Resize(float aspectRatio) {
            _aspectRatio = aspectRatio;
            _projectionMatrixNeedsUpdated = true;
        }
    }
}