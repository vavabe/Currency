using CurrencyServices.DbMigrator.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyServices.DbMigrator.Controllers;

[ApiController]
[Route("/api/migration")]
public class MigrationController : ControllerBase
{
    private readonly IMigratorService _migratorService;

    public MigrationController(IMigratorService migratorService)
    {
        _migratorService = migratorService;
    }

    [HttpPost("apply")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> ApplyMigrations(long? versionTo)
    {
        var appliedMigration = await _migratorService.ApplyMigrations(versionTo);
        return Ok(appliedMigration);
    }

    [HttpPost("rollback")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> RollbackMigrations(long versionTo)
    {
        var rollbackedMigrations = await _migratorService.RollbackMigrations(versionTo);
        return Ok(rollbackedMigrations);
    }

    [HttpGet("history")]
    public async Task<IActionResult> GetAppliedMigrations()
    {
        return Ok(await _migratorService.GetMigrationsHistory());
    }

    [HttpGet("pending")]
    public async Task<IActionResult> GetMigrationsToApply()
    {
        return Ok(await _migratorService.GetMigrationToApply());
    }
}
