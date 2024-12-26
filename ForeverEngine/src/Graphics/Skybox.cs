using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using StbImageSharp;

namespace ForeverEngine.Graphics {
    public class Skybox {        
        private int handle;
        private string[] paths;
        private Shader shader;

        private int vao;
        private int vbo;

        float[] skyboxVertices = {
            -1f,  1f, -1f,
            -1f, -1f, -1f,
            1f, -1f, -1f,
            1f, -1f, -1f,
            1f,  1f, -1f,
            -1f,  1f, -1f,

            -1f, -1f,  1f,
            -1f, -1f, -1f,
            -1f,  1f, -1f,
            -1f,  1f, -1f,
            -1f,  1f,  1f,
            -1f, -1f,  1f,

            1f, -1f, -1f,
            1f, -1f,  1f,
            1f,  1f,  1f,
            1f,  1f,  1f,
            1f,  1f, -1f,
            1f, -1f, -1f,

            -1f, -1f,  1f,
            -1f,  1f,  1f,
            1f,  1f,  1f,
            1f,  1f,  1f,
            1f, -1f,  1f,
            -1f, -1f,  1f,

            -1f,  1f, -1f,
            1f,  1f, -1f,
            1f,  1f,  1f,
            1f,  1f,  1f,
            -1f,  1f,  1f,
            -1f,  1f, -1f,

            -1f, -1f, -1f,
            -1f, -1f,  1f,
            1f, -1f, -1f,
            1f, -1f, -1f,
            -1f, -1f,  1f,
            1f, -1f,  1f
        };

        public Skybox(string[] paths) {
            this.paths = paths; 
            LoadSkybox();
            GenerateMesh();
            shader = new Shader("assets/glsl/skyboxv.glsl", "assets/glsl/skyboxf.glsl");
            shader.SetInt("skybox", 0); 
        }

        private void LoadSkybox() {
            handle = GL.GenTexture();
            GL.BindTexture(TextureTarget.TextureCubeMap, handle);

            for(int i = 0; i < 6; i++) {
                //StbImage.stbi_set_flip_vertically_on_load(1);

                using (Stream stream = File.OpenRead(paths[i])) {
                    ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlue);
                    GL.TexImage2D(TextureTarget.TextureCubeMapPositiveX + i, 0, PixelInternalFormat.Rgb,
                                image.Width, image.Height, 0, PixelFormat.Rgb, PixelType.UnsignedByte, image.Data);
                }
            }

            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapR, (int)TextureWrapMode.ClampToEdge);

            GL.BindTexture(TextureTarget.TextureCubeMap, 0);
        }

        private void GenerateMesh() {
            vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, skyboxVertices.Length * sizeof(float), skyboxVertices, BufferUsageHint.StaticDraw);

            // vertices
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

            // Unbind buffer 
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void Render(Matrix4 projection, Matrix4 view) {
            GL.DepthFunc(DepthFunction.Lequal); 

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.TextureCubeMap, handle);

            Matrix4 skyMatrix = Matrix4.Invert(view);

            shader.Use();
            shader.SetMatrix4("projection", Matrix4.CreateScale(0.2f) * projection);
            shader.SetMatrix4("view", skyMatrix);

            GL.BindVertexArray(vao);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);

            GL.BindVertexArray(0);
            GL.DepthFunc(DepthFunction.Less);
        }
    }
}   