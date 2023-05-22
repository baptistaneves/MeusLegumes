using Microsoft.AspNetCore.Identity;

namespace MeusLegumes.Infrastructure.Repositories.Usuarios;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _siginManager;
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;

    public UsuarioRepository(UserManager<AppUser> userManager,
                             IMapper mapper,
                             ApplicationContext context,
                             SignInManager<AppUser> siginManager)
    {
        _userManager = userManager;
        _mapper = mapper;
        _context = context;
        _siginManager = siginManager;
    }

    public async Task<CriarUsuarioResponse> Adicionar(Usuario usuario, string password)
    {
        var appUser = _mapper.Map<AppUser>(usuario);
        var identityResult = await _userManager.CreateAsync(appUser, password);

        return new CriarUsuarioResponse(appUser.Id, identityResult.Succeeded, identityResult.Succeeded ? null : identityResult.Errors.Select(e => new ErrorResponse(e.Description)));
    }

    public async Task<CriarUsuarioResponse> Actualizar(Usuario usuario)
    {
        var appUser = _mapper.Map<AppUser>(usuario);
        var identityResult = await _userManager.UpdateAsync(appUser);

        return new CriarUsuarioResponse(appUser.Id, identityResult.Succeeded, identityResult.Succeeded ? null : identityResult.Errors.Select(e => new ErrorResponse(e.Description)));
    }

    public async Task<CriarUsuarioResponse> AdicionarAoPerfil(Guid id, string perlfi)
    {
        var appUser  = await _userManager.FindByIdAsync(id.ToString());

        var identityResult = await _userManager.AddToRoleAsync(appUser, perlfi);

        return new CriarUsuarioResponse(appUser.Id, identityResult.Succeeded, identityResult.Succeeded ? null : identityResult.Errors.Select(e => new ErrorResponse(e.Description)));
    }

    public async Task<CriarUsuarioResponse> RemoverUsuarioDoPerfil(Guid id, string perlfi)
    {
        var appUser = await _userManager.FindByIdAsync(id.ToString());

        var identityResult = await _userManager.RemoveFromRoleAsync(appUser, perlfi);

        return new CriarUsuarioResponse(appUser.Id, identityResult.Succeeded, identityResult.Succeeded ? null : identityResult.Errors.Select(e => new ErrorResponse(e.Description)));
    }

    public async Task<CriarUsuarioResponse> Remover(Guid id)
    {
        var appUser = await _userManager.FindByIdAsync(id.ToString());
        var identityResult = await _userManager.DeleteAsync(appUser);

        return new CriarUsuarioResponse(appUser.Id, identityResult.Succeeded, identityResult.Succeeded ? null : identityResult.Errors.Select(e => new ErrorResponse(e.Description)));
    }

    public async Task<IEnumerable<UsuarioDto>> ObterTodosUsuarios()
    {
        return await _context.Users.AsNoTracking()
            .Include(ur => ur.UserRoles)
            .ThenInclude(r => r.Role)
            .Where(u => u.UserRoles.Select(ur => ur.Role.Name).FirstOrDefault() != "Cliente")
            .Select(usuario => new UsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.UserName,
                Email = usuario.Email,
                Perfil = usuario.UserRoles.Select(ur => ur.Role.Name).FirstOrDefault()
            }).ToListAsync();
    }

    public async Task<Usuario> ObterUsuarioPorEmail(string email)
    {
        return _mapper.Map<Usuario>(await _userManager.FindByEmailAsync(email));

    }

    public async Task<UsuarioDto> ObterUsuarioPorId(Guid id)
    {
        return await _context.Users.AsNoTracking()
            .Include(ur => ur.UserRoles)
            .ThenInclude(r => r.Role)
            .Select(usuario => new UsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.UserName,
                Email = usuario.Email,
                Perfil = usuario.UserRoles.Select(ur => ur.Role.Name).FirstOrDefault()
            }).FirstOrDefaultAsync(u => u.Id == id);
    }


    public async Task<LoginResponse> CheckPasswordAsync(Guid id, string password)
    {
        var appUser = await _userManager.FindByIdAsync(id.ToString());

        var siginResult = await _siginManager.CheckPasswordSignInAsync(appUser, password, true);
        
        return new LoginResponse(siginResult.Succeeded, siginResult.IsLockedOut);
    }

    public async Task<CriarUsuarioResponse> AlterarSenha(Guid id, string senhaActual, string novaSenha)
    {
        var appUser = await _userManager.FindByIdAsync(id.ToString());

        var identityResult = await _userManager.ChangePasswordAsync(appUser, senhaActual, novaSenha);

        return new CriarUsuarioResponse(appUser.Id, identityResult.Succeeded, identityResult.Succeeded ? null : identityResult.Errors.Select(e => new ErrorResponse(e.Description)));

    }
}
