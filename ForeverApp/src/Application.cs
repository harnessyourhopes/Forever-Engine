using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ForeverApp.EditorUI;
using ForeverEngine;
using ForeverEngine.Graphics;
using ForeverEngine.Graphics.FDebug;
using ForeverEngine.Editor;
using ForeverEngine.Components;
using ForeverEngine.Globals;
using ForeverEngine.Graphics.Geometry;
using ForeverEngine.Helpers;
using ImGuiNET;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Vector2 = System.Numerics.Vector2;

namespace ForeverApp {
    public class Application : GameWindow {
        public Shader shader;
        public List<GameObject> gameObjects;
        public Skybox Skybox;
        public Hierarchy Hierarchy = new();
        
        private Mesh _mesh;

        private RenderState _renderState;

        private ImGuiController _imGuiController;

        private Vector2 _screenDimensions;
        
        public float[] vertices = {
            // Front face (Z+)
            -0.5f,  0.5f,  0.5f,   0.0f,  0.0f,  1.0f,   0.0f, 1.0f,  // Top-left
            -0.5f, -0.5f,  0.5f,   0.0f,  0.0f,  1.0f,   0.0f, 0.0f,  // Bottom-left
             0.5f,  0.5f,  0.5f,   0.0f,  0.0f,  1.0f,   1.0f, 1.0f,  // Top-right
             0.5f,  0.5f,  0.5f,   0.0f,  0.0f,  1.0f,   1.0f, 1.0f,  // Top-right
            -0.5f, -0.5f,  0.5f,   0.0f,  0.0f,  1.0f,   0.0f, 0.0f,  // Bottom-left
             0.5f, -0.5f,  0.5f,   0.0f,  0.0f,  1.0f,   1.0f, 0.0f,  // Bottom-right

            // Back face (Z-)
             0.5f,  0.5f, -0.5f,   0.0f,  0.0f, -1.0f,   0.0f, 1.0f,  // Top-left
             0.5f, -0.5f, -0.5f,   0.0f,  0.0f, -1.0f,   0.0f, 0.0f,  // Bottom-left
            -0.5f,  0.5f, -0.5f,   0.0f,  0.0f, -1.0f,   1.0f, 1.0f,  // Top-right
            -0.5f,  0.5f, -0.5f,   0.0f,  0.0f, -1.0f,   1.0f, 1.0f,  // Top-right
             0.5f, -0.5f, -0.5f,   0.0f,  0.0f, -1.0f,   0.0f, 0.0f,  // Bottom-left
            -0.5f, -0.5f, -0.5f,   0.0f,  0.0f, -1.0f,   1.0f, 0.0f,  // Bottom-right

            // Right face (X+)
             0.5f,  0.5f,  0.5f,   1.0f,  0.0f,  0.0f,   0.0f, 1.0f,  // Top-left
             0.5f, -0.5f,  0.5f,   1.0f,  0.0f,  0.0f,   0.0f, 0.0f,  // Bottom-left
             0.5f,  0.5f, -0.5f,   1.0f,  0.0f,  0.0f,   1.0f, 1.0f,  // Top-right
             0.5f,  0.5f, -0.5f,   1.0f,  0.0f,  0.0f,   1.0f, 1.0f,  // Top-right
             0.5f, -0.5f,  0.5f,   1.0f,  0.0f,  0.0f,   0.0f, 0.0f,  // Bottom-left
             0.5f, -0.5f, -0.5f,   1.0f,  0.0f,  0.0f,   1.0f, 0.0f,  // Bottom-right

            // Left face (X-)
            -0.5f,  0.5f, -0.5f,  -1.0f,  0.0f,  0.0f,   0.0f, 1.0f,  // Top-left
            -0.5f, -0.5f, -0.5f,  -1.0f,  0.0f,  0.0f,   0.0f, 0.0f,  // Bottom-left
            -0.5f,  0.5f,  0.5f,  -1.0f,  0.0f,  0.0f,   1.0f, 1.0f,  // Top-right
            -0.5f,  0.5f,  0.5f,  -1.0f,  0.0f,  0.0f,   1.0f, 1.0f,  // Top-right
            -0.5f, -0.5f, -0.5f,  -1.0f,  0.0f,  0.0f,   0.0f, 0.0f,  // Bottom-left
            -0.5f, -0.5f,  0.5f,  -1.0f,  0.0f,  0.0f,   1.0f, 0.0f,  // Bottom-right

            // Top face (Y+)
            -0.5f,  0.5f, -0.5f,   0.0f,  1.0f,  0.0f,   0.0f, 1.0f,  // Top-left
            -0.5f,  0.5f,  0.5f,   0.0f,  1.0f,  0.0f,   0.0f, 0.0f,  // Bottom-left
             0.5f,  0.5f,  0.5f,   0.0f,  1.0f,  0.0f,   1.0f, 0.0f,  // Bottom-right
             0.5f,  0.5f,  0.5f,   0.0f,  1.0f,  0.0f,   1.0f, 0.0f,  // Bottom-right
             0.5f,  0.5f, -0.5f,   0.0f,  1.0f,  0.0f,   1.0f, 1.0f,  // Top-right
            -0.5f,  0.5f, -0.5f,   0.0f,  1.0f,  0.0f,   0.0f, 1.0f,  // Top-left
            
            // Bottom face (Y-)
            -0.5f, -0.5f, -0.5f,   0.0f, -1.0f,  0.0f,   0.0f, 0.0f,  // Bottom-left (0,0)
             0.5f, -0.5f, -0.5f,   0.0f, -1.0f,  0.0f,   1.0f, 0.0f,  // Bottom-right (1,0)
             0.5f, -0.5f,  0.5f,   0.0f, -1.0f,  0.0f,   1.0f, 1.0f,  // Top-right (1,1)
             0.5f, -0.5f,  0.5f,   0.0f, -1.0f,  0.0f,   1.0f, 1.0f,  // Top-right (1,1) - duplicate for second triangle
            -0.5f, -0.5f,  0.5f,   0.0f, -1.0f,  0.0f,   0.0f, 1.0f,  // Top-left (0,1)
            -0.5f, -0.5f, -0.5f,   0.0f, -1.0f,  0.0f,   0.0f, 0.0f   // Bottom-left (0,0) - duplicate for second triangle
        };
        
