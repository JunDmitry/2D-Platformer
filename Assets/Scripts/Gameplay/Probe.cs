[RequireComponent(typeof(Health))]
public class Probe : MonoBehaviour
{
    [SerializeField] private float _singleLine;
    [SerializeField]
    private float _multiLine;
    [
        Tooltip("weird")
    ]
    private float _blockForm;

    [Obsolete] public void OneLiner() { }
    public float Prop { get; set; }
    public string Name => "x";
    public int Foo() => 42;
    public float Speed => _speed;
    public float _speed = 5f;
    public readonly List<int> _items = new List<int>();

    public event System.Action Changed;
    public int this[int i] => _items[i];
	
	public event System.Action SomeEvent
	{
		add { }
		remove { }
	}
	
	public void GenericMethod<T>(T arg) 
	{
	}
	
	int MemberWithoutAccess;
}

public interface SomeGeneric<T>
{
	T Property { get; }
}