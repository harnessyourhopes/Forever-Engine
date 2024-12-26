#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;
layout (location = 2) in vec2 aTexCoords;

struct GameObject {
    vec3 scale;
};

uniform GameObject gameObject;

uniform mat4 viewMatrix;
uniform mat4 projectionMatrix;
uniform mat4 modelMatrix;
// temporary?
uniform int textureDivide;

out vec3 fragPosition;
out vec3 normal;
out vec2 textureCoordinates;

void main()
{
    // out variables
    fragPosition = vec3(vec4(aPos, 1.0) * modelMatrix);

    normal = aNormal;

    gl_Position = vec4(fragPosition, 1.0) * viewMatrix * projectionMatrix;
    
    if(abs(aNormal.z) > 0.9) {
        textureCoordinates = (aPos.xy * gameObject.scale.xy) / textureDivide;
    } else if (abs(aNormal.y) > 0.9) {
        textureCoordinates = (aPos.xz * gameObject.scale.xz) / textureDivide;
    } else {
        textureCoordinates = (aPos.yz * gameObject.scale.yz) / textureDivide;
    }
}