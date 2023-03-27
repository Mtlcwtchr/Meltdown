public class PlayerDatabaseFactory
{
    private static PlayerDatabaseFactory _instance;
    public static PlayerDatabaseFactory Instance => _instance ??= new PlayerDatabaseFactory(new PlayerPrefsPlayerDatabase());

    private readonly IPlayerDatabase _playerDatabase;
    
    private PlayerDatabaseFactory(IPlayerDatabase playerDatabase)
    {
        _playerDatabase = playerDatabase;
    }

    public IPlayerDatabase Create() => _playerDatabase;
}