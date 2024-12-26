using OpenTK.Graphics.OpenGL4;

namespace ForeverEngine.Graphics.FDebug {
    public sealed class RenderState {
        private bool _wireframe;
        private bool _lighting = true;

        static RenderState()
        {
        }

        private RenderState()
        {
        }

        public void ToggleLighting() {
            _lighting = !_lighting;
        }

        public void ToggleWireframe() {
            _wireframe = !_wireframe;

            GL.PolygonMode(MaterialFace.FrontAndBack, _wireframe ? PolygonMode.Line : PolygonMode.Fill);
            Console.WriteLine("Toggling Wireframe");
        }

        public static RenderState Instance { get; } = new RenderState();
    }
}