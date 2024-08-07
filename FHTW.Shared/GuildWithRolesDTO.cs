﻿using System.Collections.Generic;

namespace FHTW.Shared;

public class GuildWithRolesDTO
{
    public string GuildName { get; set; }
    public ulong GuildId { get; set; }
    public IEnumerable<RoleDTO> Roles { get; set; }
}