﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityUserManagment.Shared.DTOs;
public class GetUserDto
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public List<string> UserRoles { get; set; }
}
