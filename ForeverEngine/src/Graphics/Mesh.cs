using OpenTK.Graphics.OpenGL;

namespace ForeverEngine.Graphics {
    public class Mesh {
        private int vao;
        private int vbo;
        private int vertexCount;

        public int GetVertexArrayObject() {
            return vao;
        }

        public int GetVertexCount() {
            return vertexCount;
        }

        public Mesh(float[] vertices, float[] normals, float[] textureCoordinates) {
            vertexCount = vertices.Length / 3;

            vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            // bind vertices to vertex buffer object
            GL.BufferData(BufferTarget.ArrayBuffer, 
            vertices.Length * sizeof(float),
            vertices,
            BufferUsageHint.StaticDraw);
            

            // vertices
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // normals
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            // texture coords
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
            GL.EnableVertexAttribArray(2);


            // Unbind buffer 
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void Render() {
            GL.BindVertexArray(GetVertexArrayObject());

            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);

            GL.BindVertexArray(0);
        }

        public void Dispose() {
            // 1: vertices 2: normals
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(vbo);
            GL.BindVertexArray(0);
            GL.DeleteVertexArray(vao);
        }
    }
}