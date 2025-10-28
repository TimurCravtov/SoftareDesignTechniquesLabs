namespace Laboratory.GameEntities.Ammo;

public class BulletPoolManager: IBulletPoolManager
{
    private int POOL_SIZE = 50;
    private readonly Queue<Bullet> _bulletPool = new Queue<Bullet>();

    public BulletPoolManager()
    {
        foreach (int i in Enumerable.Range(0, POOL_SIZE))
        {
            Bullet bullet = new Bullet();
            bullet.Deactivate();
            // ensure pool callback returns bullet back here
            bullet.ReturnToPool = ReturnBullet;
            _bulletPool.Enqueue(bullet);
        }        
    }
    
    public Bullet GetBullet()
    {
        var b = _bulletPool.Count > 0 ? _bulletPool.Dequeue() : new Bullet();
        // wire return callback for newly created bullets
        b.ReturnToPool = ReturnBullet;
        return b;
    }
    
    public void ReturnBullet(Bullet bullet)
    {
        if (_bulletPool.Count < POOL_SIZE) 
        {
            // reset state
            bullet.Position = new System.Drawing.Point(-1, -1);
            _bulletPool.Enqueue(bullet);
        }
    }
}