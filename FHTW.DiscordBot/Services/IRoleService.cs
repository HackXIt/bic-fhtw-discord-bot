﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using FHTW.Shared;

namespace FHTW.DiscordBot.Services;

public interface IRoleService
{
    Task<Result> GiveRoleToUserAsync(IGuildUser user, IRole role);
    Task<Result> GiveRoleToUserAsync(ulong guildId, ulong userId, ulong roleId);
    Task<Result> TakeRoleFromUserAsync(IGuildUser user, IRole role);
    Task<Result> TakeRoleFromUserAsync(ulong guildId, ulong userId, ulong roleId);
    Task<Result> SetRoleRequestableAsync(IRole role);
    Task<Result> SetRoleRequestableAsync(ulong guildId, ulong roleId);
    Task<Result> UnsetRoleRequestableAsync(IRole role);
    Task<Result> UnsetRoleRequestableAsync(ulong roleId);
    Task<ulong[]> GetRequestableRoleIdsAsync(ulong guildId);
    Task<IEnumerable<RoleDTO>> GetRequestableRolesAsync(ulong guildId);
    Task<IEnumerable<RoleDTO>> GetDiscordRolesAsync(ulong guildId);
    Task<IEnumerable<RoleDTO>> GetUserDiscordRolesAsync(ulong guildId, ulong userId);

    public enum Result : byte
    {
        Success,
        GuildNotFound,
        UserNotFound,
        RoleNotFound,
        RoleNotAllowed,
        AlreadyHasRole,
        DoesNotHaveRole,
        RoleAlreadyRequestable,
        RoleNotRequestable
    }

    Task<Result> AddRoleAsync(ulong newRoleId, ulong guildId, string newRoleName);
}