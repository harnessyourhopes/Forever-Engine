using System.Numerics;
using ForeverEngine;
using ForeverEngine.Components;
using ForeverEngine.Globals;
using ImGuiNET;
using OpenTK.Graphics.ES20;
using Quaternion = OpenTK.Mathematics.Quaternion;

namespace ForeverApp.EditorUI;

public class EditorProperties
{
    public static void Render()
    {
        ImGui.Begin("Property Window");
        switch (GlobalHierarchy.GetSelectedObject())
        {
            case GameObject gameObject:
                RenderGameObject(gameObject);
                break;
            default:
                ImGui.Text("Nothing selected.");
                break;
        }
        ImGui.End();
    }

    private static void RenderGameObject(GameObject gameObject)
    {
        ImGui.BeginGroup();
        
        ImGui.Text("Name");
        ImGui.SameLine();
        
        string name = gameObject.name;
        if(ImGui.InputText("##input", ref name, 32))
        {
            gameObject.name = name;
        }
        
        ImGui.EndGroup();

        if (gameObject.GetComponent<Transform>() != null)
        {
            var position = gameObject.GetComponent<Transform>().GetPosition();
            var virtualPosition = new Vector3(position.X, position.Y, position.Z);

            var scale = gameObject.GetComponent<Transform>().GetScale();
            var virtualScale = new Vector3(scale.X, scale.Y, scale.Z);

            var rotation = gameObject.GetComponent<Transform>().GetRotation().ToEulerAngles();
            var virtualRotation = new Vector3(rotation.X, rotation.Y, rotation.Z);

            
            ImGui.SeparatorText("Transform");
        
            ImGui.BeginGroup();
        
            ImGui.Text("Position");
            ImGui.SameLine();
            ImGui.DragFloat3("##position", ref virtualPosition, EditorTopBar.snap);
        
            ImGui.EndGroup();   
            
            ImGui.BeginGroup();
        
            ImGui.Text("Rotation");
            ImGui.SameLine();
            ImGui.DragFloat3("##rotation", ref virtualRotation, 15f);
        
            ImGui.EndGroup();   
            
            ImGui.BeginGroup();
        
            ImGui.Text("Scale");
            ImGui.SameLine();
            ImGui.DragFloat3("##scale", ref virtualScale, EditorTopBar.snap);
        
            ImGui.EndGroup();   
            
            if (virtualPosition.X != position.X 
                || virtualPosition.Y != position.Y 
                || virtualPosition.Z != position.Z)
            {
                OpenTK.Mathematics.Vector3 newPosition = new OpenTK.Mathematics.Vector3(virtualPosition.X, virtualPosition.Y, virtualPosition.Z);
                gameObject.GetComponent<Transform>().SetPosition(newPosition);
            }

            if (virtualRotation.X != rotation.X
                || virtualRotation.Y != rotation.Y
                || virtualRotation.Z != rotation.Z)
            {
                Quaternion newRotation =
                    new Quaternion(new OpenTK.Mathematics.Vector3(virtualRotation.X, 
                        virtualRotation.Y,
                        virtualRotation.Z));
                gameObject.GetComponent<Transform>().SetRotation(newRotation);
            }

            if (virtualScale.X != scale.X
                || virtualScale.Y != scale.Y
                || virtualScale.Z != scale.Z)
            {
                OpenTK.Mathematics.Vector3 newScale = new OpenTK.Mathematics.Vector3(virtualScale.X, virtualScale.Y, virtualScale.Z);
                gameObject.GetComponent<Transform>().SetScale(newScale);
            }
        }
    }
}