using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeverEngine.Graphics;

public class Shader
{
    public int shaderProgramID;

    public Shader(string vertexShaderPath, string fragmentShaderPath)
    {
        shaderProgramID = GL.CreateProgram();

        if(string.IsNullOrEmpty(vertexShaderPath) 
        || string.IsNullOrEmpty(fragmentShaderPath)) {
            throw new ArgumentException("Shader path cannot be null");
        }
        string vertexShaderSource = LoadShaderFile(vertexShaderPath, ShaderType.VertexShader);
        string fragmentShaderSource = LoadShaderFile(fragmentShaderPath, ShaderType.FragmentShader);

        int vertexShader = CompileShader(vertexShaderSource, ShaderType.VertexShader);
        int fragmentShader = CompileShader(fragmentShaderSource, ShaderType.FragmentShader);

        GL.AttachShader(shaderProgramID, vertexShader);
        GL.AttachShader(shaderProgramID, fragmentShader);

        GL.LinkProgram(shaderProgramID);

        GL.GetProgram(shaderProgramID, GetProgramParameterName.LinkStatus, out int success);
        if (success == 0)
        {
            string infoLog = GL.GetProgramInfoLog(shaderProgramID);
            Console.Error.WriteLine(infoLog);
        }

        // cleanup

        GL.DetachShader(shaderProgramID, vertexShader);
        GL.DetachShader(shaderProgramID, fragmentShader);
        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);
    }

    private string LoadShaderFile(String path, ShaderType shaderType)
    {
        // logging

        switch(shaderType)
        {
            case ShaderType.VertexShader:
                if(!path.Contains("vert"))
                {
                    Console.WriteLine("Warning: Supplied vertex shader may not be a vertex shader. Expect compiler errors");
                }
                break;
            case ShaderType.FragmentShader:
                if (!path.Contains("frag"))
                {
                    Console.WriteLine("Warning: Supplied fragment shader may not be a fragment shader. Expect compiler errors");
                }
                break;
            default:
                break;
        }

        // load shader file
        using StreamReader reader = new(path);

        string output = reader.ReadToEnd(); 

        return output;
    }
    
    private int CompileShader(string shaderSource, ShaderType shaderType) {
        int shader;

        shader = GL.CreateShader(shaderType);
        GL.ShaderSource(shader, shaderSource);  
        GL.CompileShader(shader);

        GL.GetShader(shader, ShaderParameter.CompileStatus, out int success);

        if(success == 0) {
            string infoLog = GL.GetShaderInfoLog(shader);
            
            return -1;
        }

        return shader;
    }

    public void Use() {
        GL.UseProgram(shaderProgramID);
    }

    public void SetMatrix4(string uniformName, Matrix4 uniformMatrix) {
        GL.UniformMatrix4(GL.GetUniformLocation(this.shaderProgramID, uniformName), true, ref uniformMatrix);
    }

    public void SetFloat(string uniformName, float uniformFloat) {
        GL.Uniform1(GL.GetUniformLocation(this.shaderProgramID, uniformName), uniformFloat);
    }
    
    public void SetInt(string uniformName, int uniformInt) {
        GL.Uniform1(GL.GetUniformLocation(this.shaderProgramID, uniformName), uniformInt);
    }

    public void SetVec3(string uniformName, Vector3 uniformVec3) {
        GL.Uniform3(GL.GetUniformLocation(this.shaderProgramID, uniformName), uniformVec3.X, uniformVec3.Y, uniformVec3.Z);
    }

    public void SetVec3(string uniformName, float x, float y, float z) {
        GL.Uniform3(GL.GetUniformLocation(this.shaderProgramID, uniformName), x, y, z);
    }

    public void SetColor(string uniformName, Color4 color) {
        GL.Uniform3(GL.GetUniformLocation(this.shaderProgramID, uniformName), color.R, color.G, color.B);
    }
}
