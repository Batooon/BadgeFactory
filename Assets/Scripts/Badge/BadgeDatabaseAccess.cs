using System;

namespace Badge
{
    public class BadgeDatabaseAccess : IBadgeDatabase
    {
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

        public void SaveBadgeData()
        {
            throw new NotImplementedException();
        }

        private BadgeDatabaseAccess()
        {

        }
    }
}
