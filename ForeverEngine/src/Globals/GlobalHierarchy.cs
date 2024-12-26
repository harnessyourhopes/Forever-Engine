namespace ForeverEngine.Globals;

public class GlobalHierarchy
{
    private List<GameObject> _workspace;
    private GameObject? SelectedObject;
    private GlobalHierarchy()
    {
        _workspace = [];
    }

    public static void CreateInstance()
    {
        if (_instance == null)
            _instance = new GlobalHierarchy();
    }

    private static GlobalHierarchy? _instance;
    
    public static void AddToWorkspace(GameObject obj)
    {
        _instance?._workspace.Add(obj);
    }

    public static GameObject? GetSelectedObject()
    {
        return _instance?.SelectedObject;
    }

    public static void SetSelectedObject(GameObject gameObject)
    {
        if (_instance == null)
            return;
        
        _instance.SelectedObject = gameObject;
    }

    public static List<GameObject>? Workspace()
    {
        return _instance?._workspace;
    }

    public static void Render()
    {
        if (_instance == null)
            return;
        
        foreach (var gameObject in _instance._workspace) {
            
            gameObject.Render();
        }
    }
}