using BugStore.Data;
using BugStore.Handlers.Customers.Commands;
using BugStore.Handlers.Customers.Queries;
using BugStore.Handlers.Orders.Commands;
using BugStore.Handlers.Orders.Queries;
using BugStore.Handlers.Products.Commands;
using BugStore.Handlers.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

builder.Services.AddMediatR(typeof(CreateProductHandler).Assembly);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");


app.MapGet("/v1/customers", async ([FromServices] IMediator mediator) =>
{
    var result = await mediator.Send(new GetCustomersQuerie());
    return Results.Ok(result.Value);
});

app.MapGet("/v1/customers/{id}", async ([FromServices] IMediator mediator, Guid id) =>
{
    var result = await mediator.Send(new GetCustomerByIdQuerie { Id = id });

    if (!result.IsSuccess)
        return Results.NotFound(result.ErrorMessage);

    return Results.Ok(result.Value);
});

app.MapPut("/v1/customers/{id}", async ([FromServices] IMediator mediator, Guid id, [FromBody] UpdateCustomerCommand command) =>
{
    command.Id = id;
    var result = await mediator.Send(command);

    if (!result.IsSuccess)
        return Results.NotFound(result.ErrorMessage);

    return Results.NoContent();
});

app.MapDelete("/v1/customers/{id}", async ([FromServices] IMediator mediator, Guid id) =>
{
    var result = await mediator.Send(new DeleteCustomerCommand { Id = id });

    if (!result.IsSuccess)
        return Results.NotFound(result.ErrorMessage);

    return Results.NoContent();
});

app.MapPost("/v1/customers", async ([FromBody] CreateCustomerCommand command, [FromServices] IMediator mediator) =>
{
    var result = await mediator.Send(command);

    if (!result.IsSuccess)
        return Results.BadRequest(result.ErrorMessage);

    return Results.Created($"/v1/customers/{result.Value!.Id}", result.Value);
});


app.MapPost("/v1/products", async ([FromBody] CreateProductCommand command, [FromServices] IMediator mediator) =>
{
    var result = await mediator.Send(command);

    if (!result.IsSuccess)
        return Results.BadRequest(result.ErrorMessage);

    return Results.Created($"/v1/products/{result.Value!.Id}", result.Value);
});

app.MapGet("/v1/products", async ([FromServices] IMediator mediator) =>
{
    var result = await mediator.Send(new GetProductsQuerie());
    return Results.Ok(result.Value);
});

app.MapGet("/v1/products/{id}", async ([FromServices] IMediator mediator, Guid id) =>
{
    var result = await mediator.Send(new GetProductByIdQuerie { Id = id });

    if (!result.IsSuccess)
        return Results.NotFound(result.ErrorMessage);

    return Results.Ok(result.Value);
});

app.MapPut("/v1/products/{id}", async ([FromServices] IMediator mediator, Guid id, [FromBody] UpdateProductCommand command) =>
{
    command.Id = id;
    var result = await mediator.Send(command);

    if (!result.IsSuccess)
        return Results.NotFound(result.ErrorMessage);

    return Results.NoContent();
});

app.MapDelete("/v1/products/{id}", async ([FromServices] IMediator mediator, Guid id) =>
{
    var result = await mediator.Send(new DeleteProductCommand { Id = id });

    if (!result.IsSuccess)
        return Results.NotFound(result.ErrorMessage);

    return Results.NoContent();
});



app.MapGet("/v1/orders/{id}", async ([FromServices] IMediator mediator, Guid id) =>
{
    var result = await mediator.Send(new GetOrderByIdQuery { Id = id });

    if (!result.IsSuccess)
        return Results.NotFound(result.ErrorMessage);

    return Results.Ok(result);
});

app.MapPost("/v1/orders", async ([FromBody] CreateOrderCommand command, [FromServices] IMediator mediator) =>
{
    var result = await mediator.Send(command);

    if (!result.IsSuccess)
        return Results.BadRequest(result.ErrorMessage);

    return Results.Created($"/v1/orders/{result.Value!.Id}", result.Value);
});

app.Run();
