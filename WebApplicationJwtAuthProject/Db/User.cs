﻿using System;
using System.Collections.Generic;

namespace WebApplicationJwtAuthProject.Db;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}
