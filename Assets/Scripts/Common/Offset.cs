/// <summary>
/// 로직상에서 사용하는 X, Y 좌표 구조체
/// </summary>
public struct Offset
{
    public int x;
    public int y;

    public Offset(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

	public void Set(int x, int y)
	{
		this.x = x;
		this.y = y;
	}
}
