using RestSharp;
using RestSharp.Authenticators;
using System.Net;
using System.Net.Http;
using TenmoClient.Models;

namespace TenmoClient.Services
{
    public class AuthenticatedApiService
    {
        public static IRestClient client = null;
        private static ApiUser user = new ApiUser();

        public int UserId
        {
            get
            {
                return (user == null) ? 0 : user.UserId;
            }
        }

        public string Username
        {
            get
            {
                return (user == null) ? "anonymous" : user.Username;
            }
        }
        public bool IsLoggedIn { get { return !string.IsNullOrWhiteSpace(user.Token); } }

        public AuthenticatedApiService(string apiUrl)
        {
            if (client == null)
            {
                client = new RestClient(apiUrl);
            }
        }

        public AuthenticatedApiService(IRestClient restClient)
        {
            client = restClient;
        }

        //login endpoints
        public bool Register(LoginUser registerUser)
        {
            RestRequest request = new RestRequest("login/register");
            request.AddJsonBody(registerUser);
            IRestResponse<ApiUser> response = client.Post<ApiUser>(request);

            CheckForError(response);

            return true;
        }

        public ApiUser Login(LoginUser loginUser)
        {
            RestRequest request = new RestRequest("login");
            request.AddJsonBody(loginUser);
            IRestResponse<ApiUser> response = client.Post<ApiUser>(request);

            CheckForError(response);
            user = response.Data;
            client.Authenticator = new JwtAuthenticator(user.Token);
            return response.Data;
        }

        public void Logout()
        {
            user = new ApiUser();
            client.Authenticator = null;
        }

        /// <summary>
        /// Checks RestSharp response for errors. If error, writes a log message and throws an exception 
        /// if the call was not successful. If no error, just returns to caller.
        /// </summary>
        /// <param name="response">Response returned from a RestSharp method call.</param>
        /// <param name="action">Description of the action the application was taking. Written to the log file for context.</param>
        protected void CheckForError(IRestResponse response)
        {
            string message;
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                message = $"Error occurred - unable to reach server. Response status was '{response.ResponseStatus}'.";
                throw new HttpRequestException(message, response.ErrorException);
            }
            else if (!response.IsSuccessful)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    message = $"Authorization is required and the user has not logged in.";
                }
                else if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    message = $"The user does not have permission.";
                }
                else
                {
                    message = $"An http error occurred. Status code {(int)response.StatusCode} {response.StatusDescription}";
                }
                throw new HttpRequestException(message, response.ErrorException);
            }
        }
    }
}
