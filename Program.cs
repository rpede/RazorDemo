using RazorLight;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var engine = new RazorLightEngineBuilder()
    .UseEmbeddedResourcesProject(typeof(Program))
    .UseMemoryCachingProvider()
    .UseOptions(new RazorLightOptions() { EnableDebugMode = true })
    .Build();

var templates = new string[] { "ImplicitExpression", "ExplicitExpression", "Block", "Loop" };
var model = new Model { Field = "Value", Array = new[] { 1, 2, 3 } };

Func<string, Object, Task<HtmlResult>> Html = async (template, model) =>
    new HtmlResult(await engine.CompileRenderAsync($"RazorDemo.Emails.{template}", model));

app.MapGet("/", async () => await Html("Index", new Tuple<string[], Model>(templates, model)));
app.MapGet("/{template}", async (string template) => await Html(template, model));

app.Run();
