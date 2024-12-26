namespace ForeverEngine.Graphics.Geometry;

public static class Cube
{
    public static float[] vertices = {
        // Positions           // Normals           // Texture Coords
        // Front face (two triangles)
        -0.5f, -0.5f,  0.5f,   0.0f,  0.0f,  1.0f,   0.0f, 0.0f,  // Bottom-left
        0.5f, -0.5f,  0.5f,   0.0f,  0.0f,  1.0f,   1.0f, 0.0f,  // Bottom-right
        0.5f,  0.5f,  0.5f,   0.0f,  0.0f,  1.0f,   1.0f, 1.0f,  // Top-right
        0.5f,  0.5f,  0.5f,   0.0f,  0.0f,  1.0f,   1.0f, 1.0f,  // Top-right
        -0.5f,  0.5f,  0.5f,   0.0f,  0.0f,  1.0f,   0.0f, 1.0f,  // Top-left
        -0.5f, -0.5f,  0.5f,   0.0f,  0.0f,  1.0f,   0.0f, 0.0f,  // Bottom-left

        // Back face (two triangles)
        -0.5f, -0.5f, -0.5f,   0.0f,  0.0f, -1.0f,   0.0f, 0.0f,  // Bottom-left
        0.5f, -0.5f, -0.5f,   0.0f,  0.0f, -1.0f,   1.0f, 0.0f,  // Bottom-right
        0.5f,  0.5f, -0.5f,   0.0f,  0.0f, -1.0f,   1.0f, 1.0f,  // Top-right
        0.5f,  0.5f, -0.5f,   0.0f,  0.0f, -1.0f,   1.0f, 1.0f,  // Top-right
        -0.5f,  0.5f, -0.5f,   0.0f,  0.0f, -1.0f,   0.0f, 1.0f,  // Top-left
        -0.5f, -0.5f, -0.5f,   0.0f,  0.0f, -1.0f,   0.0f, 0.0f,  // Bottom-left

        // Left face (two triangles)
        -0.5f,  0.5f,  0.5f,  -1.0f,  0.0f,  0.0f,   1.0f, 1.0f,  // Top-right
        -0.5f,  0.5f, -0.5f,  -1.0f,  0.0f,  0.0f,   0.0f, 1.0f,  // Top-left
        -0.5f, -0.5f, -0.5f,  -1.0f,  0.0f,  0.0f,   0.0f, 0.0f,  // Bottom-left
        -0.5f, -0.5f, -0.5f,  -1.0f,  0.0f,  0.0f,   0.0f, 0.0f,  // Bottom-left
        -0.5f, -0.5f,  0.5f,  -1.0f,  0.0f,  0.0f,   1.0f, 0.0f,  // Bottom-right
        -0.5f,  0.5f,  0.5f,  -1.0f,  0.0f,  0.0f,   1.0f, 1.0f,  // Top-right

        // Right face (two triangles)
        0.5f,  0.5f,  0.5f,   1.0f,  0.0f,  0.0f,   1.0f, 1.0f,  // Top-left
        0.5f, -0.5f,  0.5f,   1.0f,  0.0f,  0.0f,   1.0f, 0.0f,  // Bottom-left
        0.5f, -0.5f, -0.5f,   1.0f,  0.0f,  0.0f,   0.0f, 0.0f,  // Bottom-right
        0.5f, -0.5f, -0.5f,   1.0f,  0.0f,  0.0f,   0.0f, 0.0f,  // Bottom-right
        0.5f,  0.5f, -0.5f,   1.0f,  0.0f,  0.0f,   0.0f, 1.0f,  // Top-right
        0.5f,  0.5f,  0.5f,   1.0f,  0.0f,  0.0f,   1.0f, 1.0f,  // Top-left

        // Top face (two triangles)
        -0.5f,  0.5f, -0.5f,   0.0f,  1.0f,  0.0f,   0.0f, 0.0f,  // Top-left
        0.5f,  0.5f, -0.5f,   0.0f,  1.0f,  0.0f,   1.0f, 0.0f,  // Top-right
        0.5f,  0.5f,  0.5f,   0.0f,  1.0f,  0.0f,   1.0f, 1.0f,  // Bottom-right
        0.5f,  0.5f,  0.5f,   0.0f,  1.0f,  0.0f,   1.0f, 1.0f,  // Bottom-right
        -0.5f,  0.5f,  0.5f,   0.0f,  1.0f,  0.0f,   0.0f, 1.0f,  // Bottom-left
        -0.5f,  0.5f, -0.5f,   0.0f,  1.0f,  0.0f,   0.0f, 0.0f,  // Top-left

        // Bottom face (two triangles)
        -0.5f, -0.5f, -0.5f,   0.0f, -1.0f,  0.0f,   0.0f, 0.0f,  // Top-right
        0.5f, -0.5f, -0.5f,   0.0f, -1.0f,  0.0f,   1.0f, 0.0f,  // Top-left
        0.5f, -0.5f,  0.5f,   0.0f, -1.0f,  0.0f,   1.0f, 1.0f,  // Bottom-left
        0.5f, -0.5f,  0.5f,   0.0f, -1.0f,  0.0f,   1.0f, 1.0f,  // Bottom-left
        -0.5f, -0.5f,  0.5f,   0.0f, -1.0f,  0.0f,   0.0f, 1.0f,  // Bottom-right
        -0.5f, -0.5f, -0.5f,   0.0f, -1.0f,  0.0f,   0.0f, 0.0f   // Top-right
    };
    
