using OpenTK.Mathematics;

namespace ForeverEngine.Graphics {
    public class Material(Shader shader, Texture diffuse, Texture specularTexture, Texture ao, int textureDivide)
    {
        private Color4 color = Color4.Orange;
        private float specular = 0.5f;
        private float shininess = 32f;
        private Texture ambientOcclusionTexture = ao;
        private Shader shader = shader;

        public Shader GetShader() {
            return shader;
        }

        public void SetShader(Shader shader) {
            this.shader = shader;
        }

        public Color4 GetColor() {
            return color;
        }

        public void SetColor(float x, float y, float z) {
            this.color = new Color4(x, y, z, 1);
        }

        public void SetColor(Vector3 color) {
            this.color = new Color4(color.X, color.Y, color.Z, 1);
        }

        public void SetColor(Color4 color) {
            this.color = color;
        }

        public float GetSpecular() {
            return specular;
        }

        public void SetSpecular(float specular) {
            this.specular = specular;
        }

        public float GetShininess() {
            return shininess;
        }

        public void SetShininess(float shininess) {
            this.shininess = shininess;
        }

        public Texture GetDiffuseMap() {
            return diffuse;
        }

        public Texture GetSpecularMap() {
            return specularTexture;
        }

        public Texture GetAmbientOcclusionMap()
        {
            return ambientOcclusionTexture;
        }

        public int GetTextureDivide() {
            return textureDivide;
        }
    }
}