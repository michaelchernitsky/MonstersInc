using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Models
{ 
    public class RegisterViewModel : RegisterInputModel
    {
        public bool IsSuccess { get; set; }
    }
}