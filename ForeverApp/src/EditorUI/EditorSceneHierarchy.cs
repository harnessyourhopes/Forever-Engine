using ForeverEngine.Globals;
using ImGuiNET;

namespace ForeverApp.EditorUI;

public class EditorSceneHierarchy
{
    public static void Render()
    {
        ImGui.Begin("Hierarchy");
        ImGuiTreeNodeFlags flag = ImGuiTreeNodeFlags.DefaultOpen;
        // TODO: icon images based on type
        if (ImGui.TreeNodeEx("Workspace", flag))
        {
            foreach (var gameObject in GlobalHierarchy.Workspace()!)
            {
                var flags = ImGuiTreeNodeFlags.Leaf;
                
                if (ImGui.TreeNodeEx(gameObject.name, ImGuiTreeNodeFlags.Leaf))
                {
                    if (ImGui.IsItemClicked())
                    {
                        flags = flags | ImGuiTreeNodeFlags.Selected;
                        GlobalHierarchy.SetSelectedObject(gameObject);
                    }
                    
                    ImGui.TreePop();
                }
            }
            
            ImGui.TreePop();
        }
        
        ImGui.End();
    }
}