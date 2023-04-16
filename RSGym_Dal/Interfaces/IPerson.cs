using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSGym_Dal.Interfaces
{
    public interface IPerson
    {
        //TODO Devido a tipologia do programa optei por não separar em Primeiro e Ultimo Nome
        string FullName { get; set; }
        //TODO Devido a tipologia do programa optei gravar o nif como string
        string Nif { get; set; }
        int PostalCodeID { get; set; }
        string Address { get; set; }
        string Phone { get; set; }
        string Email { get; set; }
    }

}
