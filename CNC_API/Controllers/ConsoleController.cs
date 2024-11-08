using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ConsoleController  : ControllerBase
{
    private readonly ConsoleService _consoleService;

    public ConsoleController()
    {
        _consoleService = new ConsoleService();
        _consoleService.OutputReceived += (output) =>
        {
            Console.WriteLine(output); // Puedes almacenar o procesar la salida aquí si lo necesitas
        };
    }

    [HttpGet("install-packages")]
    public IActionResult InstallPackages()
    {
        // Ruta de tu proyecto donde deseas ejecutar los comandos
        string projectPath = @"C:\Users\admin\Documents\adrian\Trabajo\CloverCube\integracion Whatsapp\api";

        // Ejecutar 'pnpm install' para instalar paquetes
        _consoleService.ExecuteCommand(projectPath, "pnpm", "i");

        return Ok("Instalación de paquetes iniciada.");
    }

    [HttpGet("start-api")]
    public IActionResult StartApi()
    {
        // Ruta de tu proyecto donde deseas ejecutar los comandos
        string projectPath = @"C:\Users\admin\Documents\adrian\Trabajo\CloverCube\integracion Whatsapp\api";

        // Ejecutar 'pnpm start' para iniciar la API
        _consoleService.ExecuteCommand(projectPath, "pnpm", "run dev");

        return Ok("API iniciada.");
    }
}
