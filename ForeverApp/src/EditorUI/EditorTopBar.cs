using System.Numerics;
using ImGuiNET;

namespace ForeverApp.EditorUI;

public class EditorTopBar
{
    public static float snap = 1.0f;
    public static void Render()
    {
        ImGui.Begin("Editor Top Bar");
        
        ImGui.Text("Snap");
        ImGui.SameLine();
        
        ImGui.PushItemWidth(80);
        ImGui.InputFloat("##", ref snap);
        ImGui.PopItemWidth();
        
        ImGui.SameLine();
        if (ImGui.Button("New GameObject"))
        {
            
        }
        ImGui.SameLine();
        if (ImGui.Button("Save Scene"))
        {
            
        }
        ImGui.SameLine();
        if (ImGui.Button("Load Scene"))
        {
            
        }
        ImGui.End();
    }
}