﻿using System.Collections.Generic;
using System.Threading.Tasks;
using FHTW.Shared;

namespace FHTW.WebClient.Services.Core;

public interface IRoleApi
{
    Task<IEnumerable<RoleDTO>> GetDiscordRolesAsync(ulong guildId);
    Task<IEnumerable<RoleDTO>> GetUserDiscordRolesAsync(ulong guildId, ulong userId);
    Task<IEnumerable<ulong>> GetRequestableRoleIdsAsync(ulong guildId);
    Task GiveRoleAsync(ulong guildId, ulong roleId);
    Task TakeRoleAsync(ulong guildId, ulong roleId);
    Task SetRequestableAsync(ulong guildId, ulong roleId);
    Task UnsetRequestableAsync(ulong guildId, ulong roleId);
    Task<IEnumerable<GuildWithRolesDTO>> GetUserGuildsWithRequestableRoles(ulong userID);
}