    public static float[] normals = {
        0.0f,  0.0f,  1.0f,
        0.0f,  0.0f,  1.0f,
        0.0f,  0.0f,  1.0f,
        0.0f,  0.0f,  1.0f,
        0.0f,  0.0f,  1.0f,
        0.0f,  0.0f,  1.0f,
        
        0.0f,  0.0f, -1.0f, 
        0.0f,  0.0f, -1.0f, 
        0.0f,  0.0f, -1.0f, 
        0.0f,  0.0f, -1.0f, 
        0.0f,  0.0f, -1.0f, 
        0.0f,  0.0f, -1.0f, 
        
        -1.0f,  0.0f,  0.0f, 
        -1.0f,  0.0f,  0.0f, 
        -1.0f,  0.0f,  0.0f, 
        -1.0f,  0.0f,  0.0f, 
        -1.0f,  0.0f,  0.0f, 
        -1.0f,  0.0f,  0.0f, 
        
        1.0f,  0.0f,  0.0f,
        1.0f,  0.0f,  0.0f,
        1.0f,  0.0f,  0.0f,
        1.0f,  0.0f,  0.0f,
        1.0f,  0.0f,  0.0f,
        1.0f,  0.0f,  0.0f,
        
        0.0f,  1.0f,  0.0f,
        0.0f,  1.0f,  0.0f,
        0.0f,  1.0f,  0.0f,
        0.0f,  1.0f,  0.0f,
        0.0f,  1.0f,  0.0f,
        0.0f,  1.0f,  0.0f,
        
        0.0f, -1.0f,  0.0f, 
        0.0f, -1.0f,  0.0f, 
        0.0f, -1.0f,  0.0f, 
        0.0f, -1.0f,  0.0f, 
        0.0f, -1.0f,  0.0f, 
        0.0f, -1.0f,  0.0f, 
    };

    public static float[] textureCoordinates = {
        0.0f, 0.0f,
        1.0f, 0.0f, 
        1.0f, 1.0f, 
        1.0f, 1.0f, 
        0.0f, 1.0f,
        0.0f, 0.0f,
        
        0.0f, 0.0f,
        1.0f, 0.0f, 
        1.0f, 1.0f, 
        1.0f, 1.0f, 
        0.0f, 1.0f,
        0.0f, 0.0f,
        
        1.0f, 1.0f,
        0.0f, 1.0f,
        0.0f, 0.0f,
        0.0f, 0.0f,
        1.0f, 0.0f,
        1.0f, 1.0f,
        
        1.0f, 1.0f, 
        1.0f, 0.0f, 
        0.0f, 0.0f, 
        0.0f, 0.0f, 
        0.0f, 1.0f, 
        1.0f, 1.0f, 
        
        0.0f, 0.0f,
        1.0f, 0.0f, 
        1.0f, 1.0f, 
        1.0f, 1.0f, 
        0.0f, 1.0f,
        0.0f, 0.0f,
        
        0.0f, 0.0f,
        1.0f, 0.0f, 
        1.0f, 1.0f, 
        1.0f, 1.0f, 
        0.0f, 1.0f,
        0.0f, 0.0f
    };
}