        public Application(int width, int height, string title) : base(
            GameWindowSettings.Default, 
            new NativeWindowSettings() {
            ClientSize = (width, height),
            Title = title
        }) {
            gameObjects = new List<GameObject>();
            shader = new Shader("assets/glsl/testv.glsl", "assets/glsl/testf.glsl");

            Skybox = new Skybox([
                "assets/gfx/skybox/autumn/2.bmp", // right
                "assets/gfx/skybox/autumn/4.bmp", // left
                "assets/gfx/skybox/autumn/6.bmp", // top
                "assets/gfx/skybox/autumn/5.bmp", // bottom
                "assets/gfx/skybox/autumn/1.bmp", // back 
                "assets/gfx/skybox/autumn/3.bmp", // front
            ]);
            
            _renderState = RenderState.Instance;
            _imGuiController = new ImGuiController(ClientSize.X, ClientSize.Y);
            _screenDimensions = new Vector2(width, height);
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            ForeverGlobals.InitializeGlobals();
            EditorSceneView.init();
            OnFramebufferResize(new FramebufferResizeEventArgs((int)_screenDimensions.X,(int) _screenDimensions.Y));
            

            _mesh = new Mesh(vertices, vertices, vertices);

            Transform[] transforms = [
                new (new Vector3(0, 0, 0), new Vector3(1, 1, 1)),
                new (new Vector3(1, 0, 0), new Vector3(1, 1, 1)),
                new (new Vector3(2, 0, 0), new Vector3(1, 1, 1)),
                new (new Vector3(3, 0, 0), new Vector3(1, 1, 1)),
                new (new Vector3(4, 0, 0), new Vector3(1, 1, 1)),
                new (new Vector3(5, 0, 0), new Vector3(1, 1, 1)),
                new (new Vector3(0, 0, 1), new Vector3(1, 1, 1)),
                new (new Vector3(1, 0, 1), new Vector3(1, 1, 1)),
                new (new Vector3(2, 0, 1), new Vector3(1, 1, 1)),
                new (new Vector3(3, 0, 1), new Vector3(1, 1, 1)),
                new (new Vector3(4, 0, 1), new Vector3(1, 1, 1)),
                new (new Vector3(5, 0, 1), new Vector3(1, 1, 1)),
                // ground
                new (new Vector3(0, -1, 0), new Vector3(50, 1, 50))
                
            ];

            Texture[][] textures = [
                [
                    new Texture("assets/gfx/textures/wood.png"), new Texture("assets/gfx/textures/wood_specular.png"), new Texture("assets/gfx/textures/wood_specular.png"),
                ],
                [
                    new Texture("assets/gfx/textures/sand.png"), new Texture("assets/gfx/textures/sand_specular.png"), new Texture("assets/gfx/textures/sand.png"),
                ],
                [
                    new Texture("assets/gfx/textures/plank.png"), new Texture("assets/gfx/textures/plank_specular.png"), new Texture("assets/gfx/textures/plank_specular.png"),
                ],
                [
                    new Texture("assets/gfx/textures/grass.png"), new Texture("assets/gfx/textures/grass_specular.png"), new Texture("assets/gfx/textures/grass_specular.png"),
                ],
                [
                    new Texture("assets/gfx/textures/brick.png"), new Texture("assets/gfx/textures/brick_specular.png"), new Texture("assets/gfx/textures/brick_ao.png")
                ],
                [
                   new Texture("assets/gfx/textures/studs/stud_diffuse.png"), new Texture("assets/gfx/textures/studs/stud_specular.png"), new Texture("assets/gfx/textures/studs/stud_specular.png"),
                ],
                [
                    new Texture("assets/gfx/textures/cobblestone.png"), new Texture("assets/gfx/textures/cobblestone_specular.png"), new Texture("assets/gfx/textures/cobblestone_specular.png"),
                ],
                [
                    new Texture("assets/gfx/textures/granite.png"), new Texture("assets/gfx/textures/granite_specular.png"), new Texture("assets/gfx/textures/granite_specular.png"),
                ],
                [
                    new Texture("assets/gfx/textures/marble.png"), new Texture("assets/gfx/textures/marble_specular.png"), new Texture("assets/gfx/textures/marble_specular.png"),
                ],
                [
                    new Texture("assets/gfx/textures/diamond_plate.png"), new Texture("assets/gfx/textures/diamond_plate_specular.png"), new Texture("assets/gfx/textures/diamond_plate_specular.png"),
                ],
                [
                    new Texture("assets/gfx/textures/fabric.png"), new Texture("assets/gfx/textures/fabric_specular.png"), new Texture("assets/gfx/textures/fabric_specular.png"),
                ],
                [
                    new Texture("assets/gfx/textures/missing.png"), new Texture("assets/gfx/textures/missing.png"), new Texture("assets/gfx/textures/missing.png"),
                ],
                [
                    new Texture("assets/gfx/textures/grass.png"), new Texture("assets/gfx/textures/grass_specular.png"), new Texture("assets/gfx/textures/grass_specular.png"),
                ],
            ];

            Color4[] colors = [
                Color4.BurlyWood, Color4.Cornsilk, Color4.SaddleBrown,
                Color4.Green, Color4.OrangeRed, Color4.Orange,
                Color4.Gray, Color4.White, Color4.Gray,
                Color4.Gray, Color4.Coral, Color4.White,
                Color4.LimeGreen
            ];

            string[] names =
            [
                "wood", "sand", "plank", "grass", "brick", "stud", "cobblestone", "granite", "marble", "diamond_plate", "fabric", "source engine joke", "ground"
            ];

            int[] divide = [
                2, 2, 2,
                2, 2, 1, 
                2, 2, 2, 
                2, 2, 1, 2
            ];

            for(int i = 0; i < 13; i++) {               
                Material material = new Material(shader, textures[i][0], textures[i][1], textures[i][2], divide[i]);
                material.SetColor(colors[i]);
                material.SetShininess(32f);
                material.SetSpecular(1f);

                MeshRenderer meshRenderer = new MeshRenderer(_mesh, material);

                GameObject gameObject = new GameObject();
                gameObject.name = names[i];

                gameObject.AddComponent(meshRenderer);
                gameObject.AddComponent(transforms[i]);
                GlobalHierarchy.AddToWorkspace(gameObject);
            }

            GL.ClearColor(Color4.Black);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.Enable(EnableCap.Multisample);
            GL.Enable(EnableCap.Blend);
            
            //GL.Enable(EnableCap.CullFace);
            //GL.CullFace(CullFaceMode.Back);
            //GL.FrontFace(FrontFaceDirection.Ccw);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            ImGuiController.CheckGLError("End of frame");

            Camera camera = EditorSceneView.GetSceneViewCamera();

            Matrix4 projectionMatrix = camera.GetProjectionMatrix();
            Matrix4 viewMatrix = camera.GetViewMatrix();
            

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

            shader.Use();
            shader.SetMatrix4("viewMatrix", viewMatrix);
            shader.SetMatrix4("projectionMatrix", projectionMatrix);
            shader.SetVec3("viewPosition", camera.GetPosition());

            shader.SetColor("directionalLight.color", Color4.White);
            shader.SetVec3("directionalLight.direction", 0, -90, 90);
            shader.SetFloat("directionalLight.brightness", 1f);
            
            // /shader.SetFloat("fog.density", 2f);
            // shader.SetFloat("fog.near", 2f);
            // shader.SetFloat("fog.far", 6f);
            // shader.SetColor("fog.color", Color4.White);
            
            EditorSceneView.BindFramebuffer();
            
            GL.Enable(EnableCap.CullFace);
            
            GL.PushAttrib(AttribMask.ViewportBit);
            {
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                GL.ClearColor(Color4.White);

                if (GlobalHierarchy.GetSelectedObject() != null)
                {
                    //ImGuizmo.Enable(true);
                    //ImGuizmo.SetOrthographic(false);
                   
                    //ImGuizmo.SetRect(ImGui.GetWindowPos().X, ImGui.GetWindowPos().Y, _screenDimensions.X, _screenDimensions.Y);

                    if (GlobalHierarchy.GetSelectedObject() is GameObject gO && gO.GetComponent<MeshRenderer>() != null)
                    {   
                        /*
                        GL.Enable(EnableCap.StencilTest);
                        GL.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Replace);
                        GL.StencilFunc(StencilFunction.Always, 1, 0xFF);
                        GL.StencilMask(0xFF);
                        */
                    }
                    //ImGuizmo.Enable(false);
                }
                
                GlobalHierarchy.Render();
                Skybox.Render(projectionMatrix, viewMatrix);
            }
            GL.PopAttrib();
            
            GL.Disable(EnableCap.CullFace);
            
            EditorSceneView.UnbindFramebuffer();
            
            // Enable Docking
            ImGui.DockSpaceOverViewport();
            ImGui.ShowDemoWindow();
            EditorMainMenu.Render();
            EditorMainMenu.Render();
            EditorSceneHierarchy.Render();
            EditorSceneView.Render();
            EditorProperties.Render();
            EditorTopBar.Render();
            
            _imGuiController.Render();
            
            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            KeyboardState input = KeyboardState;
            MouseState mouse = MouseState;

            if(input.IsKeyReleased(Keys.F1)) {
                _renderState.ToggleWireframe();
            }
            if(EditorSceneView.IsMouseOver())
                EditorSceneView.GetSceneViewCamera().Input((float)args.Time, input, mouse);
        
            Hierarchy.Update((float) args.Time);
            _imGuiController.Update(this, (float)args.Time);
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
        {
            base.OnFramebufferResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);
            _imGuiController.WindowResized(e.Width, e.Height);
            EditorSceneView.ResizeFramebuffer(new Vector2(e.Width, e.Height));
            _screenDimensions = new Vector2(e.Width, e.Height);
        }
        
        protected override void OnTextInput(TextInputEventArgs e)
        {
            base.OnTextInput(e);
            
            _imGuiController.PressChar((char)e.Unicode);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            
            _imGuiController.MouseScroll(e.Offset);
        }
    }
}