using System;
using System.IO;

namespace Kribo.Class
{
    class dBFunctions
    {
        public static string ConnectionStringSQLite
        {
            get
            {
                string database = @"C:\Users\user\Desktop\C#\SQLite\SQLite\Database\Contact.s3db";
                string connectionString = @"Data Source=" + database;
                return connectionString;
            }
        }
    }
}
