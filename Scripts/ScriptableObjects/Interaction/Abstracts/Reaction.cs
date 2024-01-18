using UnityEngine;

// 不同種 Reaction 會繼承這個 class
// 之後在 Editor 中的 ReactionCollections 裡面會有一連串的 Reaction
namespace lnfu
{
	public abstract class Reaction : ScriptableObject
	{
	    // ReactionCollection 會呼叫 Init()
	    public void Init()
	    {
	        SpecificInit();
	    }
	
	
	    // 繼承自 Reaction 的可以複寫 SpecificInit()
	    protected virtual void SpecificInit()
	    { }
	
	
	    // ReactionCollection 會呼叫 React()
	    public void React(MonoBehaviour monoBehaviour)
	    {
	        ImmediateReaction();
	    }
	
	
	    protected abstract void ImmediateReaction();
	}
}
