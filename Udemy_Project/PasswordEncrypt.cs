﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Security.Cryptography;
using System.Text;

namespace Udemy_Project
{
    public class PasswordEncrypt
    {
        public static string Encrypt(string Password)
        {
           //return Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(Password)));
            return Convert.ToBase64String(System.Security.Cryptography.MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(Password)));
        }
    }
}