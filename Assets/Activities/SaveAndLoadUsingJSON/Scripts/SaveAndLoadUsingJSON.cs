using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class SaveAndLoadUsingJSON : MonoBehaviour
{

    #region Input Event Reader
    [Header("Input Reader")]
    [SerializeField] private InputReader _inputReader;
    #endregion

    private Transform _player;
    private Dictionary<string, (Vector3 position, Quaternion rotation)> _enemies;
    private int _enemyCount = 0;
    private string _fileDirectory;

    [SerializeField] private SpawnerManager _spawnerManager;
    [SerializeField] private UIManager _uiManager;

    private void Start()
    {
        #region Input Event Handler
        _inputReader.SaveEvent += HandleSave;
        _inputReader.LoadEvent += HandleLoad;
        #endregion

        _fileDirectory = Application.persistentDataPath + "/SaveFile.txt";
        _enemies = new Dictionary<string, (Vector3 position, Quaternion rotation)>();
    }

    private void Update()
    {
        _enemyCount = _enemies.Count;
        _uiManager.SetEnemyCount(_enemyCount);
    }

    public void SetPlayerTransform(Transform player)
    {
        _player = player;
    }

    public void SetEnemyTransform(Transform enemy)
    {
        if (!_enemies.ContainsKey(enemy.name))
        {
            _enemies.Add(enemy.name, (position: enemy.position, rotation: enemy.rotation));
        }
        else
        {
            _enemies[enemy.name] = (position: enemy.position, rotation: enemy.rotation);
        }
    }

    public void RemoveEnemyTransform(Transform enemy)
    {
        if (_enemies.ContainsKey(enemy.name))
        {
            _enemies.Remove(enemy.name);
        }
    }

    #region Input Event Handler
    private void HandleSave()
    {

        if (_player != null)
        {

            PlayerModel playerModel = new PlayerModel(
                    _player.name,
                    _player.position,
                    _player.rotation);

            EnemyModel enemyModel = new EnemyModel();
            foreach (string name in _enemies.Keys)
            {
                if (enemyModel.names.Count == enemyModel.positions.Count && enemyModel.positions.Count == enemyModel.rotations.Count &&
                    !enemyModel.names.Contains(name))
                {
                    enemyModel.SetEnemyModel(name, _enemies[name].position, _enemies[name].rotation, _enemyCount);
                }
            }

            string savedPlayerData = JsonUtility.ToJson(playerModel);
            string savedEnemyData = JsonUtility.ToJson(enemyModel);
            File.WriteAllText(_fileDirectory, $"{savedPlayerData}/{savedEnemyData}");

        }

    }

    private void HandleLoad()
    {
        if (File.Exists(_fileDirectory) && _player != null)
        {
            string readData = File.ReadAllText(_fileDirectory);
            string playerData = readData.Split("/")[0];
            string enemyData = readData.Split("/")[1];
            PlayerModel playerModel = JsonUtility.FromJson<PlayerModel>(playerData);
            EnemyModel enemyModel = JsonUtility.FromJson<EnemyModel>(enemyData);

            _spawnerManager.LoadPlayer(playerModel.position, playerModel.rotation);

            List<string> enemyNames = _enemies.Keys.ToList();
            List<string> enemyModelNames = enemyModel.names.ToList();
            List<string> difference = enemyNames.Except(enemyModelNames).ToList();

            foreach (string name in difference)
            {
                _spawnerManager.DestroyEnemies(name);
                _enemies.Remove(name);
            }

            for (int i = 0; i < enemyModel.names.Count; i++)
            {
                if (_enemies.ContainsKey(enemyModel.names[i]))
                {
                    _spawnerManager.LoadEnemies(enemyModel.names[i], enemyModel.positions[i], enemyModel.rotations[i]);
                }
                else
                {
                    _spawnerManager.RespawnEnemies(enemyModel.names[i], enemyModel.positions[i], enemyModel.rotations[i]);
                }
            }
        }
    }
    #endregion

    public class PlayerModel
    {

        public string name;
        public Vector3 position;
        public Quaternion rotation;

        public PlayerModel(string name, Vector3 position, Quaternion rotation)
        {
            this.name = name;
            this.position = position;
            this.rotation = rotation;
        }
    }

    public class EnemyModel
    {

        public List<string> names;
        public List<Vector3> positions;
        public List<Quaternion> rotations;
        public int enemyCount;

        public EnemyModel()
        {
            names = new List<string>();
            positions = new List<Vector3>();
            rotations = new List<Quaternion>();
        }

        public void SetEnemyModel(string name, Vector3 position, Quaternion rotation, int enemyCount)
        {
            this.names.Add(name);
            this.positions.Add(position);
            this.rotations.Add(rotation);
            this.enemyCount = enemyCount;
        }

    }

}