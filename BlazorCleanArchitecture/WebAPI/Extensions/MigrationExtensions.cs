﻿using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Extensions;

/// <summary>
/// Extensions for handling database migrations.
/// </summary>
public static class MigrationExtensions
{
    /// <summary>
    /// Applies the migrations to the database.
    /// </summary>
    /// <param name="app">Used to configure an application's request pipeline.</param>
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();
    }
}