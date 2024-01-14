using UnityEditor;
using UnityEngine;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
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
		    CreateDirectories("_Project", "Scripts", "Art", "Scenes");
		    Refresh();
	    }

	    [MenuItem("Tools/Setup/Load New Manifest")]
	    public static async void LoadNewManifest()
	    {
		    var url = GetGistUrl("5340644135ddc9d074bbe544d03f33f0");
		    var contents = await GetContents(url);
		    ReplacePackageFile(contents);
	    }

	    static void CreateDirectories(string root, params string[] dir)
	    {
		    var fullpath = Combine(dataPath, root);
		    foreach (var newDirectory in dir)
		    {
			    CreateDirectory(Combine(fullpath, newDirectory));
		    }
	    }

	    // https://gist.githubusercontent.com/lnfu/5340644135ddc9d074bbe544d03f33f0/raw
	    static string GetGistUrl(string id, string user = "lnfu")
	    {
		    return $"https://gist.githubusercontent.com/{user}/{id}/raw";
	    }

	    static async Task<string> GetContents(string url)
	    {
		    using var client = new HttpClient();
		    var response = await client.GetAsync(url);
		    var content = await response.Content.ReadAsStringAsync();
		    return content;
	    }

	    static void ReplacePackageFile(string contents)
	    {
		    var existing = Path.Combine(Application.dataPath, "../Packages/manifest.json");
		    File.WriteAllText(existing, contents);
		    UnityEditor.PackageManager.Client.Resolve();
	    }
    }
}
