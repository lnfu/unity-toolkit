using UnityEngine;
using System.Collections;

// 繼承自 Reaction
namespace lnfu
{
	public abstract class DelayedReaction : Reaction
	{
	    public float delay;             // 當 React() 被呼叫時延遲幾秒後才 React
	
	    protected WaitForSeconds wait;
	
	
	    // 會把 Reaction 的 Init() 隱藏，改用現在這個 Init()
	    public new void Init()
	    {
	        wait = new WaitForSeconds(delay);
	        SpecificInit();
	    }
	
	
	    // 會把 Reaction 的 React() 隱藏，改用現在這個 React()
	    public new void React(MonoBehaviour monoBehaviour)
	    {
	        monoBehaviour.StartCoroutine(ReactCoroutine());
	    }
	
	
	    protected IEnumerator ReactCoroutine()
	    {
	        yield return wait;
	
	        ImmediateReaction();
	    }
	}
}
