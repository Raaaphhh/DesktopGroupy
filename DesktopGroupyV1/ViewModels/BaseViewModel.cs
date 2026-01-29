using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopGroupyV1.Data;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace DesktopGroupyV1.ViewModels
{
    internal class BaseViewModel
    {
        private readonly GroupyContext _db;
        public BaseViewModel() 
        {
            _db = new GroupyContext();
        }

        public bool IsConnected(string email, string pwd)
        {
            bool connected = false;
            if (email == null && pwd == null)
            {
                connected = false;
            }
            return connected;
        }

        public bool testConnexion() 
        {
            bool reussi = false; 
            try
            {
                bool connectDb = _db.Database.CanConnect();
                reussi = connectDb;
                Console.WriteLine("Connection OK");
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Connection failed: " + ex.Message);
            }
            return reussi;
        }
    }
}
