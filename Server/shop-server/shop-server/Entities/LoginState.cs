﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shop_server.Entities
{
    public enum LoginState
    {
        LoginSuccess ,
        AccountNotFound , 
        PasswordError
    }
}
