using System;

[Serializable]
public class Range{
	public float Max;
	public float Current;

	// Phuong thuc khoi tao
	public Range(float max, float current){
		this.Max = max;
		this.Current = current;
	}
}

[Serializable]
public class Offset
{
    public float Current;
    public float Next;

    // Phuong thuc khoi tao
    public Offset(float current, float next)
    {
        this.Next = next;
        this.Current = current;
    }
}

[Serializable]
public class Level
{
    public int Max;
    public int Current;

    // Phuong thuc khoi tao
    public Level( int current,int max)
    {
        this.Max = max;
        this.Current = current;
    }
}
