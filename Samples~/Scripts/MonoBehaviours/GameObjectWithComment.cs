using UnityEngine;

namespace lnfu
{
	[AddComponentMenu("Comment")]
    public class GameObjectWithComment : MonoBehaviour
    {
	    [TextArea(10, 1000)]
	    public string comment = "Write your comments down...";
    }
}
