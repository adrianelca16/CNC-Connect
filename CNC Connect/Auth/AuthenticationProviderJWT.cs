using CNC_Connect.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace CNC_Connect.Auth
{
    public class AuthenticationProviderJWT : AuthenticationStateProvider, ILoginService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly HttpClient _httpClient;
        private readonly string _tokenKey;
        private readonly AuthenticationState _anonymous;

        public AuthenticationProviderJWT(IJSRuntime jsRuntime, HttpClient httpClient)
        {
            _jsRuntime = jsRuntime;
            _httpClient = httpClient;
            _tokenKey = "TOKEN_KEY";
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _jsRuntime.GetLocalStorage(_tokenKey);
            if (token is null)
            {
                return _anonymous;
            }

            return BuildAuthenticationState(token!.ToString()!);
        }

        private AuthenticationState BuildAuthenticationState(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var claims = ParseClaimsFromJWT(token);
            var identity = new ClaimsIdentity(claims, "jwt");
            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        private IEnumerable<Claim> ParseClaimsFromJWT(string token)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var unserializedToken = jwtSecurityTokenHandler.ReadJwtToken(token);
            return unserializedToken.Claims;
        }

        // Método para obtener el ID del usuario autenticado
        public async Task<string?> GetUserIdAsync()
        {
            var authState = await GetAuthenticationStateAsync();
            var user = authState.User;

            // Obtener el claim del ID del usuario (sujeto)
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public async Task LoginAsync(string token)
        {
            await _jsRuntime.SetLocalStorage(_tokenKey, token);
            var authState = BuildAuthenticationState(token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task LogoutAsync()
        {
            await _jsRuntime.RemoveLocalStorage(_tokenKey);
            _httpClient.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(Task.FromResult(_anonymous));
        }
    }
}
