namespace BettingGame.Framework.Abstraction.Clients.Betting
{
    [System.CodeDom.Compiler.GeneratedCode("NSwag", "11.17.0.0 (NJsonSchema v9.10.42.0 (Newtonsoft.Json v9.0.0.0))")]
    public partial interface IBettingClient
    {
        /// <returns>Success</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task ApiBetGetAsync(System.Guid? gameId);

        /// <returns>Success</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task ApiBetGetAsync(System.Guid? gameId, System.Threading.CancellationToken cancellationToken);

        /// <returns>Success</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task ApiBetPostAsync(ParticipantBetCommand command);

        /// <returns>Success</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task ApiBetPostAsync(ParticipantBetCommand command, System.Threading.CancellationToken cancellationToken);

        /// <returns>Success</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<BetsWithResultQueryResult>> ApiBetScoreGetAsync();

        /// <returns>Success</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<BetsWithResultQueryResult>> ApiBetScoreGetAsync(System.Threading.CancellationToken cancellationToken);

        /// <returns>Success</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task ApiGameMetadataPostAsync(SaveGameMetadata command);

        /// <returns>Success</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task ApiGameMetadataPostAsync(SaveGameMetadata command, System.Threading.CancellationToken cancellationToken);

        /// <returns>Success</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task ApiResultPostAsync(SetActualResultCommand command);

        /// <returns>Success</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task ApiResultPostAsync(SetActualResultCommand command, System.Threading.CancellationToken cancellationToken);

        /// <returns>Success</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task ApiTeamMetadataPostAsync(SaveTeamMetadata command);

        /// <returns>Success</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        System.Threading.Tasks.Task ApiTeamMetadataPostAsync(SaveTeamMetadata command, System.Threading.CancellationToken cancellationToken);

    }
}