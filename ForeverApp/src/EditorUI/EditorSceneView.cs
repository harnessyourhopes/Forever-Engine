using System;
using System.Numerics;
using ForeverEngine.Graphics;
using ImGuiNET;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace ForeverApp.EditorUI;

// https://github.com/mono/opentk/blob/main/Source/Examples/OpenGL/1.x/FramebufferObject.cs

public class EditorSceneView
{
    private static uint _colorTexture;
    private static uint _frameBuffer;
    private static uint _renderBuffer;

    private static Camera _sceneViewCamera;

    private static Vector2 _targetResolution = new(500, 500);
    private static float _aspectRatio;

    private static bool _itemHovered;
    
    public static void init()
    {
        _aspectRatio = _targetResolution.X / _targetResolution.Y;
        
        GL.GenTextures(1, out _colorTexture);
        GL.BindTexture(TextureTarget.Texture2D, _colorTexture);
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, (int)_targetResolution.X, (int)_targetResolution.Y, 0, PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToBorder);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToBorder);
        
        #region Framebuffer Generation
            GL.Ext.GenFramebuffers(1, out _frameBuffer);
            BindFramebuffer();
            GL.Ext.FramebufferTexture2D(FramebufferTarget.FramebufferExt, FramebufferAttachment.ColorAttachment0Ext, TextureTarget.Texture2D, _colorTexture, 0);
        #endregion
        #region Framebuffer Object Generation Error

            switch (GL.Ext.CheckFramebufferStatus(FramebufferTarget.FramebufferExt))
            {
                case FramebufferErrorCode.FramebufferCompleteExt:
                    {
                        Console.WriteLine("FBO: The framebuffer is complete and valid for rendering.");
                        break;
                    }
                case FramebufferErrorCode.FramebufferIncompleteAttachmentExt:
                    {
                        Console.WriteLine("FBO: One or more attachment points are not framebuffer attachment complete. This could mean there’s no texture attached or the format isn’t renderable. For color textures this means the base format must be RGB or RGBA and for depth textures it must be a DEPTH_COMPONENT format. Other causes of this error are that the width or height is zero or the z-offset is out of range in case of render to volume.");
                        break;
                    }
                case FramebufferErrorCode.FramebufferIncompleteMissingAttachmentExt:
                    {
                        Console.WriteLine("FBO: There are no attachments.");
                        break;
                    }
                case FramebufferErrorCode.FramebufferIncompleteDimensionsExt:
                    {
                        Console.WriteLine("FBO: Attachments are of different size. All attachments must have the same width and height.");
                        break;
                    }
                case FramebufferErrorCode.FramebufferIncompleteFormatsExt:
                    {
                        Console.WriteLine("FBO: The color attachments have different format. All color attachments must have the same format.");
                        break;
                    }
                case FramebufferErrorCode.FramebufferIncompleteDrawBufferExt:
                    {
                        Console.WriteLine("FBO: An attachment point referenced by GL.DrawBuffers() doesn’t have an attachment.");
                        break;
                    }
                case FramebufferErrorCode.FramebufferIncompleteReadBufferExt:
                    {
                        Console.WriteLine("FBO: The attachment point referenced by GL.ReadBuffers() doesn’t have an attachment.");
                        break;
                    }
                case FramebufferErrorCode.FramebufferUnsupportedExt:
                    {
                        Console.WriteLine("FBO: This particular FBO configuration is not supported by the implementation.");
                        break;
                    }
                default:
                    {
                        Console.WriteLine("FBO: Status unknown. (yes, this is really bad.)");
                        break;
                    }
            }

            // using FBO might have changed states, e.g. the FBO might not support stereoscopic views or double buffering
            int[] queryinfo = new int[6];
            GL.GetInteger(GetPName.MaxColorAttachmentsExt, out queryinfo[0]);
            GL.GetInteger(GetPName.AuxBuffers, out queryinfo[1]);
            GL.GetInteger(GetPName.MaxDrawBuffers, out queryinfo[2]);
            GL.GetInteger(GetPName.Stereo, out queryinfo[3]);
            GL.GetInteger(GetPName.Samples, out queryinfo[4]);
            GL.GetInteger(GetPName.Doublebuffer, out queryinfo[5]);
            Console.WriteLine("max. ColorBuffers: " + queryinfo[0] + " max. AuxBuffers: " + queryinfo[1] + " max. DrawBuffers: " + queryinfo[2] +
                               "\nStereo: " + queryinfo[3] + " Samples: " + queryinfo[4] + " DoubleBuffer: " + queryinfo[5]);

            Console.WriteLine("Last GL Error: " + GL.GetError());

            #endregion Test for Error

            GL.GenRenderbuffers(1, out _renderBuffer);
            BindRenderbuffer();
            GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.Depth24Stencil8, (int)_targetResolution.X, (int)_targetResolution.Y);
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthStencilAttachment, RenderbufferTarget.Renderbuffer, _renderBuffer);
            UnbindRenderbuffer();
            
        
        UnbindFramebuffer();

        _sceneViewCamera = new Camera(_aspectRatio);
    }

    private Vector2 last_window_dimensions = Vector2.Zero;

    public static void Render()
    {
        ImGui.Begin("Scene View");

        Vector2 viewportSize = new Vector2(ImGui.GetContentRegionAvail().X, ImGui.GetContentRegionAvail().Y);
        Vector2 position = ImGui.GetCursorScreenPos();

        #region hover event math
            Vector2 windowPos = ImGui.GetWindowPos();
            Vector2 windowSize = ImGui.GetWindowSize();
            Vector2 cursorPos = ImGui.GetCursorPos();

            Vector2 contentRegionMin = windowPos + cursorPos;
            Vector2 contentRegionMax = windowPos + windowSize;

            Vector2 mouseCursor = ImGui.GetMousePos();
            // i hate this so much
            if (mouseCursor.X > contentRegionMin.X &&
                mouseCursor.Y > contentRegionMin.Y &&
                mouseCursor.X < contentRegionMax.X &&
                mouseCursor.Y < contentRegionMax.Y)
            {
                _itemHovered = true;
            }
        #endregion

        ImDrawListPtr drawList = ImGui.GetWindowDrawList();

        float imageAspectRatio = _targetResolution.X / _targetResolution.Y;
        float contentRegionAspectRatio = viewportSize.X / viewportSize.Y;
        
        //GL.Viewport(0, 0, (int) ImGui.GetWindowWidth(), (int)  ImGui.GetWindowHeight());

        if (contentRegionAspectRatio > imageAspectRatio)
        {
            float imageWidth = viewportSize.Y * imageAspectRatio;
            float xPadding = (viewportSize.X - imageWidth) / 2;
            ImGui.SetCursorPosX(ImGui.GetCursorPosX() + xPadding);
            ImGui.Image((IntPtr) _colorTexture,
                new Vector2(imageWidth, viewportSize.Y),
                new Vector2(0, 1),
                new Vector2(1, 0));
        }
        else
        {
            float imageHeight = viewportSize.X / imageAspectRatio;
            float yPadding = (viewportSize.Y - imageHeight) / 2;

            ImGui.SetCursorPosY(ImGui.GetCursorPosY() + yPadding);
            ImGui.Image((IntPtr) _colorTexture,
                new Vector2(viewportSize.X, imageHeight),
                new Vector2(0, 1),
                new Vector2(1, 0));
        }
        
        _itemHovered = ImGui.IsItemHovered();
        
        ImGui.End();
    }

    public static void BindFramebuffer()
    {
        GL.Ext.BindFramebuffer(FramebufferTarget.FramebufferExt, _frameBuffer);
    }

    public static void UnbindFramebuffer()
    {
        GL.Ext.BindFramebuffer(FramebufferTarget.FramebufferExt, 0);
    }

    public static void BindRenderbuffer()
    {
        GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, _renderBuffer);
    }
    
    public static void UnbindRenderbuffer()
    {
        GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
    }
    
    public static void ResizeFramebuffer(Vector2 size)
    {
        GL.BindTexture(TextureTarget.Texture2D, _colorTexture);
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, (int)size.X, (int)size.Y, 0, PixelFormat.Rgb, PixelType.UnsignedByte, 0);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMagFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, _colorTexture, 0);
        
        BindRenderbuffer();
        GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.Depth24Stencil8, (int)size.X, (int)size.Y);
        GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthStencilAttachment, RenderbufferTarget.Renderbuffer, _renderBuffer);
        UnbindRenderbuffer();
        
        _sceneViewCamera.Resize( size.X / (float)size.Y);
        _targetResolution = size;
    }

    public static bool IsMouseOver()
    {
        return _itemHovered;
    }

    public static Camera GetSceneViewCamera()
    {
        return _sceneViewCamera;
    }
}