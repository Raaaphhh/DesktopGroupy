using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopGroupyV1.Data;
using DesktopGroupyV1.Models;
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
                try
                {
                    bool connectDb = _db.Database.CanConnect();
                    if (connectDb)
                    {
                        var user = _db.Vendeurs.FirstOrDefault(u => u.Email == email && u.MotDePasse == pwd);
                        if (user != null)
                        {
                            Session.currentVendeurConnected = user;
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
                }
            }
            return connected;
        }
    }
}
