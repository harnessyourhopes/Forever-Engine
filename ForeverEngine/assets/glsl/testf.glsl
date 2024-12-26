#version 330 core
#define NR_POINT_LIGHTS 20

out vec4 FragColor;

in vec3 normal;
in vec2 textureCoordinates;
in vec3 fragPosition;

struct GameObject {
    vec3 scale;
};

struct Material {
    vec3 color;
    float specular;
    float shininess;
    // texture
    sampler2D diffuseMap;
    sampler2D specularMap;
    sampler2D occlusionMap;
};

struct DirectionalLight {
    vec3 direction;
    vec3 color;
    float brightness;
};

struct PointLight {
    vec3 position;
    vec3 color;
    float brightness;
};

struct Fog {
    vec3 color;
    float near;
    float far;
    float density;
};

uniform GameObject gameObject;
uniform DirectionalLight directionalLight;
uniform PointLight pointLights[NR_POINT_LIGHTS];
uniform Material material;
uniform Fog fog;

uniform vec3 viewPosition;

vec3 CalculateDirectionalLight(DirectionalLight light, vec3 normal, vec3 viewDirection) {
    vec3 diffuseTexture = vec3(texture(material.diffuseMap, textureCoordinates));
    vec3 specularTexture = vec3(texture(material.specularMap, textureCoordinates));
    vec3 occlusionTexture = vec3(texture(material.occlusionMap, textureCoordinates));
    
    vec3 lightDirection = normalize(-light.direction);
    vec3 reflectDirection = reflect(-lightDirection, normal);
    
    float preDiffuse = max(dot(normal, lightDirection), 0.0);
    float preSpecular = pow(max(dot(viewDirection, reflectDirection), 0.0), material.shininess);
    
    vec3 ambientLightPower = (light.color * light.brightness) / 4;
    
    vec3 ambient  = (ambientLightPower * material.color * diffuseTexture);
    vec3 diffuse  = light.color * preDiffuse * material.color * diffuseTexture;
    vec3 specular = light.color * preSpecular * specularTexture * material.specular;

    return (ambient + diffuse + specular);
}

vec3 CalculateFog(vec3 lighting, Fog fog, float depth) {
    float depthVector = exp(-pow(depth * fog.density, 2.0));
    
    return mix(fog.color, lighting, depthVector);
}

float LinearizeDepth(float depth) {
    float z = depth * 2.0 - 1.0; // back to NDC 
    return (2.0 * fog.near * fog.far) / (fog.far + fog.near - z * (fog.far - fog.near));
}

void main()
{
    //float depth = LinearizeDepth(gl_FragCoord.z) / fog.far;
    
    vec3 norm = normalize(normal);
    vec3 viewDirection = normalize(viewPosition - fragPosition);

    vec3 finalLighting = CalculateDirectionalLight(directionalLight, norm, viewDirection);
    vec3 finalFragment = finalLighting;

    FragColor = vec4(finalLighting, 1);
} 