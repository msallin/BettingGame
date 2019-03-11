namespace BettingGame.Framework.Abstraction.Clients.UserManagement
{
    [System.CodeDom.Compiler.GeneratedCode("NSwag", "11.17.0.0 (NJsonSchema v9.10.42.0 (Newtonsoft.Json v9.0.0.0))")]
    public partial interface IUserManagementClient
    {
        /// <returns>Success</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<Profile> ApiProfileGetAsync();

        /// <returns>Success</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<Profile> ApiProfileGetAsync(System.Threading.CancellationToken cancellationToken);

        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task ApiRegistrationPostAsync(RegisterUserCommand command);

        /// <returns>Success</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task ApiRegistrationPostAsync(RegisterUserCommand command, System.Threading.CancellationToken cancellationToken);

        /// <returns>Success</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task ApiSecurityTokenServicePostAsync(SignInValidQuery query);

        /// <returns>Success</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task ApiSecurityTokenServicePostAsync(SignInValidQuery query, System.Threading.CancellationToken cancellationToken);

        /// <returns>Success</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<Profile>> ApiUserGetAsync(System.Guid? id);

        /// <returns>Success</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<Profile>> ApiUserGetAsync(System.Guid? id, System.Threading.CancellationToken cancellationToken);

    }
}