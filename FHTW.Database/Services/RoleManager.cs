﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FHTW.Database.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace FHTW.Database.Services;

public class RoleManager
{
    private readonly ApplicationContext _context;

    public RoleManager(ApplicationContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<bool> AddRoleAsync(ulong roleId, ulong guildId, string roleName)
    {
        if (await _context.RequestableRoles.AnyAsync(r => r.RoleId == roleId))
            return false;

        _context.RequestableRoles.Add(new Models.DiscordServerRole
        {
            RoleId = roleId,
            GuildId = guildId,
            RoleName = roleName
        });

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveRoleAsync(ulong roleId)
    {
        var role = await _context.RequestableRoles.FirstOrDefaultAsync(r => r.RoleId == roleId);

        if (role == null)
            return false;

        _context.RequestableRoles.Remove(role);

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task UpdateRoleNameAsync(ulong roleId, string newName)
    {
        var role = await _context.RequestableRoles.FirstOrDefaultAsync(r => r.RoleId == roleId);

        if (role == null)
            return;

        role.RoleName = newName;

        await _context.SaveChangesAsync();
    }

    public async Task UpdateRoleNamesAsync(IEnumerable<(ulong roleId, string newName)> roles)
    {
        var rolesToUpdate = await _context.RequestableRoles
            .Where(rr => roles.Any(r => r.roleId == rr.RoleId))
            .ToArrayAsync();

        if (rolesToUpdate.Length == 0)
            return;

        for (int i = 0; i < rolesToUpdate.Length; i++)
        {
            rolesToUpdate[i].RoleName = roles.First(r => r.roleId == rolesToUpdate[i].RoleId).newName;
        }

        await _context.SaveChangesAsync();
    }

    public Task<bool> IsRoleRequestableAsync(ulong roleId) =>
        _context.RequestableRoles
            .AsNoTracking()
            .AnyAsync(r => r.RoleId == roleId);

    public Task<ulong[]> GetRequestableRoleIdsAsync(ulong guildId) =>
        _context.RequestableRoles
            .AsNoTracking()
            .Select(r => r.GuildId)
            .Where(r => r == guildId)
            .ToArrayAsync();
}