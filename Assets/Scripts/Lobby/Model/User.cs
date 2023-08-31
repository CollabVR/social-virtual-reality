using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User
{
    public int sub;
    public string email;
    public string fullName;
    public List<Roles> roles;
}