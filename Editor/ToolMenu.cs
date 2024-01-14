using UnityEditor;
using UnityEngine;
using static System.IO.Directory;
using static System.IO.Path;
using static UnityEngine.Application;
using static UnityEditor.AssetDatabase;

namespace lnfu
{
    public static class ToolMenu
    {
	    [MenuItem("Tools/Setup/Create Default Directories")]
	    public static void CreateDefaultDirectories()
	    {
		    Debug.Log("test");
		    CreateDirectories("_Project", "Scripts", "Art", "Scenes");
		    Refresh();
	    }

	    public static void CreateDirectories(string root, params string[] dir)
	    {
		    var fullpath = Combine(dataPath, root);
		    foreach (var newDirectory in dir)
		    {
			    CreateDirectory(Combine(fullpath, newDirectory));
		    }
	    }
    }
}
