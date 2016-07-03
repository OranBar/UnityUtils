using System.Collections;

public class AbortableEnumerator : IEnumerator
{
	protected IEnumerator enumerator;
	protected bool isAborted;

	public AbortableEnumerator(IEnumerator enumerator)
	{
		this.enumerator = enumerator;
	}

	public void Abort()
	{
		isAborted = true;
	}

	bool IEnumerator.MoveNext ()
	{
		if (isAborted)
			return false;
		else
			return enumerator.MoveNext ();
	}

	void IEnumerator.Reset ()
	{
		isAborted = false;
		enumerator.Reset ();
	}

	object IEnumerator.Current 
	{
		get { return enumerator.Current; }
	}
}