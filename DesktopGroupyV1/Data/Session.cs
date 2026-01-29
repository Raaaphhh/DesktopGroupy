using DesktopGroupyV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopGroupyV1.Data
{
    static class Session
    {
        public static Vendeur? currentVendeurConnected { get; set; }
    }
}
