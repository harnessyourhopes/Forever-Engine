using ImGuiNET;

namespace ForeverApp.EditorUI;

public class EditorMainMenu
{
    public static void Render()
    {
        if (ImGui.BeginMainMenuBar())
        {
            if (ImGui.BeginMenu("File"))
            {
                ImGui.SeparatorText("Project");
                if (ImGui.MenuItem("New Project"))
                {

                }

                if (ImGui.MenuItem("Save Project"))
                {

                }

                if (ImGui.MenuItem("Open Project"))
                {

                }

                ImGui.Separator();
                if (ImGui.MenuItem("Exit"))
                {

                }
            }

            if (ImGui.BeginMenu("Edit"))
            {

            }

            if (ImGui.BeginMenu("Physics"))
            {

            }

            if (ImGui.BeginMenu("Tools"))
            {
                
            }

            if (ImGui.BeginMenu("Settings"))
            {

            }

            if (ImGui.BeginMenu("Help"))
            {
 
            }

        }
        ImGui.EndMainMenuBar();
    }
}