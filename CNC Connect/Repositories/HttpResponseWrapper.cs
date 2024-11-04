using System.Net;

namespace CNC_Connect.Repositories
{
    public class HttpResponseWrapper<T>
    {
        public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage)
        {
            Error = error;
            Response = response;
            HttpResponseMessage = httpResponseMessage;
        }

        public bool Error { get; set; }

        public T? Response { get; set; }

        public HttpResponseMessage HttpResponseMessage { get; set; }

        public async Task<string?> GetErrorMessageAsync()
        {
            if (!Error)
            {
                return null;
            }

            var codigoEstatus = HttpResponseMessage.StatusCode;

            if (codigoEstatus == HttpStatusCode.NotFound)
            {
                return "Recursos no encontrado";
            }
            else if (codigoEstatus == HttpStatusCode.BadRequest)
            {
                return await HttpResponseMessage.Content.ReadAsStringAsync();
            }
            else if (codigoEstatus == HttpStatusCode.Unauthorized)
            {
                return "Tienes que logearte para hacer esta operacion";
            }
            else if (codigoEstatus == HttpStatusCode.Forbidden)
            {
                return "no tienes permiso para hacer esta operacion";
            }
            return "ha ocurrido un error inesperaado";
        }
    }
}
