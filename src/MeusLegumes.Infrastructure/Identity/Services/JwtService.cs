namespace MeusLegumes.Infrastructure.Identity.Services;

public class JwtService : IJwtService
{
    private readonly JwtSettings _jwtSettings;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly byte[] _key;
    public JwtService(UserManager<AppUser> userManager, IOptions<JwtSettings> jwtSettings, IMapper mapper)
    {
        _jwtSettings = jwtSettings.Value;
        _key = Encoding.ASCII.GetBytes(_jwtSettings.SigningKey);
        _userManager = userManager;
        _mapper = mapper;
    }

    public JwtSecurityTokenHandler TokenHandler = new JwtSecurityTokenHandler();

    private string WritenToken(SecurityToken securityToken)
    {
        return TokenHandler.WriteToken(securityToken);
    }

    private SecurityToken CreateSecurityToken(ClaimsIdentity claimsIdentity)
    {
        var tokenDescriptor = GetSecurityTokenDescriptor(claimsIdentity);
        return TokenHandler.CreateToken(tokenDescriptor);
    }

    private SecurityTokenDescriptor GetSecurityTokenDescriptor(ClaimsIdentity claimsIdentity)
    {
        return new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Expires = DateTime.Now.AddHours(2),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key),
                                         SecurityAlgorithms.HmacSha256Signature)
        };
    }

    public async Task<string> GetJwtString(Usuario usuario)
    {
        var identityUser = _mapper.Map<AppUser>(usuario);

        var claimsIdentity = new ClaimsIdentity(new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, identityUser.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, identityUser.Email),
            new Claim("Nome", identityUser.UserName),
            new Claim("IdentityUserId", identityUser.Id.ToString()),
            new Claim("Email", identityUser.Email)
        });

        var userRoles = await _userManager.GetRolesAsync(identityUser);
        foreach (var role in userRoles)
        {
            claimsIdentity.AddClaim(new Claim("role", role));
        }

        var token = CreateSecurityToken(claimsIdentity);
        return WritenToken(token);
    }
}
