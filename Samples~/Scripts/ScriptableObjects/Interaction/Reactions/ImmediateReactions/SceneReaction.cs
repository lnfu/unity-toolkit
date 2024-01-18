// 因為在淡入淡出已經有 delay 了
// 所以這邊繼承 Reaction 就好 (immediate)
namespace lnfu
{
	public class SceneReaction : Reaction
	{
	    public string sceneName;                    // The name of the scene to be loaded.
	    public string startingPointInLoadedScene;   // The name of the StartingPosition in the newly loaded scene.
	    
		// TODO
		// public SaveData playerSaveData;             // Reference to the save data asset that will store the StartingPosition.
	
	    private SceneController sceneController;    // SceneController
	
	    protected override void SpecificInit()
	    {
	        sceneController = FindObjectOfType<SceneController>();
	    }
	
	
	    protected override void ImmediateReaction()
	    {
	        sceneController.FadeAndLoadScene(this);
	    }
	}
}
