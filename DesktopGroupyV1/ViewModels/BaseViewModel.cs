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
            if (email == "" && pwd == "")
            {
                connected = false;
            }
            else
            {
                // appeler fonction de connxeion a la base. 
                // Si c'est bon on tente la connexion du user. 
                try
                {
                    bool connectDb = _db.Database.CanConnect();
                    if (connectDb)
                    {
                        var user = _db.Vendeurs.FirstOrDefault(u => u.Email == email && u.MotDePasse == pwd);
                        if (user != null)
                        {
                            connected = true;
                        }
                        else
                        {
                            connected = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    connected = false;
                    // Dans ce cas on renvoie un message d'erreur
                }
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
