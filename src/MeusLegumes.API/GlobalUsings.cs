﻿global using MeusLegumes.API.Registrars.Interfaces;
global using Microsoft.AspNetCore.Diagnostics;
global using System.Net;
global using System.Text.Json;
global using Microsoft.AspNetCore.Mvc.Versioning;
global using Microsoft.AspNetCore.Mvc;
global using MeusLegumes.API.Middleware;
global using Microsoft.AspNetCore.Mvc.ApiExplorer;
global using MeusLegumes.API.Extensions;
global using Microsoft.Extensions.Options;
global using Microsoft.OpenApi.Models;
global using Swashbuckle.AspNetCore.SwaggerGen;
global using MeusLegumes.API.Options;
global using MeusLegumes.API.Errors;
global using Microsoft.AspNetCore.Mvc.Filters;
global using MeusLegumes.Infrastructure.Context;
global using Microsoft.EntityFrameworkCore;
global using MeusLegumes.Domain.Communication.Notifications;
global using MeusLegumes.API.Filters;
global using MeusLegumes.Application.Contexts.Categorias.Contracts;
global using MeusLegumes.Application.Contexts.Categorias.Services;
global using MeusLegumes.Application.Contexts.Unidades.Contracts;
global using MeusLegumes.Application.Contexts.Unidades.Services;
global using MeusLegumes.Domain.Contexts.Categorias.Repositories;
global using MeusLegumes.Domain.Contexts.Unidades.Repositories;
global using MeusLegumes.Infrastructure.Repositories.Categorias;
global using MeusLegumes.Infrastructure.Repositories.Unidades;
global using MeusLegumes.Application.AutoMapper;
global using MeusLegumes.Application.Contexts.Enderecos.Contracts;
global using MeusLegumes.Application.Contexts.Enderecos.Services;
global using MeusLegumes.Domain.Contexts.Enderecos.Repositories;
global using MeusLegumes.Infrastructure.Repositories.Enderecos;
global using MeusLegumes.Application.Contexts.Impostos.Contracts;
global using MeusLegumes.Application.Contexts.Impostos.Services;
global using MeusLegumes.Domain.Contexts.Impostos.Repositories;
global using MeusLegumes.Infrastructure.Repositories.Impostos;
global using MeusLegumes.Application.Contexts.Clientes.Contracts;
global using MeusLegumes.Application.Contexts.Clientes.Services;
global using MeusLegumes.Domain.Contexts.Clientes.Repositories;
global using MeusLegumes.Infrastructure.Repositories.Clientes;
global using MeusLegumes.Application.Contexts.Produtos.Contracts;
global using MeusLegumes.Application.Contexts.Produtos.Services;
global using MeusLegumes.Application.Contexts.Pacotes.Contracts;
global using MeusLegumes.Application.Contexts.Pacotes.Services;
global using MeusLegumes.Domain.Contexts.Pacotes.Repositories;
global using MeusLegumes.Domain.Contexts.Produtos.Repositories;
global using MeusLegumes.Infrastructure.Repositories.Pacotes;
global using MeusLegumes.Infrastructure.Repositories.Produtos;
global using MeusLegumes.API.Helpers.ImageUpload;
global using MeusLegumes.Application.Contexts.Pacotes;
global using MeusLegumes.Application.Contexts.Identity.Services;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using MeusLegumes.Application.Contexts.Identity.Options;
global using Microsoft.IdentityModel.Tokens;
global using System.Text;
global using Microsoft.AspNetCore.Identity;
global using MediatR;
global using MeusLegumes.Application.Contexts.Identity.Commands;
global using MeusLegumes.Application.Contexts.Identity.Models;








