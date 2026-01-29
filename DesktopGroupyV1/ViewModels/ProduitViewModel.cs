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
    internal class ProduitViewModel
    {
        private readonly GroupyContext _db;
        public ProduitViewModel()
        {
            _db = new GroupyContext();
        }
    }
}
