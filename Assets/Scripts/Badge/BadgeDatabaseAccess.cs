using System.IO;
using UnityEngine;

namespace Badge
{
    public class BadgeDatabaseAccess : IBadgeDatabase
    {
        private const string _dataPath = "BadgeData.json";

        private static BadgeDatabaseAccess _singleton;
        private BadgeData _badgeData;

        public static BadgeDatabaseAccess GetBadgeDatabase()
        {
            if (_singleton == null)
                return _singleton = new BadgeDatabaseAccess();

            return _singleton;
        }

        public BadgeData GetBadgeData()
        {
            return _badgeData;
        }

        private BadgeDatabaseAccess()
        {
            _badgeData = FileOperations.Deserialize<BadgeData>(_dataPath);
        }

        public void Serialize()
        {
            FileOperations.Serialize(_badgeData,_dataPath);
        }
    }
}
