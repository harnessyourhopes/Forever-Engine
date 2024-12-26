using ForeverEngine.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace ForeverEngine.Components {
    public class MeshRenderer : Component
    {
        private Mesh mesh;
        private Material material;

        public Material GetMaterial() {
            return material;
        }

        public MeshRenderer(Mesh mesh, Material material) { 
            this.mesh = mesh;
            this.material = material;
        }

        public override void Render()
        {
            var shader = material.GetShader();
            shader.SetMatrix4("modelMatrix", GetComponent<Transform>().GetModelMatrix());
            shader.SetColor("material.color", material.GetColor());
            shader.SetFloat("material.specular", material.GetSpecular());
            shader.SetFloat("material.shininess", material.GetShininess());

            material.GetDiffuseMap().Use(TextureUnit.Texture0);
            material.GetSpecularMap().Use(TextureUnit.Texture1);
            
            if (material.GetAmbientOcclusionMap() != null)
            {
                material.GetAmbientOcclusionMap().Use(TextureUnit.Texture2);   
            }
            
            shader.SetInt("material.diffuseMap", 0);
            shader.SetInt("material.specularMap", 1);
            shader.SetInt("material.occlusionMap", 2);
            
            shader.SetInt("textureDivide", material.GetTextureDivide());
            shader.SetVec3("gameObject.scale", GetComponent<Transform>().GetScale());
            
            mesh.Render();
        }

        public override void Update(double deltaTime)
        {
            
        }
    }
}